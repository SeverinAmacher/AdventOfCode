using var sr = new StreamReader("./day7_input.txt");

var beamIndexes = new List<int>();
var line = sr.ReadLine();
var totalSplits = 0;
while (line is not null)
{
    if (beamIndexes.Count() is 0)
    {
        beamIndexes.Add(line.IndexOf('S'));
    }
    else
    {
        var addedIndexes = new HashSet<int>();
        foreach (var beamIndex in beamIndexes)
        {
            Console.WriteLine($"Checking Index: {beamIndex}");
            if (line[beamIndex] == '^')
            {
                totalSplits++;
                addedIndexes.Add(beamIndex - 1) ? 1 : 0;
                addedIndexes.Add(beamIndex + 1) ? 1 : 0;
            }
            else
            {
                addedIndexes.Add(beamIndex);
            }
        }
        beamIndexes = addedIndexes.ToList();
        Console.WriteLine($"New Indexes: {string.Join(',', beamIndexes)}");
    }
    line = sr.ReadLine();
}

Console.WriteLine(totalSplits);