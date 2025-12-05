using var sr = new StreamReader("./day5_input.txt");
var freshIds = new List<(long LowerBound, long UpperBound)>();
long allFreshIdCount = 0;

var line = sr.ReadLine();
while (!String.IsNullOrEmpty(line))
{
    var idRange = line.Split('-');
    var lowerBound = Int64.Parse(idRange[0]);
    var upperBound = Int64.Parse(idRange[1]);

    freshIds.Add((lowerBound, upperBound));
    line = sr.ReadLine();
}

var orderedFreshIds = freshIds.OrderBy(range => range.LowerBound).ThenByDescending(range => range.UpperBound);
var processedLines = new List<(long LowerBound, long UpperBound)>();

foreach (var range in orderedFreshIds)
{
    Console.WriteLine($"Processing Line: {range.LowerBound}-{range.UpperBound}");
    if (processedLines.Any(line => line.LowerBound <= range.LowerBound && line.UpperBound >= range.UpperBound))
        continue;

    var trouble = processedLines.Where(line => line.LowerBound > range.LowerBound && line.UpperBound < range.UpperBound);
    if (trouble.Any())
    {
        Console.WriteLine($"-- There is trouble, Count {trouble.Count()} --");
    }

    var (lowerBoundForCalculation, upperBoundForCalculation, correctionModifier) = CalculateBounds(processedLines, range.LowerBound, range.UpperBound);

    var idsCountOfRange = upperBoundForCalculation - lowerBoundForCalculation + correctionModifier;
    if (idsCountOfRange <= 0)
    {
        Console.WriteLine($"Something went wrong, id count: {idsCountOfRange}");
    }

    Console.WriteLine($"Lower Bound: {lowerBoundForCalculation}, Upper Bound: {upperBoundForCalculation}, Correction: {correctionModifier}");
    allFreshIdCount += idsCountOfRange;
    processedLines.Add(range);
}

Console.WriteLine(allFreshIdCount);

// Day 1 Solution
// line = sr.ReadLine();
// var availableFreshIdCount = 0;
// while (line is not null)
// {
//     var id = Int64.Parse(line);
//     if (freshIds.Any(bounds => bounds.LowerBound <= id && bounds.UpperBound >= id))
//         availableFreshIdCount++;
//     line = sr.ReadLine();
// }
// 
// Console.WriteLine(availableFreshIdCount);

static (long lowerBound, long upperBound, int correctionModifier) CalculateBounds(List<(long LowerBound, long UpperBound)> freshIds, long lowerBound, long upperBound)
{
    // The correction modifier helps get the exact count of the range
    // 1. No other overlapping part => add 1 since upperBound - lowerBound returns 1 less than actually contained in the range
    // 2. One overlapping part either upper- or lowerbound => add 0 since equation returns exactly the ids of the range
    // 3. upper- and lowerbound overlapping => add -1 since equation returns one more than in the range
    var correctionModifier = 1;
    var lowerBoundRange = freshIds.Where(range => range.LowerBound <= lowerBound && range.UpperBound >= lowerBound);
    if (lowerBoundRange.Any())
    {
        lowerBound = lowerBoundRange.Max(range => range.UpperBound);
        correctionModifier--;
    }
    var upperBoundRange = freshIds.Where(range => range.LowerBound <= upperBound && range.UpperBound >= upperBound);
    if (upperBoundRange.Any())
    {
        upperBound = upperBoundRange.Min(range => range.LowerBound);
        correctionModifier--;
    }
    return (lowerBound, upperBound, correctionModifier);
}