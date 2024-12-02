using System.Collections.ObjectModel;

using Day_01.Services;


static (Collection<int> left, Collection<int> right) ReadFile(string path)
{
    Collection<int> leftRaw = [];
    Collection<int> rightRaw = [];

    IOService.ReadFile(path, (reader) =>
    {
        var line = reader.ReadLine();

        if (line != null)
        {
            var split = line.Split("   ");
            leftRaw.Add(int.Parse(split[0]));
            rightRaw.Add(int.Parse(split[1]));
        }
    });

    return (leftRaw, rightRaw);
}

static int CalculateTotalDistanceScore(Collection<int> left, Collection<int> right)
{
    var leftList = left.OrderBy(x => x).ToList();
    var rightList = right.OrderBy(x => x).ToList();
    var total = 0;

    for (int i = 0; i < leftList.Count; i++)
    {
        total += Math.Abs(leftList[i] - rightList[i]);
    }

    return total;
}

static int CalculateTotalSimalarityScore(Collection<int> left, Collection<int> right)
{
    var leftList = left.OrderBy(x => x).ToList();
    var rightList = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
    var total = 0;

    for (int i = 0; i < leftList.Count; i++)
    {
        var exists = rightList.TryGetValue(leftList[i], out var count);

        if (exists)
            total += leftList[i] * count;
    }

    return total;
}


var (left, right) = ReadFile("./data/location_id_list.txt");

var totalDistanceScore = CalculateTotalDistanceScore(left, right);
var totalSimalarityScore = CalculateTotalSimalarityScore(left, right);


Console.WriteLine("Total Distance score: {0}", totalDistanceScore);
Console.WriteLine("Total Simalarity Score: {0}", totalSimalarityScore);