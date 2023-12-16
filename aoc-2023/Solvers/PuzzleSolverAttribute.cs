namespace aoc_2023.Solvers;

[AttributeUsage(AttributeTargets.Method)]
public class PuzzleInputAttribute : Attribute
{
    public string FileName { get; }

    public PuzzleInputAttribute(string fileName)
    {
        FileName = fileName;
    }
}