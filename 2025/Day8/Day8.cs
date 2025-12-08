const int CAPACITY = 10;

var boxes = File.ReadLines("./day8_input.txt").Select(lines =>
{
    var coordinates = lines.Split(',');
    return new JunctionBox(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));
})
.ToArray();
var totalBoxCount = boxes.Length;

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
    Console.WriteLine($"{closestDistance.Box1.GetJunctionBoxString()}");
    Console.WriteLine($"{closestDistance.Box2.GetJunctionBoxString()}");
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

            if (circuitSizes[indexBox1].Count() >= totalBoxCount)
            {
                Console.WriteLine($"Broke, multiplying: {closestDistance.Box1.CoordinateX} and {closestDistance.Box2.CoordinateX}");
                finalResult = (long)closestDistance.Box1.CoordinateX * (long)closestDistance.Box2.CoordinateX;
                break;
            }
        }
    }
    else if (box1AlreadyInCircuit)
    {
        circuitSizes[indexBox1].Add(closestDistance.Box2);
        usedBoxesWithIndex.Add(closestDistance.Box2.GetJunctionBoxString(), indexBox1);
        if (circuitSizes[indexBox1].Count() >= totalBoxCount)
        {
            Console.WriteLine($"Broke, multiplying: {closestDistance.Box1.CoordinateX} and {closestDistance.Box2.CoordinateX}");
            finalResult = (long)closestDistance.Box1.CoordinateX * (long)closestDistance.Box2.CoordinateX;
            break;
        }
    }
    else if (box2AlreadyInCircuit)
    {
        circuitSizes[indexBox2].Add(closestDistance.Box1);
        usedBoxesWithIndex.Add(closestDistance.Box1.GetJunctionBoxString(), indexBox2);
        if (circuitSizes[indexBox2].Count() >= totalBoxCount)
        {
            Console.WriteLine($"Broke, multiplying: {closestDistance.Box1.CoordinateX} and {closestDistance.Box2.CoordinateX}");
            finalResult = (long)closestDistance.Box1.CoordinateX * (long)closestDistance.Box2.CoordinateX;
            break;
        }
    }
    else
    {
        circuitSizes[currentCapacityIndex] = new List<JunctionBox>();
        circuitSizes[currentCapacityIndex].Add(closestDistance.Box1);
        circuitSizes[currentCapacityIndex].Add(closestDistance.Box2);
        usedBoxesWithIndex.Add(closestDistance.Box1.GetJunctionBoxString(), currentCapacityIndex);
        usedBoxesWithIndex.Add(closestDistance.Box2.GetJunctionBoxString(), currentCapacityIndex);
        if (circuitSizes[currentCapacityIndex].Count() >= totalBoxCount)
        {
            Console.WriteLine($"Broke, multiplying: {closestDistance.Box1.CoordinateX} and {closestDistance.Box2.CoordinateX}");
            finalResult = (long)closestDistance.Box1.CoordinateX * (long)closestDistance.Box2.CoordinateX;
            break;
        }
        currentCapacityIndex++;
        if (currentCapacityIndex % CAPACITY == 0)
        {
            Array.Resize(ref circuitSizes, circuitSizes.Length + CAPACITY);
        }
    }
}

var biggest = 0;
var secondBiggest = 0;
var thirdBiggest = 0;

for (int i = 0; i < currentCapacityIndex; i++)
{
    if (circuitSizes[i] is null)
    {
        break;
    }
    Console.WriteLine($"Size: {circuitSizes[i].Count()}");
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