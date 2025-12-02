using var sr = new StreamReader("./day2_input.txt");

var input = sr.ReadLine();
var idRanges = input.Split(',', '-');
long invalidIdSum = 0;

for (int i = 0; i < idRanges.Length; i += 2)
{
    // Console.WriteLine($"{idRanges[i]}, {idRanges[i + 1]}");
    var minRange = Int64.Parse(idRanges[i]);
    var maxRange = Int64.Parse(idRanges[i + 1]);
    for (long currentId = minRange; currentId <= maxRange; currentId++)
    {
        var currentIdString = currentId.ToString();

        // Part 1 - Solution
        // if (IsValidIdForPart1Problem(currentIdString))
        //     invalidIdSum += currentId;
        // Part 2 - Solution
        if (IsInvalidIdForPart2Problem(currentIdString))
        {
            Console.WriteLine($"Invalid Id found: {currentIdString}");
            invalidIdSum += currentId;
        }


    }
}

Console.WriteLine(invalidIdSum);

static bool IsValidIdForPart1Problem(string id)
{
    // sequence cant be repeated if not divisible by 2
    if (id.Length % 2 is not 0)
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

static bool IsInvalidIdForPart2Problem(string id)
{
    var lengthToCheck = id.Length / 2;
    for (int currentSubstringLength = 1; currentSubstringLength <= lengthToCheck; currentSubstringLength++)
    {
        // current substring can be used inside whole id
        if (id.Length % currentSubstringLength == 0)
        {
            for (int j = currentSubstringLength; j + currentSubstringLength <= id.Length; j += currentSubstringLength)
            {
                if (!CheckIfSequenceEqual(id, 0, j, currentSubstringLength))
                {
                    break;
                }
                if (j + currentSubstringLength == id.Length)
                {
                    Console.WriteLine($"INVALID ID FOUND: {id.Substring(0, currentSubstringLength)}, {currentSubstringLength}");
                    return true;
                }
            }
        }
    }
    return false;
}

static bool CheckIfSequenceEqual(string str, int start, int start2, int length)
{
    for (int increment = 0; increment < length; increment++)
    {
        if (str[start + increment] != str[start2 + increment])
        {
            return false;
        }
    }
    return true;
}