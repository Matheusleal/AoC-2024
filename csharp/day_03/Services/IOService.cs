using System.Text;

namespace Day_03.Services;

public static class IOService
{
    public static void ReadFile(string path, Action<StreamReader> transform)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        using var sr = new StreamReader(fs);

        while (!sr.EndOfStream)
        {
            transform(sr);
        }
        fs.Close();
    }
}