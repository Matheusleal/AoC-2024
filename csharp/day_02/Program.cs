using System.Collections.ObjectModel;

using Day_02.Services;

static Collection<string> LoadReports(string path)
{
    Collection<string> listRaw = [];

    IOService.ReadFile(path, (reader) =>
    {
        if (reader.ReadLine() is string line && line != null)
            listRaw.Add(line);
    });

    return listRaw;
}

static bool SafetyCheck(List<string> line)
{
    bool isSafe = true;
    int upDown = 0; // -1 = down, 1 = up

    for (int i = 1; i < line.Count; i++)
    {
        var current = int.Parse(line[i - 1]);
        var next = int.Parse(line[i]);

        if (upDown == 0)
            upDown = current < next ? 1 : -1;

        var safetyCheck = (current, next) switch
        {
            (int c, int n) when c > n && (c - n) <= 3 && upDown == -1 => true,
            (int c, int n) when c < n && (n - c) <= 3 && upDown == 1 => true,
            (_, _) => false
        };

        if (!safetyCheck)
        {
            isSafe = false;
            break;
        }
    }

    return isSafe;
}

static int CalculateReports_Part1(Collection<string> list)
{
    var safeCounter = 0;

    foreach (var line in list)
    {
        var splitedRow = line.Split(' ').ToList();

        if (SafetyCheck(splitedRow))
            safeCounter++;
    }

    return safeCounter;
}
static int CalculateReports_Part2(Collection<string> list)
{
    var safeCounter = 0;

    foreach (var line in list)
    {
        var splitedRow = line.Split(' ').ToList();

        if (SafetyCheck(splitedRow))
            safeCounter++;

        else
            for (int i = 0; i < splitedRow.Count; i++)
            {
                var copy = splitedRow.ToList();
                copy.RemoveAt(i);

                if (SafetyCheck(copy))
                {
                    safeCounter++;
                    break;
                }
            }
    }

    return safeCounter;
}

var list = LoadReports("data/report_data.txt");

var safeTotal_part1 = CalculateReports_Part1(list);
var safeTotal_part2 = CalculateReports_Part2(list);

Console.WriteLine($"Safe total: {safeTotal_part1}, Unsafe total: {list.Count - safeTotal_part1}");
Console.WriteLine($"Safe total: {safeTotal_part2}, Unsafe total: {list.Count - safeTotal_part2}");