using var sr = new StreamReader("./day3_input.txt");

var line = sr.ReadLine();
long maxJoltage = 0;

while (line is not null)
{
    Console.WriteLine($"Line: {line}");
    // part 1 Solution
    // var add = FindMaxLineJoltage(line);
    // maxJoltage += add;
    // part 2 Solution
    var add = FindMaxLineJoltagePart2(line, 0, 11);
    maxJoltage += Int64.Parse(add);
    Console.WriteLine($"Max Joltage of line: {add}");
    line = sr.ReadLine();
}

Console.WriteLine(maxJoltage);

static int FindMaxLineJoltage(string line)
{
    var highestChar = '0';
    var highestCharIndex = 0;
    var secondHighestChar = '0';

    for (int currentIndex = 0; currentIndex < line.Length - 1; currentIndex++)
    {
        if (line[currentIndex] > highestChar)
        {
            highestChar = line[currentIndex];
            highestCharIndex = currentIndex;
        }
    }

    for (int currentIndex = highestCharIndex + 1; currentIndex < line.Length; currentIndex++)
    {
        if (line[currentIndex] > secondHighestChar)
        {
            secondHighestChar = line[currentIndex];
        }
    }

    return Int32.Parse($"{highestChar}{secondHighestChar}");
}

static string FindMaxLineJoltagePart2(string line, int minIndex, int offset)
{
    var highestChar = '0';
    var highestCharIndex = 0;

    for (int currentIndex = minIndex; currentIndex < line.Length - offset; currentIndex++)
    {
        if (line[currentIndex] > highestChar)
        {
            highestChar = line[currentIndex];
            highestCharIndex = currentIndex;
        }
    }

    if (offset == 0)
    {
        return $"{highestChar}";
    }

    return $"{highestChar}{FindMaxLineJoltagePart2(line, highestCharIndex + 1, offset - 1)}";
}