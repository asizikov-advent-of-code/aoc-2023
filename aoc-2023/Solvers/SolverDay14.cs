namespace aoc_2023.Solvers;

[PuzzleSolver("14-02", 14)]
public class SolverDay14 : Solver
{
    public override void Solve(string[] input)
    {
        var platform = input.Select(l => l.ToCharArray()).ToArray();
        var (width, height) = (platform[0].Length, platform.Length);

        var history = new List<int>();
        var memory = new Dictionary<int, List<int>>();
        for (var i = 0; i < 10000; i++)
        {
            tilt();
            var l = load();
            history.Add(l);
            memory.TryAdd(l, new List<int>());
            memory[l].Add(i);
        }

        
        for (int i = 0; i < 500; i++)
        {
            var indices = memory[history[i]];
            if (indices.Count == 1) continue;
            foreach (var index in memory.Keys)
            {
                if (1000000000 % index == 0)
                {
                    Console.WriteLine("Answer: " + index);
                    break;
                }
            }
        }
        
        int load()
        {
            var load = 0;
            for (var r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c++)
                {
                    if (platform[r][c] == 'O')
                    {
                        load += height - r;
                    }
                }
            }

            return load;
        }

        void tilt()
        {
            // tilt up
            for (var r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c++)
                {
                    var pos = (r, c);
                    while (pos.r < height && platform[pos.r][pos.c] == '.')
                    {
                        pos = (pos.r + 1, pos.c);
                    }

                    if (pos.r == height) continue;
                    if (platform[pos.r][pos.c] == '#') continue;
                    (platform[pos.r][pos.c], platform[r][c]) = (platform[r][c], platform[pos.r][pos.c]);
                }
            }
            
            // tilt left
            for (var c = 0; c < width; c++)
            {
                for (var r = 0; r < height; r++)
                {
                    var pos = (r, c);
                    while (pos.c < width && platform[pos.r][pos.c] == '.')
                    {
                        pos = (pos.r, pos.c + 1);
                    }

                    if (pos.c == width) continue;
                    if (platform[pos.r][pos.c] == '#') continue;
                    (platform[pos.r][pos.c], platform[r][c]) = (platform[r][c], platform[pos.r][pos.c]);
                }
            }
            
            // tilt down
            for (var r = height - 1; r >= 0; r--)
            {
                for (var c = 0; c < width; c++)
                {
                    var pos = (r, c);
                    while (pos.r >= 0 && platform[pos.r][pos.c] == '.')
                    {
                        pos = (pos.r - 1, pos.c);
                    }

                    if (pos.r == -1) continue;
                    if (platform[pos.r][pos.c] == '#') continue;
                    (platform[pos.r][pos.c], platform[r][c]) = (platform[r][c], platform[pos.r][pos.c]);
                }
            }
            
            // tilt right
            
            for (var c = width - 1; c >= 0; c--)
            {
                for (var r = 0; r < height; r++)
                {
                    var pos = (r, c);
                    while (pos.c >= 0 && platform[pos.r][pos.c] == '.')
                    {
                        pos = (pos.r, pos.c - 1);
                    }

                    if (pos.c == -1) continue;
                    if (platform[pos.r][pos.c] == '#') continue;
                    (platform[pos.r][pos.c], platform[r][c]) = (platform[r][c], platform[pos.r][pos.c]);
                }
            }
            
        }
    }
}