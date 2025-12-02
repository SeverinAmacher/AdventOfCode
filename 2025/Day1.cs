using System;
using System.IO;

const int totalPositions = 100; // positions from 0 to 99
const int maxPosition = 99;
var position = 50;
var numberOfZeroes = 0; // number of times the current position hit 0
try
{
    using var sr = new StreamReader("./day1_input.txt");
    var line = sr.ReadLine();
    while (line is not null)
    {
        var (rotationDirection, rotationCount) = GetInstructions(line);
        var (newPosition, timesHitZero) = CalculateNewPosition(position, rotationDirection, rotationCount);
        Console.WriteLine($"-- Result for {rotationDirection}{rotationCount} Current Position: {position}, Current numbers of Zeroes {numberOfZeroes} --");
        numberOfZeroes += timesHitZero;
        Console.WriteLine($"New Position: {newPosition}, timesHitZero: {timesHitZero}, newNumbersOfZeroes: {numberOfZeroes}");
        position = newPosition;
        line = sr.ReadLine();
    }
}
catch (Exception e)
{
    Console.WriteLine("Exception: {exception}", e.Message);
}

Console.WriteLine(numberOfZeroes);

static (char rotationDirection, int rotationCount) GetInstructions(string input)
{
    return (input[0], Int32.Parse(input.Substring(1)));
}

static (int position, int timesHitZero) CalculateNewPosition(int currentPosition, char rotationDirection, int rotationCount)
{
    switch (rotationDirection)
    {
        case 'L':
            if (currentPosition == rotationCount)
                return (0, 1);
            if (currentPosition > rotationCount)
                return (currentPosition - rotationCount, 0);
            // current position is not 0, additional +1 on overflow
            var additionalIncrease = currentPosition is 0 ? 0 : 1;
            var rotationOverflow = rotationCount - currentPosition;
            return (maxPosition - ((rotationOverflow - 1) % totalPositions), (rotationOverflow / totalPositions) + additionalIncrease);
        case 'R':
            var endPosition = (currentPosition + rotationCount);
            return (endPosition % totalPositions, endPosition / totalPositions);
        default:
            throw new Exception("Invalid rotation direction");
    }
}