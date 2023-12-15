namespace aoc_2023.Solvers;

[PuzzleSolver("15-02", 15)]
public class SolverDay15 : Solver
{
    public override void Solve(string[] input)
    {
        var parts = input[0].Split(',');

        var answer = 0L;

        foreach (var part in parts){
            var res = 0;
            foreach (var c in part)
            {
                res += (int)c;
                res *= 17;
                res %=256;
            }
            answer += res;
        }

        Console.WriteLine("Answer: " + answer);
    }
}