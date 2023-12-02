namespace aoc_2023;

public static class DataProvider
{
    public static string[] ReadLines(string fileName)
    {
        var path = $"InputFiles/{fileName}.txt";
        if (!File.Exists(path))
        {
            Console.WriteLine($"Couldn't find {fileName}");
            throw new Exception();
        }
        return File.ReadAllLines(path);
    }
}