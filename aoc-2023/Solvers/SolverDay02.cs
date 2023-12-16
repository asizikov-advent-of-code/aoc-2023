namespace aoc_2023.Solvers;

public class SolverDay02 : Solver
{
    [PuzzleInput("02-02")]
    public override void Solve(string[] input)
    {
        var bag = new Dictionary<string, int>
        {
            ["red"] = 12,
            ["green"] = 13,
            ["blue"] = 14
        };
        
        var answer = 0;
        for (var i = 0; i < input.Length; i++) 
        {
            var possibleGame = true;
            foreach (var hand in input[i].Split(": ")[1].Split("; "))
            {
                var possible = true;
                foreach (var ball in hand.Split(", "))
                {
                    var pair = ball.Split(" ");
                    var (count, color) = (int.Parse(pair[0]),pair[1]);
                    if (bag[color] >= count) continue;

                    possible = false;
                    break;
                }
                if (possible) continue;
                {
                    possibleGame = false;
                    break;
                }
            }
            if (possibleGame)
            {
                answer += i + 1;
            }
        }
        Console.WriteLine("Answer: " + answer);
    }
}