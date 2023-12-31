using System.Reflection;

namespace aoc_2023.Solvers;

public abstract class SolversProvider
{
    public static (ISolver, string dataFileName) Get(string dayNumber)
    {
        if (dayNumber is null) throw new ArgumentNullException(nameof(dayNumber));

        var type = Assembly.GetExecutingAssembly().GetType($"aoc_2023.Solvers.SolverDay{dayNumber}");
        if (type is null) return (new NullSolver(dayNumber), string.Empty);

        var method = type.GetMethods().FirstOrDefault(m => m.GetCustomAttribute<PuzzleInputAttribute>() is not null);
        if (method is null) return (new NullSolver(dayNumber), string.Empty);
        var attribute = method.GetCustomAttribute<PuzzleInputAttribute>();

        var instance = Activator.CreateInstance(type);
        if (instance is ISolver solver)
        {
            return (solver, attribute?.FileName ?? string.Empty );
        }

        return (new NullSolver(dayNumber), string.Empty);
    }
}