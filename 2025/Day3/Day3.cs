using var sr = new StreamReader("./day3_input.txt");

var line = sr.ReadLine();
long maxJoltage = 0;

while (line is not null)
{
    Console.WriteLine($"Line: {line}");
    // part 1 Solution
    // var add = FindMaxLineJoltage(line, 0, 1);
    // maxJoltage += add;
    // part 2 Solution
    var add = FindMaxLineJoltage(line, 0, 11);
    maxJoltage += Int64.Parse(add);
    Console.WriteLine($"Max Joltage of line: {add}");
    line = sr.ReadLine();
}

Console.WriteLine(maxJoltage);

static string FindMaxLineJoltage(string line, int minIndex, int offset)
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

    return $"{highestChar}{FindMaxLineJoltage(line, highestCharIndex + 1, offset - 1)}";
}