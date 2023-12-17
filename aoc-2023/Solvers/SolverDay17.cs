namespace aoc_2023.Solvers;

public class SolverDay17 : Solver
{
    [PuzzleInput("17-02")]
    public override void Solve(string[] input)
    {
        var answer = int.MaxValue;
        var cost = new Dictionary<((int r, int c), (int dr, int dc), int steps), int>();

        visit((0,0),(-1,0));

        Console.WriteLine($"Answer: {answer}");

        bool isValid((int r, int c) p) 
            => p.r >= 0 && p.r < input.Length && p.c >= 0 && p.c < input[0].Length;

        void visit((int r, int c) pos, (int dr, int dc) direction) 
        {
            var queue = new PriorityQueue<((int r, int c), (int dr, int dc), int steps, int loss), int>();
            queue.Enqueue((pos, direction, -1, 0), 0);

            while (queue.Count > 0)
            {
                var (p, d, s, l) = queue.Dequeue();
                if (cost.ContainsKey((p,d,s))) continue;
                if (p.r == input.Length - 1 && p.c == input[0].Length - 1)
                {
                    answer = Math.Min(answer, l);
                }
                cost[(p,d,s)] = l;

                foreach (var (dr, dc) in new []{(d.dc, -d.dr),(-d.dc, d.dr), (d.dr, d.dc)} ) 
                {
                    var sameDir = d.dr == dr && d.dc == dc;
                    if (sameDir && s >= 2) continue;

                    (int r, int c) next = (p.r + dr, p.c + dc);
                    if (isValid(next))
                    {
                        var nextCost = l + (input[next.r][next.c] - '0');
                        var nextStep = sameDir ? (s + 1) : 0;
                        queue.Enqueue( ( next, (dr, dc), nextStep, nextCost), nextCost);
                    }
                }
            }
        }
    }
}
