using System.Diagnostics;

namespace aoc_2023.Solvers;

[PuzzleSolver("09-02", 9)]
public class SolverDay09 : Solver
{

    public override void Solve(string[] input)
    {
        var answer = input.Select(line => line.Split(" ")
                        .Select(int.Parse).ToList())
                        .Select(Extrapolate)
                        .Sum();
        
        Console.WriteLine(answer);

        int Extrapolate(List<int> history)
        {
            var iterations = new List<List<int>> { history };

            var (ready, pointer) = (false, 0);
            while (!ready)
            {
                var next = new List<int>();
                for (var i = 0; i < iterations[pointer].Count-1; i++)
                {
                    next.Add(iterations[pointer][i+1] - iterations[pointer][i]);
                }
                iterations.Add(next);
                pointer++;
                ready = next.All(x => x is 0);
            }

            var extrapolated = 0;
            for (var i = iterations.Count-2; i >= 0; i--)
            {
                extrapolated = iterations[i][0] - extrapolated;
            }
            
            return extrapolated;
        }
    }

}