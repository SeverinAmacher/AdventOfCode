const int CAPACITY = 10;

var boxes = File.ReadLines("./day8_input.txt").Select(lines =>
{
    var coordinates = lines.Split(',');
    return new JunctionBox(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));
})
.ToArray();

var allDistances = new PriorityQueue<BoxDistance, double>();

for (int i = 0; i < boxes.Length - 1; i++)
{
    for (int j = i + 1; j < boxes.Length; j++)
    {
        var distance = GetEuclideanDistance(boxes[i], boxes[j]);
        allDistances.Enqueue(new BoxDistance(boxes[i], boxes[j], distance), distance);
    }
}

var circuitSizes = new List<JunctionBox>[CAPACITY];
var currentCapacityIndex = 0;
var usedBoxesWithIndex = new Dictionary<string, int>();

var finalResult = 0L;

while (allDistances.TryDequeue(out var closestDistance, out var distance))
{
    var modifiedCircuitBoxCount = 0;
    var box1AlreadyInCircuit = usedBoxesWithIndex.TryGetValue(closestDistance.Box1.GetJunctionBoxString(), out var indexBox1);
    var box2AlreadyInCircuit = usedBoxesWithIndex.TryGetValue(closestDistance.Box2.GetJunctionBoxString(), out var indexBox2);
    if (box1AlreadyInCircuit && box2AlreadyInCircuit)
    {
        if (indexBox1 != indexBox2)
        {
            var circuitToCombine = circuitSizes[indexBox2];
            foreach (var junctionBox in circuitSizes[indexBox2])
            {
                circuitSizes[indexBox1].Add(junctionBox);
                usedBoxesWithIndex[junctionBox.GetJunctionBoxString()] = indexBox1;
            }
            circuitSizes[indexBox2] = new List<JunctionBox>();
            modifiedCircuitBoxCount = circuitSizes[indexBox1].Count();
        }
    }
    else if (box1AlreadyInCircuit)
    {
        circuitSizes[indexBox1].Add(closestDistance.Box2);
        usedBoxesWithIndex.Add(closestDistance.Box2.GetJunctionBoxString(), indexBox1);
        modifiedCircuitBoxCount = circuitSizes[indexBox1].Count();
    }
    else if (box2AlreadyInCircuit)
    {
        circuitSizes[indexBox2].Add(closestDistance.Box1);
        usedBoxesWithIndex.Add(closestDistance.Box1.GetJunctionBoxString(), indexBox2);
        modifiedCircuitBoxCount = circuitSizes[indexBox2].Count();
    }
    else
    {
        circuitSizes[currentCapacityIndex] = new List<JunctionBox>();
        circuitSizes[currentCapacityIndex].AddRange([closestDistance.Box1, closestDistance.Box2]);
        usedBoxesWithIndex.Add(closestDistance.Box1.GetJunctionBoxString(), currentCapacityIndex);
        usedBoxesWithIndex.Add(closestDistance.Box2.GetJunctionBoxString(), currentCapacityIndex);
        modifiedCircuitBoxCount = circuitSizes[currentCapacityIndex].Count();
        currentCapacityIndex++;
        if (currentCapacityIndex % CAPACITY == 0)
        {
            Array.Resize(ref circuitSizes, circuitSizes.Length + CAPACITY);
        }
    }

    if (modifiedCircuitBoxCount >= boxes.Length)
    {
        finalResult = (long)closestDistance.Box1.CoordinateX * (long)closestDistance.Box2.CoordinateX;
        break;
    }
}


#region Not used in Part 2
var biggest = 0;
var secondBiggest = 0;
var thirdBiggest = 0;

for (int i = 0; i < currentCapacityIndex; i++)
{
    if (circuitSizes[i] is null)
    {
        break;
    }
    if (circuitSizes[i].Count() > biggest)
    {
        thirdBiggest = secondBiggest;
        secondBiggest = biggest;
        biggest = circuitSizes[i].Count();
    }
    else if (circuitSizes[i].Count() > secondBiggest)
    {
        thirdBiggest = secondBiggest;
        secondBiggest = circuitSizes[i].Count();
    }
    else if (circuitSizes[i].Count() > thirdBiggest)
    {
        thirdBiggest = circuitSizes[i].Count();
    }
}

var result = biggest * secondBiggest * thirdBiggest;
Console.WriteLine($"{biggest}, {secondBiggest}, {thirdBiggest} = {result}");
#endregion

Console.WriteLine(finalResult);

static double GetEuclideanDistance(JunctionBox box1, JunctionBox box2)
{
    return Math.Sqrt(Math.Pow(box1.CoordinateX - box2.CoordinateX, 2)
        + Math.Pow(box1.CoordinateY - box2.CoordinateY, 2)
        + Math.Pow(box1.CoordinateZ - box2.CoordinateZ, 2));
}

public static class JunctionBoxExtensions
{
    public static string GetJunctionBoxString(this JunctionBox box)
    {
        return $"{box.CoordinateX},{box.CoordinateY},{box.CoordinateZ}";
    }
}

public record BoxDistance(JunctionBox Box1, JunctionBox Box2, double Distance);

public record JunctionBox(int CoordinateX, int CoordinateY, int CoordinateZ);