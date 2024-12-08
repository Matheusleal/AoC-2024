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
static Collection<(int A, int B)> ParseData_Part2(Collection<string> rows)
{
    List<string> allMatches = [];

    Collection<(int A, int B)> rawLines = [];

    foreach (var line in rows)
    {
        var matches = Regex.Matches(line, @"mul\(\d+,\d+\)|do\(\)|don't\(\)");

        foreach (Match match in matches)
        {
            var replaced =
            match
            .Value
            .Replace("do()", "go")
            .Replace("don't()", "stop")
            .Replace("mul(", "")
            .Replace(")", "");

            allMatches.Add(replaced);
        }
    }

    bool stop = false;
    foreach (var match in allMatches)
    {
        if (match == "stop")
        {
            stop = true;
            continue;
        }

        if (match == "go")
        {
            stop = false;
            continue;
        }

        if (stop) continue;

        var values = match.Split(",");

        var x = int.Parse(values[0]);
        var y = int.Parse(values[1]);

        rawLines.Add((x, y));
    }
    return rawLines;
}
static int CalculateValues(Collection<(int A, int B)> rawLines)
{
    var result = rawLines
        .Aggregate(0, (acc, line) => acc + line.A * line.B);

    return result;
}

var lines = ReadFile("data/input_data.txt");

var parsedData_part1 = ParseData_Part1(lines);
var result_part1 = CalculateValues(parsedData_part1);

var parsedData_part2 = ParseData_Part2(lines);
var result_part2 = CalculateValues(parsedData_part2);

Console.WriteLine($"Result Part 1: {result_part1}");
Console.WriteLine($"Result Part 2: {result_part2}");