namespace aoc_2023.Solvers;

public abstract class SolversProvider
{
    public static Solver Get(string dayNumber)
    {
        if (dayNumber == null) throw new ArgumentNullException(nameof(dayNumber));
        var type = Type.GetType($"aoc_2023.Solvers.SolverDay{dayNumber}");
        if (type is null)
        {
            return new NullSolver(dayNumber);
        }
        
        var instance = Activator.CreateInstance(type);
        if (instance is Solver solver)
        {
            return solver;
        }

        return new NullSolver(dayNumber);
    }
}