using var sr = new StreamReader("./day2_input.txt");

var input = sr.ReadLine();
var idRanges = input.Split(',', '-');
long invalidIdSum = 0;

for (int i = 0; i < idRanges.Length; i += 2)
{
    Console.WriteLine($"{idRanges[i]}, {idRanges[i + 1]}");
    var minRange = Int64.Parse(idRanges[i]);
    var maxRange = Int64.Parse(idRanges[i + 1]);
    for (long currentId = minRange; currentId <= maxRange; currentId++)
    {
        var currentIdString = currentId.ToString();

        // Part 1 - Solution
        if (IsValidIdForPart1Problem(currentIdString))
            invalidIdSum += currentId;
    }
}

Console.WriteLine(invalidIdSum);

static bool IsValidIdForPart1Problem(string id)
{
    // sequence cant be repeated if not divisible by 2
    if (currentIdString.Length % 2 is not 0)
        return false;
    var middle = id.Length / 2;
    var leftSide = id.Substring(0, middle);
    var rightSide = id.Substring(middle);
    var valid = true;
    for (int j = 0; j < leftSide.Length; j++)
    {
        if (leftSide[j] != rightSide[j])
        {
            valid = false;
            break;
        }
    }
    return valid;
}