using System.Collections.ObjectModel;


using Day_01.Services;

Collection<int> leftRaw = [];
Collection<int> rightRaw = [];

IOService
.ReadFile("./data/location_id_list.txt", (reader) => {
    var line = reader.ReadLine();

    if (line != null)
    {
        var split = line.Split("   ");
        leftRaw.Add(int.Parse(split[0]));
        rightRaw.Add(int.Parse(split[1]));
    }
});

var leftList = leftRaw.OrderBy(x => x).ToList();
var rightList = rightRaw.OrderBy(x => x).ToList();
var total = 0;

for (int i = 0; i < leftList.Count; i++)
{
   total += Math.Abs(leftList[i] - rightList[i]); 
}

Console.WriteLine("Total: {0}", total);