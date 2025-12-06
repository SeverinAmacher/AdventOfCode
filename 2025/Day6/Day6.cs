using System.Text;

var lines = File.ReadLines("./day6_input.txt").ToArray();

var operations = lines[lines.Length - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
var results = new long[operations.Length];

var columnIndex = 0;
for (int index = 0; index < lines[0].Length; index++)
{
    var sb = new StringBuilder();
    for (int j = 0; j < lines.Length - 1; j++)
    {
        if (lines[j][index] != ' ')
        {
            sb.Append(lines[j][index]);
        }
    }
    if (sb.Length > 0)
    {
        var number = Int64.Parse(sb.ToString());
        results[columnIndex] = operations[columnIndex] switch
        {
            "+" => results[columnIndex] + number,
            "*" => results[columnIndex] == 0
                ? 1 * number
                : results[columnIndex] * number,
            _ => throw new Exception("Invalid Operator"),
        };
    }
    else
    {
        columnIndex++;
    }
}

var grandTotal = 0L;
for (int i = 0; i < results.Length; i++)
{
    Console.WriteLine($"Result Column {i}: {results[i]}");
    grandTotal += results[i];
}

Console.WriteLine(grandTotal);