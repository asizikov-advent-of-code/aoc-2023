using System.Diagnostics;

namespace aoc_2023.Solvers;

[PuzzleSolver("14-02", 14)]
public class SolverDay14 : Solver
{
    public override void Solve(string[] input)
    {
        var platform = input.Select(l => l.ToCharArray()).ToArray();
        var (width, height) = (platform[0].Length, platform.Length);
        tilt();
        
        var answer = 0;
        for (var r = 0; r < height; r++)
        {
            for (var c = 0; c < width; c++)
            {
                if (platform[r][c] == 'O')
                {
                    answer += height - r;
                }
            }
            
        }
        
        
        Console.WriteLine($"Answer: {answer}");

        void tilt()
        {
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
        }
    }
}