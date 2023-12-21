using System.Linq.Expressions;

namespace aoc_2023.Solvers;

public class SolverDay21 : ISolver
{
    [PuzzleInput("21-02")]
    public void Solve(string[] input)
    {

        var answer = 0;
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
            var directions = new (int dr, int dc)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
            var queue = new Queue<(int r, int c)>();
            queue.Enqueue(pos);
            
            var steps = 0;
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
                        if (next.r < 0 || next.r >= input.Length || next.c < 0 || next.c >= input[0].Length) continue;
                        if (input[next.r][next.c] == '#') continue;
                        unique.Add(next);
                    }
                }
                foreach (var next in unique)
                {
                    queue.Enqueue(next);
                }

                Console.WriteLine("Steps: " + steps + " Size: " + unique.Count);
                if (steps == 64)
                {
                    answer = unique.Count;
                    return;
                }
            }
        }
    }
}