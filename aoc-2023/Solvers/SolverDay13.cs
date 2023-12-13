namespace aoc_2023.Solvers;

[PuzzleSolver("13-02", 13)]
public class SolverDay13 : Solver
{
    public override void Solve(string[] input)
    {
        var answer = 0;
        var pattern = new List<string>();
        foreach (var line in input)
        {
            if (line is "")
            {
                processPattern();
                pattern = new List<string>();
            }
            else
            {
                pattern.Add(line);
            }
        }
        processPattern();
        
        Console.WriteLine($"Answer: {answer}");


        void processPattern()
        {
            var mistakes = new int [pattern[0].Length-1];
            for (var c = 0; c < pattern[0].Length-1; c++)
            {
                foreach (var t in pattern)
                {
                    var (left, right) = (c, c + 1);
                    while (left >= 0 && right < pattern[0].Length)
                    {
                        if (t[left--] != t[right++])
                        {
                            mistakes[c]++;
                        }
                    }
                }
            }
            
            var verticalMistakes = new int [pattern.Count-1];
            for (var r = 0; r < pattern.Count-1; r++)
            {
                for (var c = 0; c < pattern[0].Length; c++)
                {
                    var (top, bottom) = (r, r + 1);
                    while (top >= 0 && bottom < pattern.Count)
                    {
                        if (pattern[top--][c] != pattern[bottom++][c])
                        {
                            verticalMistakes[r]++;
                        }
                    }
                }
            }
            
            for (var c = 0; c < pattern[0].Length-1; c++)
            {
                if (mistakes[c] != 1) continue;
                answer += c + 1;
                return;
            }
            
            for (var r = 0; r < pattern.Count-1; r++)
            {
                if (verticalMistakes[r] != 1) continue;
                answer += (r + 1) * 100;
                return;
            }
        }
    }
}