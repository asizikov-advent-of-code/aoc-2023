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
            for (var c = 0; c < pattern[0].Length-1; c++)
            {
                var foundMirror = true;
                foreach (var t in pattern)
                {
                    var (left, right) = (c, c + 1);
                    var reflects = true;
                    while (left >= 0 && right < pattern[0].Length)
                    {
                        if (t[left] != t[right])
                        {
                            reflects = false;
                            break;
                        }
                        left--;
                        right++;
                    }

                    if (reflects) continue;
                    foundMirror = false;
                    break;
                }

                if (!foundMirror) continue;
                answer += (c + 1);
                return;
            }

            for (var r = 0; r < pattern.Count-1; r++)
            {
                var foundMirror = true;
                
                for (var c = 0; c < pattern[0].Length; c++)
                {
                    var (top, bottom) = (r, r + 1);
                    var reflects = true;
                    while (top >= 0 && bottom < pattern.Count)
                    {
                        if (pattern[top][c] != pattern[bottom][c])
                        {
                            reflects = false;
                            break;
                        }
                        top--;
                        bottom++;
                    }

                    if (reflects) continue;
                    foundMirror = false;
                    break;
                }

                if (!foundMirror) continue;
                answer += (r + 1) * 100;
                return;
            }
        }
    }
}