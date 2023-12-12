namespace aoc_2023.Solvers;

[AttributeUsage(AttributeTargets.Class)]
public class PuzzleSolverAttribute : Attribute
{
    public string FileName { get; }
    public int DayNumber { get; }
    
    public PuzzleSolverAttribute(string fileName, int dayNumber)
    {
        FileName = fileName;
        DayNumber = dayNumber;
    }
}