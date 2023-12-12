namespace aoc_2023.Solvers;

[PuzzleSolver("03-01", 3)]
public class SolverDay03 : Solver
{
    public override void Solve(string[] input)
    {
        var dirs = new (int dr, int dc)[]
        {
            (0, 1), (1, 0), (0, -1), (-1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1)
        };

        var gears = new Dictionary<(int r, int c), List<int>>();
        
        for (var r = 0; r < input.Length; r++)
        {
            for (var c = 0; c < input[r].Length;)
            {
                if (!char.IsDigit(input[r][c]))
                {
                    c++;
                    continue;
                }

                var (num, start, end) = (0, c, c);
                while (end < input[r].Length && char.IsDigit(input[r][end]))
                {
                    num = num * 10 + input[r][c] - '0';
                    end++;
                }
                c = end;

                var adjustedGears = new HashSet<(int dr, int dc)>();
                for (var i = start; i < end; i++)
                {
                    var res = Adjusted(r, i);
                    if (!res.adjusted) continue;
                    foreach (var adjustedGear in res.gears)
                    {
                        adjustedGears.Add(adjustedGear);
                    }
                }

                foreach (var gear in adjustedGears)
                {
                    gears.TryAdd(gear, new());
                    gears[gear].Add(num);
                }
            }
        }

        var answer = 0L;
        foreach (var gear in gears.Keys)
        {
            if (gears[gear].Count != 2) continue;
            var power = gears[gear][0] * gears[gear][1];
            answer += power;
        }
        
        Console.WriteLine(answer);

        (bool adjusted, List<(int sr, int sc)> gears) Adjusted(int r, int c)
        {
            var foundGears = new List<(int r, int c)>();
            foreach (var dir in dirs)
            {
                var (nr, nc) = (r + dir.dr, c + dir.dc);
                if (nr < 0 || nr >= input.Length || nc < 0 || nc >= input[0].Length) continue;
                if (input[nr][nc] == '*')
                {
                    foundGears.Add((nr, nc));
                }
            }

            return foundGears.Count != 0 ? 
                (true, foundGears) 
                : (false, new());
        }
    }
}