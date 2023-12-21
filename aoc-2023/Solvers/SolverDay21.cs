namespace aoc_2023.Solvers;

public class SolverDay21 : ISolver
{
    [PuzzleInput("21-02")]
    public void Solve(string[] input)
    {
        var answer = 0L;
        for (var r = 0; r < input.Length; r++)
        {
            for (var c = 0; c < input[0].Length; c++)
            {
                if (input[r][c] != 'S') continue;
                visit((r, c));
            }
        }
        Console.WriteLine("Answer:" + answer);
        
        void visit((int r, int c) pos)
        {
            long queueSizeIncrement = 0;
            long lastDiff = 0;
            
            var directions = new (int dr, int dc)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
            var queue = new Queue<(int r, int c)>();
            var queueSizeHistory = new List<long> { 0L };
            
            queue.Enqueue(pos);

            var steps = 0L;
            while (queue.Count > 0)
            {
                steps++;
                var size = queue.Count;

                var unique = new HashSet<(int r, int c)>();
                while (size --> 0)
                {
                    var current = queue.Dequeue();

                    foreach (var dir in directions)
                    {
                        (int r, int c) next = (current.r + dir.dr, current.c + dir.dc);
                        if (input[(next.r % input.Length + input.Length) % input.Length][(next.c % input[0].Length + input[0].Length) % input[0].Length] == '#') continue;
                        unique.Add(next);
                    }
                }
                foreach (var next in unique) queue.Enqueue(next);

                if (steps % 100 == 0) 
                {
                    Console.WriteLine(steps + " " + queue.Count);
                }
                
                if (steps % 131 == 65)
                {
                    var queueSizeDiff = queue.Count - queueSizeHistory[^1];
                    var queueSizeGrowth = queueSizeDiff - queueSizeIncrement;
                    var delta = queueSizeGrowth - lastDiff;

                    queueSizeHistory.Add(queue.Count);
                    
                    queueSizeIncrement = queueSizeDiff;
                    lastDiff = queueSizeGrowth;

                    if (delta is 0) break;
                }
            }
            
            while (steps < 26501365) 
            {
                queueSizeIncrement += lastDiff;
                queueSizeHistory.Add(queueSizeHistory[^1] + queueSizeIncrement);
                steps += 131;
            }

            answer = queueSizeHistory[^1];
        }
    }
}