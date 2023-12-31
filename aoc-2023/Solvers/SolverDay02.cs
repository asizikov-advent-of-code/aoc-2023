namespace aoc_2023.Solvers;

public class SolverDay02 : ISolver
{
    [PuzzleInput("02-02")]
    public void Solve(string[] input)
    {
        var answer = 0;
        foreach (var line in input)
        {
            var bag = new Dictionary<string, int>();
            foreach (var hand in line.Split(": ")[1].Split("; "))
            {
                foreach (var ball in hand.Split(", "))
                {
                    var pair = ball.Split(" ");
                    var (count, color) = (int.Parse(pair[0]),pair[1]);
                    bag.TryAdd(color, 0);
                    bag[color] = Math.Max(bag[color], count);
                }
            }
            answer += bag.Keys.Aggregate(1, (acc, color) => acc * bag[color]);
        }
        Console.WriteLine("Answer: " + answer);
    }
}