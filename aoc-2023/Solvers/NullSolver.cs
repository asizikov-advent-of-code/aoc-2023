namespace aoc_2023.Solvers;

public class NullSolver : Solver
{
    private readonly string dayNumber;

    public NullSolver(string dayNumber)
    {
        this.dayNumber = dayNumber;
    }
    public override void Solve(string[] input)
    {
        Console.WriteLine($"Failed to find Solver for day {dayNumber}");
    }

    public override string FileName { get; } = string.Empty;
}