var allLinesArray = File.ReadLines("./day4_input.txt").Select(line => line.ToCharArray()).ToArray();
int movableRoles = 0;
var removalHappened = false;

do
{
    removalHappened = false;
    for (int rowIndex = 0; rowIndex < allLinesArray.Length; rowIndex++)
    {
        var previousRowModifier = rowIndex - 1 >= 0 ? 1 : 0;
        var nextRowModifier = rowIndex + 1 < allLinesArray.Length ? 1 : 0;
        for (int colIndex = 0; colIndex < allLinesArray[rowIndex].Length; colIndex++)
        {
            // Console.WriteLine($"-- Testing row: {rowIndex}, col: {colIndex} --");
            if (allLinesArray[rowIndex][colIndex] != '@')
                continue;
            var adjacentRols = -1;
            var previousColModifier = colIndex - 1 >= 0 ? 1 : 0;
            var nextColModifier = colIndex + 1 < allLinesArray[rowIndex].Length ? 1 : 0;
            for (int i = rowIndex - previousRowModifier; i <= rowIndex + nextRowModifier; i++)
            {
                for (int j = colIndex - previousColModifier; j <= colIndex + nextColModifier; j++)
                {
                    // Console.WriteLine($"Testing row: {i}, col: {j}");
                    if (allLinesArray[i][j] == '@')
                        adjacentRols++;
                }
            }
            if (adjacentRols < 4)
            {
                Console.WriteLine($"Found at Row: {rowIndex}, col: {colIndex}");
                movableRoles++;
                allLinesArray[rowIndex][colIndex] = 'x';
                removalHappened = true;
            }
        }
    }
}
while (removalHappened);

Console.WriteLine($"Result: {movableRoles}");