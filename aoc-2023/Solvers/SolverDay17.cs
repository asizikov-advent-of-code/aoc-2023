namespace aoc_2023.Solvers;

public class SolverDay17 : ISolver
{
    [PuzzleInput("17-02")]
    public void Solve(string[] input)
    {
        var answer = int.MaxValue;
        var cost = new Dictionary<((int r, int c), (int dr, int dc), int steps), int>();

        process((0,0));

        Console.WriteLine($"Answer: {answer}");

        bool isValid((int r, int c) p)
            => p.r >= 0 && p.r < input.Length && p.c >= 0 && p.c < input[0].Length;

        void process((int r, int c) pos)
        {
            var queue = new PriorityQueue<((int r, int c), (int dr, int dc), int steps, int loss), int>();
            queue.Enqueue((pos, (1,0), -1, 0), 0);

            while (queue.Count > 0)
            {
                var (p, d, s, l) = queue.Dequeue();
                if (cost.ContainsKey((p,d,s))) continue;

                if (p.r == input.Length - 1 && p.c == input[0].Length - 1)
                {
                    answer = Math.Min(answer, l);
                }
                cost[(p,d,s)] = l;

                foreach (var (dr, dc) in new [] { (d.dr, d.dc), (d.dc, -d.dr), (-d.dc, d.dr) } )
                {
                    var sameDir = d.dr == dr && d.dc == dc;
                    if (sameDir && s >= 10) continue;

                    var steps = sameDir ? 1 : 4;
                    (int r, int c) next =  (p.r + dr * steps, p.c + dc * steps);

                    if (isValid(next))
                    {
                        var nextCost = l;
                        for (var i = 1; i <= steps; i++)
                        {
                            nextCost += input[p.r + dr * i][p.c + dc * i] - '0';
                        }

                        queue.Enqueue( (next, (dr, dc), sameDir ? s + 1 : 4, nextCost), nextCost);
                    }
                }
            }
        }
    }
}
