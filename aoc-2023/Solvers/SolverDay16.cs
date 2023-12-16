namespace aoc_2023.Solvers;

[PuzzleSolver("16-02", 16)]
public class SolverDay16 : Solver
{
    public override void Solve(string[] input)
    {
        var energizedTiles = new HashSet<(int r, int c)>{(0, 0)};
        var beams = new List<((int r, int c) pos, (int dr, int dc) dir)> {((0,-1),(0,1))};

        var grid = new char[input.Length][];
        for (var i = 0; i < input.Length; i++)
        {
            grid[i] = input[i].ToCharArray();
        }
        grid[0][0] = '#';

        var complete = false;
        var visited = new HashSet<((int r, int c)pos, (int dr, int dc)dir)>();
        while (!complete) 
        {
            var newBeams = new List<((int r, int c) pos, (int dr, int dc) dir)>();
            foreach (var (pos, dir) in beams)
            {
                if (visited.Contains((pos,dir))) continue;
                visited.Add((pos,dir));

                var (nr, nc) = (pos.r + dir.dr, pos.c + dir.dc);
                if (nr < 0 || nr >= input.Length || nc < 0 || nc >= input[0].Length) continue;

                energizedTiles.Add((nr, nc));
                grid[nr][nc] = '#';

                switch (input[nr][nc])
                {
                    case '.':
                        newBeams.Add(((nr, nc), dir));
                        break;
                    case '|' when dir is (0, 1) or (0, -1):
                        newBeams.Add(((nr, nc), (1, 0)));
                        newBeams.Add(((nr, nc), (-1, 0)));
                        break;
                    case '|':
                        newBeams.Add(((nr, nc), dir));
                        break;
                    case '-' when dir is (1, 0) or (-1, 0):
                        newBeams.Add(((nr, nc), (0, 1)));
                        newBeams.Add(((nr, nc), (0, -1)));
                        break;
                    case '-':
                        newBeams.Add(((nr, nc), dir));
                        break;
                    case '/' when dir is (0, 1):
                        newBeams.Add(((nr, nc), (-1, 0)));
                        break;
                    case '/' when dir is (0, -1):
                        newBeams.Add(((nr, nc), (1, 0)));
                        break;
                    case '/' when dir is (1, 0):
                        newBeams.Add(((nr, nc), (0, -1)));
                        break;
                    case '/' when dir is (-1, 0):
                        newBeams.Add(((nr, nc), (0, 1)));
                        break;
                    case '\\' when dir is (0, 1):
                        newBeams.Add(((nr, nc), (1, 0)));
                        break;
                    case '\\' when dir is (0, -1):
                        newBeams.Add(((nr, nc), (-1, 0)));
                        break;
                    case '\\' when dir is (1, 0):
                        newBeams.Add(((nr, nc), (0, 1)));
                        break;
                    case '\\' when dir is (-1, 0):
                        newBeams.Add(((nr, nc), (0, -1)));
                        break;
                    default:
                        break;
                }
            }

            complete = newBeams.Count == 0;
            beams = newBeams;
        }
    
        foreach (var row in grid) 
        {
            Console.WriteLine(row);
        }

        Console.WriteLine("Answer: " + energizedTiles.Count);
    }
}