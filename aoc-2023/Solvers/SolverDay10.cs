namespace aoc_2023.Solvers;

public class SolverDay10 : Solver
{
    public override string FileName => "10-01";

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
                gridCopy[r][c] = input[r][c];
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
        
        //print gridCopy
         for (var r = 0; r < input.Length; r++)
         {
             Console.WriteLine($"{string.Join("", gridCopy[r])}");
         }
        
        long visit((int r, int c) pos)
        {
            var visited = new HashSet<(int r, int c)>();
            var queue = new Queue<(int r, int c)>();

            visited.Add(pos);
            
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
                
                queue.Enqueue((nr, nc));
            }
            
            
            
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
                        var nr = r + dr;
                        var nc = c + dc;
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