
namespace aoc_2023.Solvers;

public class SolverDay08 : Solver
{
    [PuzzleInput("08-02")]
    public override void Solve(string[] input)
    {
        var instructions = input[0].Trim();

        var map = new Dictionary<string, List<string>>();
        var camels = new List<string>();
        for (var i = 2; i < input.Length; i++)
        {
           var line = input[i].Trim();
           var parts = line.Split(" = ");
           var node = parts[0];
           var children = parts[1].Trim('(', ')').Split(", ");
           var (left, right ) = (children[0], children[1]);

           map.TryAdd(node, new List<string>());
           map[node] = new List<string> {left, right};
           if (node[^1] == 'A') camels.Add(node);
        }

        var (dir, steps) = (0, 0L);
        var periods = new Dictionary<string, long>();

        while (periods.Count != camels.Count)
        {
            var child = instructions[dir] == 'L' ? 0 : 1;
            for (var i = 0; i < camels.Count; i++)
            {
                camels[i] = map[camels[i]][child];;
                if (camels[i][^1] != 'Z') continue;
                if (!periods.ContainsKey(camels[i]))
                {
                    periods[camels[i]] = steps + 1;
                }
            }

            dir++;
            dir %= instructions.Length;
            steps++;
        }

        Console.WriteLine($"Periods: {string.Join(", ", periods.Select(kv => $"{kv.Key}: {kv.Value}"))}");

        Console.WriteLine($": {periods.Values.Aggregate(Lcm)}");

        long Lcm (long a, long b)
        {
            return Math.Abs(a * b) / Gcd(a, b);
        }

        long Gcd (long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}