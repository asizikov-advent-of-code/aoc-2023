namespace aoc_2023.Solvers;

public class SolverDay06 : ISolver
{
    [PuzzleInput("06-02")]
    public void Solve(string[] input)
    {
        var time = input[0].Split(":")[1].Where(char.IsDigit).Aggregate(0L, (current, c) => current * 10 + c - '0');
        var distance = input[1].Split(":")[1].Where(char.IsDigit).Aggregate(0L, (current, c) => current * 10 + c - '0');

        var win = 0;
        for (var t = 0; t <= time; t++)
        {
            var d = t * (time - t);
            if (d > distance) win++;
        }
        Console.WriteLine(win);
    }

}