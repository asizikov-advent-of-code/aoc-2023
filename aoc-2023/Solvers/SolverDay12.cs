namespace aoc_2023.Solvers;

[PuzzleSolver("12-02", 12)]
public class SolverDay12 : Solver
{

    public override void Solve(string[] lines)
    {
        var answer = 0L;
        foreach (var line in lines)
        {
            var tmp = line.Split(' ')[0];
            var stage = new Span<char>($"{tmp}?{tmp}?{tmp}?{tmp}?{tmp}.".ToCharArray());

            var pp = line.Split(' ')[1];
            var groups = $"{pp},{pp},{pp},{pp},{pp}".Split(",").Select(int.Parse).ToList();

            answer += CountWays(stage, groups, 0,new Dictionary<string, long>());
        }

        Console.WriteLine("Answer: " + answer);

        long CountWays(Span<char> stage, List<int> groups, int currentGroup, Dictionary<string, long> memo)
        {
            var key = new string(stage) + currentGroup;
            
            if (memo.ContainsKey(key)) return memo[key];
            if (currentGroup == groups.Count) return stage.Contains('#') ? 0 : 1;
            
            var currentGroupSize = groups[currentGroup];
            if (currentGroupSize > stage.Length) return memo[key] = 0;
            
            var pos = 0;
            while (pos < stage.Length && stage[pos] == '.') pos++;
            if (pos > 0) return CountWays(stage[pos..], groups, currentGroup, memo);
            
            for (; pos < stage.Length; pos++)
            {
                switch (stage[pos])
                {
                    case '#':
                    {
                        if (--currentGroupSize < 0)
                        {
                            return memo[key] = 0;
                        }

                        break;
                    }
                    case '.' when currentGroupSize == 0:
                    {
                        return CountWays(stage[pos..], groups, currentGroup + 1, memo);
                    }
                    case '.':
                        return 0;
                    default:
                    {
                        var count = 0L;
                        if (currentGroupSize == 0 || currentGroupSize == groups[currentGroup])
                        {
                            stage[pos] = '.';
                            count += CountWays(stage, groups, currentGroup, memo);
                        }
                        if (currentGroupSize > 0)
                        {
                            stage[pos] = '#';
                            count += CountWays(stage, groups, currentGroup, memo);
                        }
                        stage[pos] = '?';
                        return memo[key] = count;
                    }
                }
            }
            
            return 0;
        }
    }
}