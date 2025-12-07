using var sr = new StreamReader("./day7_input.txt");
var lines = File.ReadLines("./day7_input.txt").ToArray();
var beamIndexes = new List<int>();
var beamIndexHits = new Dictionary<int, long>();
// var line = sr.ReadLine();
var currentDepth = 0;
foreach (var line in lines)
{
    if (beamIndexes.Count() is 0)
    {
        var startingIndex = line.IndexOf('S');
        beamIndexes.Add(startingIndex);
        beamIndexHits.Add(startingIndex, 1L);
    }
    else
    {
        var addedIndexes = new Dictionary<int, long>();
        foreach (var beamIndex in beamIndexes)
        {
            // Console.WriteLine($"Checking Index: {beamIndex}");
            if (line[beamIndex] == '^')
            {
                if (addedIndexes.TryGetValue(beamIndex - 1, out var nextLeft))
                {
                    addedIndexes[beamIndex - 1] = nextLeft + beamIndexHits[beamIndex];
                }
                else
                {
                    addedIndexes.Add(beamIndex - 1, beamIndexHits[beamIndex]);
                }

                if (addedIndexes.TryGetValue(beamIndex + 1, out var nextRight))
                {
                    addedIndexes[beamIndex + 1] = nextRight + beamIndexHits[beamIndex];
                }
                else
                {
                    addedIndexes.Add(beamIndex + 1, beamIndexHits[beamIndex]);
                }
            }
            else
            {
                if (addedIndexes.TryGetValue(beamIndex, out var hits))
                {
                    addedIndexes[beamIndex] = beamIndexHits[beamIndex] + hits;
                }
                else
                {
                    addedIndexes.Add(beamIndex, beamIndexHits[beamIndex]);
                }
            }

        }
        beamIndexHits = addedIndexes;
        beamIndexes = addedIndexes.Keys.ToList();
    }
    Visualize(lines, beamIndexHits, currentDepth);
    // line = sr.ReadLine();
    currentDepth++;
}

var sum = 0L;
foreach (var key in beamIndexHits.Keys)
{
    sum += beamIndexHits[key];
}

Console.WriteLine(sum);


static void Visualize(string[] lines, Dictionary<int, long> beams, int currentDepth)
{
    var index = 0;
    foreach (var character in lines[currentDepth])
    {
        if (beams.ContainsKey(index))
        {
            Console.Write(beams[index]);
        }
        else
        {
            Console.Write(character);
        }
        index++;
    }
    Console.Write(System.Environment.NewLine);
}