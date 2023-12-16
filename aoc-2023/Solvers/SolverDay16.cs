namespace aoc_2023.Solvers;

[PuzzleSolver("16-02", 16)]
public class SolverDay16 : Solver
{
    public override void Solve(string[] input)
    {
        //Console.WriteLine("Answer: " + CastBeam((0,-1),(0, 1)));

        var answer = 0;

        for (var r = 0; r < input.Length; r++)
        {
            answer = Math.Max(answer, CastBeam((r, -1), (0, 1)));
            answer = Math.Max(answer, CastBeam((r, input[0].Length), (0, -1)));
        }

        for (var c = 0; c < input[0].Length; c++)
        {
            answer = Math.Max(answer, CastBeam((-1, c), (1, 0)));
            answer = Math.Max(answer, CastBeam((input.Length, c), (-1, 0)));
        }

        Console.WriteLine("Answer: " + answer);

        int CastBeam((int r, int c) startPos, (int dr, int dc) startDir) 
        {
            var beams = new List<((int r, int c) pos, (int dr, int dc) dir)> {(startPos,startDir)};
            var energizedTiles = new HashSet<(int r, int c)>{(startPos.r + startDir.dr, startPos.c + startDir.dc)};

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
            return energizedTiles.Count;
        }
    }
}