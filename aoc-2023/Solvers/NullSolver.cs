namespace aoc_2023.Solvers;


public class NullSolver : Solver
{
    private readonly string dayNumber;

    public NullSolver(string dayNumber)
    {
        this.dayNumber = dayNumber;
    }

    [PuzzleInput("00-00")]
    public override void Solve(string[] input)
    {
        Console.WriteLine($"Failed to find Solver for day {dayNumber}");
    }
}