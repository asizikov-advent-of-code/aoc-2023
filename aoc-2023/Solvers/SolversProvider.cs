using System.Reflection;

namespace aoc_2023.Solvers;

public abstract class SolversProvider
{
    public static (Solver, string dataFileName) Get(string dayNumber)
    {
        if (dayNumber == null) throw new ArgumentNullException(nameof(dayNumber));
        
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(Solver)));
        
        var type = types.FirstOrDefault(t => t.GetCustomAttribute<PuzzleSolverAttribute>()?.DayNumber.ToString("00") == dayNumber);
        
        if (type is null)
        {
            return (new NullSolver(dayNumber), string.Empty);
        }
        
        var instance = Activator.CreateInstance(type);
        if (instance is Solver solver)
        {
            return (solver, type.GetCustomAttribute<PuzzleSolverAttribute>()?.FileName ?? string.Empty );
        }

        return (new NullSolver(dayNumber), string.Empty);
    }
}