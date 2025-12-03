using var sr = new StreamReader("./trial.txt");

var line = sr.ReadLine();
var maxJoltage = 0;

while (line is not null)
{
    Console.WriteLine($"Line: {line}");
    var add = FindMaxLineJoltage(line);
    Console.WriteLine($"Max Joltage of line: {add}");
    maxJoltage += add;
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