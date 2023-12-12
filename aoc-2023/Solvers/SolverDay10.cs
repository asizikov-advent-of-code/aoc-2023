namespace aoc_2023.Solvers;

[PuzzleSolver("10-02", 10)]
public class SolverDay10 : Solver
{
    public override void Solve(string[] input)
    {
        var pipeToDirection = new Dictionary<char, (int dr, int dc)[]>()
        {
            ['|'] = new[] { (-1, 0), (1, 0) },
            ['-'] = new[] { (0, 1), (0, -1) },
            ['L'] = new[] { (-1, 0), (0, 1) },
            ['J'] = new[] { (-1, 0), (0, -1) },
            ['7'] = new[] { (0, -1), (1, 0) },
            ['F'] = new[] { (0, 1), (1, 0) },
        };
        
        var gridCopy = new char[input.Length][];
        for (var r = 0; r < input.Length; r++)
        {
            gridCopy[r] = new char[input[0].Length];
            for (var c = 0; c < input[0].Length; c++)
            {
                gridCopy[r][c] = '.';
            }
        }
        
        for (var r = 0; r < input.Length; r++)
        {
            for (var c = 0; c < input[0].Length; c++)
            {
                if (input[r][c] != 'S') continue;
                var answer = visit((r, c));
                Console.WriteLine(answer);
                break;
            }
        }
        
        
        var inside = 0;
         for (var r = 0; r < input.Length; r++)
         {
             for (var c = 0; c < input[0].Length; c++)
             {
                 if (gridCopy[r][c] != '.') continue;
                 
                 var intersections = rayCast((r, c));
                 gridCopy[r][c] = intersections % 2 != 0 ? 'I' : '.';
                 if (intersections % 2 != 0) inside++;
             }
         }

         Console.WriteLine(inside);
         
         void printGridCopy()
         {
             for (var r = 0; r < input.Length; r++)
             {
                 Console.WriteLine($"{string.Join("", gridCopy[r])}");
             }
         }
    
         int rayCast((int r, int c) tile)
         {
             var intersections = 0;
             var (nr, nc) = (tile.r, tile.c + 1);
             
             while (nr >= 0 && nr < input.Length && nc >= 0 && nc < input[0].Length)
             {
                 if (gridCopy[nr][nc] != 'X')
                 {
                     (nr, nc) = (nr + 0, nc + 1);
                     continue;
                 }

                 if (input[nr][nc] == '|') intersections++;
                 if (input[nr][nc] is 'F' or 'L')
                 {
                     var start = input[nr][nc];
                     var intersected = false;
                     for (var i = nc; i < input[0].Length; i++)
                     {
                         if (input[nr][i] == '7')
                         {
                             intersected = start == 'L';
                             break;
                         }

                         if (input[nr][i] == 'J')
                         {
                             intersected = start == 'F';
                             break;
                         }
                     }

                     if (intersected) intersections++;
                 }

                 (nr, nc) = (nr + 0, nc + 1);
             }

             return intersections;
         }
        
        long visit((int r, int c) pos)
        {
            var visited = new HashSet<(int r, int c)>();
            var queue = new Queue<(int r, int c)>();
    
            visited.Add(pos);
    
            var possibleMoves = new[] { 0, 0, 0, 0 }; // down, up, right, left
            foreach (var dir in new (int dr, int dc)[] { (0, 1), (1, 0), (-1, 0), (0, -1) })
            {
                var (nr, nc) = (pos.r + dir.dr, pos.c + dir.dc);
                if (nr < 0 || nr >= input.Length || nc < 0 || nc >= input[0].Length) continue;
                if (input[nr][nc] == '.') continue;
                
                // left
                if (dir is (0, -1) && input[nr][nc] is '|' or 'J' or '7') continue;
                // right
                if (dir is (0, 1) && input[nr][nc] is '|' or 'L' or 'F') continue;
                // up
                if (dir is (-1, 0) && input[nr][nc] is '-' or 'L' or 'J') continue;
                // down
                if (dir is (1, 0) && input[nr][nc] is '-' or 'F' or '7') continue;
                
                if (dir.dr == 1) possibleMoves[0] = 1;
                if (dir.dr == -1) possibleMoves[1] = 1;
                if (dir.dc == 1) possibleMoves[2] = 1;
                if (dir.dc == -1) possibleMoves[3] = 1;
                queue.Enqueue((nr, nc));
            }

            var pipe = possibleMoves switch
            {
                [1, 1, 0, 0] => '|',
                [0, 0, 1, 1] => '-',
                [1, 0, 0, 1] => '7',
                [0, 1, 1, 0] => 'L',
                [1, 0, 1, 0] => 'F',
                [0, 1, 0, 1] => 'J',
                _ => '?'
            };

            gridCopy[pos.r][pos.c] = 'X';
    
            var row = input[pos.r].ToCharArray();
            row[pos.c] = pipe;
            input[pos.r] = new string(row);
            
            var maxSteps = 1L;
            var steps = 1L;
            while (queue.Count > 0)
            {
                var levelSize = queue.Count;
                for (var i = 0; i < levelSize; i++)
                {
                    var (r, c) = queue.Dequeue();
                    if (visited.Contains((r, c))) continue;
                    visited.Add((r, c));
                    gridCopy[r][c] = 'X';
                    maxSteps = Math.Max(maxSteps, steps);
                    foreach (var (dr, dc) in pipeToDirection[input[r][c]])
                    {
                        var (nr, nc) = (r + dr, c + dc);
                        if (nr < 0 || nr >= input.Length || nc < 0 || nc >= input[0].Length) continue;
                        if (input[nr][nc] == '.') continue;
                        queue.Enqueue((nr, nc));
                    }
                }
                steps++;
            }
            
            return maxSteps;
        }
    }
}