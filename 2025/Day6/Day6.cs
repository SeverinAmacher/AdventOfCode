var lines = File.ReadLines("./day6_input.txt");
var arr = new string[lines.Count()][];

var index = 0;
foreach (var line in lines)
{
    var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var newArr = new string[parts.Length];
    var i = 0;
    foreach (var part in parts)
    {
        newArr[i] = part;
        i++;
    }
    // Console.WriteLine($"Line {index}: {string.Join(',', newArr)}");
    arr[index] = newArr;
    index++;
}

var lastLine = arr[arr.Length - 1];
var results = new long[lastLine.Length];
for (int i = 0; i < arr.Length - 1; i++)
{
    for (int j = 0; j < arr[i].Length; j++)
    {
        results[j] = lastLine[j] switch
        {
            "+" => results[j] + Int64.Parse(arr[i][j]),
            "*" => results[j] == 0
                ? 1 * Int64.Parse(arr[i][j])
                : results[j] * Int64.Parse(arr[i][j]),
            _ => throw new Exception("Invalid Operator"),
        };
        // Console.WriteLine($"Result {i}, {j}: {results[j]}");
    }
}

var grandTotal = 0L;
for (int i = 0; i < results.Length; i++)
{
    grandTotal += results[i];
}

Console.WriteLine(grandTotal);