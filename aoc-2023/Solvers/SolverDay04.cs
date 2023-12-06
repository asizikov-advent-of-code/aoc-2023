using System.Linq.Expressions;

namespace aoc_2023.Solvers;

public class SolverDay04 : Solver
{
    public override string FileName { get; } = "04-02";
    public override void Solve(string[] input)
    {
        var list = new List<(int number, int points)>();
        for (var c = 0; c < input.Length; c++)
        {
            var line = input[c];
            var parts = line.Split(":");
            var cardValues = parts[1].Split("|");
            var winingNumbers = new HashSet<int>(cardValues[0].Trim().Split(" ").Where(x => x is not (" " or "")).Select(int.Parse).ToArray());
            var card2 = cardValues[1].Trim().Split(" ").Where(x => x is not (" " or "")).Select(int.Parse).ToArray();

            var wins = card2.Count(x => winingNumbers.Contains(x));
            list.Add((c, wins));
        }

        var answer = 0;
        for (var i = 0; i < list.Count; i++)
        {
            visit(i);    
        }
        
        Console.WriteLine(answer);

        void visit(int pos)
        {
            if (pos >= list.Count) return;
            answer++;
            var (_, points) = list[pos];
            for (var i = 1; i <= points; i++)
            {
                visit(pos + i);
            }
        }
    }
}