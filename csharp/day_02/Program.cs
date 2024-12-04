using System.Collections.ObjectModel;

using Day_02.Services;

static Collection<string> ReadFile(string path)
{
    Collection<string> listRaw = [];

    IOService.ReadFile(path, (reader) =>
    {
        if (reader.ReadLine() is string line && line != null)
            listRaw.Add(line);
    });

    return listRaw;
}

static int CalculateReports(Collection<string> list)
{
    var safeCounter = 0;

    foreach (var line in list)
    {
        var splitedRow = line.Split(' ');

        bool isSafe = true;
        int upDown = 0; // -1 = down, 1 = up

        for (int i = 1; i < splitedRow.Length; i++)
        {
            var current = int.Parse(splitedRow[i - 1]);
            var next = int.Parse(splitedRow[i]);

            if (upDown == 0)
                upDown = current < next ? -1 : 1;

            var safetyCheck = (current, next) switch
            {
                (int c, int n) when c > n && (c - n) <= 3 && upDown > -1 => true,
                (int c, int n) when c < n && (n - c) <= 3 && upDown < 1 => true,
                (_, _) => false
            };

            if (!safetyCheck)
            {
                isSafe = false;
                break;
            }
        }

        if (isSafe) safeCounter++;
    }

    return safeCounter;
}

var list = ReadFile("data/report_data.txt");

// var list = new Collection<string>(){
//     "1 2 3 5 7 9 10",
//     "6 5 4 3 1",
//     "1 6 4 6 2 1",
//     "1 6 4 6 2 1",
// };

var safeTotal = CalculateReports(list);

Console.WriteLine($"Safe total: {safeTotal} Unsafe total: {list.Count - safeTotal}");