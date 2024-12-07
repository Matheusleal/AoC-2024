using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Day_03.Services;

static Collection<string> ReadFile(string path)
{
    Collection<string> rawLines = [];

    IOService.ReadFile(path, (reader) =>
    {
        if (reader.ReadLine() is string line && !string.IsNullOrEmpty(line))
        {
            rawLines.Add(line);
        }
    });

    return rawLines;
}

static Collection<(int A, int B)> ParseData_Part1(Collection<string> rows)
{
    Collection<(int A, int B)> rawLines = [];

    foreach (var line in rows)
    {
        var matches = Regex.Matches(line, @"mul\(\d+,\d+\)");

        foreach (Match match in matches)
        {
            var values =
            match
            .Value
            .Replace("mul(", "")
            .Replace(")", "")
            .Split(",");

            var x = int.Parse(values[0]);
            var y = int.Parse(values[1]);

            rawLines.Add((x, y));
        }
    }

    return rawLines;
}
static int CalculateValues_Part1(Collection<(int A, int B)> rawLines)
{
    var result = rawLines
        .Aggregate(0, (acc, line) => acc + line.A * line.B);

    return result;
}

var lines = ReadFile("data/input_data.txt");

var parsedData_part1 = ParseData_Part1(lines);
var result_part1 = CalculateValues_Part1(parsedData_part1);

Console.WriteLine($"Result Part 1: {result_part1}");