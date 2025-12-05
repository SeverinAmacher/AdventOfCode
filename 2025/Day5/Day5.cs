using var sr = new StreamReader("./day5_input.txt");
var freshIds = new List<(long LowerBound, long UpperBound)>();

var line = sr.ReadLine();
while (line is not null)
{
    if (String.IsNullOrEmpty(line))
        break;
    var idRange = line.Split('-');
    freshIds.Add((Int64.Parse(idRange[0]), Int64.Parse(idRange[1])));
    line = sr.ReadLine();
}

line = sr.ReadLine();
var availableFreshIdCount = 0;
while (line is not null)
{
    var id = Int64.Parse(line);
    if (freshIds.Any(bounds => bounds.LowerBound <= id && bounds.UpperBound >= id))
        availableFreshIdCount++;
    line = sr.ReadLine();
}

Console.WriteLine(availableFreshIdCount);