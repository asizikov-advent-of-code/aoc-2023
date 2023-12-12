using System.Collections.Specialized;
using System.Text;

namespace aoc_2023.Solvers;

public class SolverDay12 : Solver
{
    public override string FileName => "12-01";
    
    public override void Solve(string[] input)
    {
        var answer = 0L;
        var tmp = 0;
        foreach (var line in input)
        {

            var parts = line.Split(' ');

            var sb = new StringBuilder();
            sb.Append(parts[0]);
            // for (var i = 0; i < 4; i++)
            // {
            //     sb.Append('?');
            //     sb.Append(parts[0]);
            // }

            var state = sb.ToString().ToCharArray();
            
            
            sb.Clear();
            sb.Append(parts[1]);
            // for (var i = 0; i < 4; i++)
            // {
            //     sb.Append(',');
            //     sb.Append(parts[1]);
            // }
            
            var groups = sb.ToString().Split(',').Select(int.Parse).ToArray();
            
            var memo = new Dictionary<string, long>();
            var count = CountWays(state, 0, 0, groups, memo, ref tmp);
            count = (long)Math.Pow(count, 4);
            answer += count;
            
            Console.WriteLine(parts[0] + " arrangements " + count);
            answer += count;
        }
        Console.WriteLine("Answer: " + answer);
    }

    private long CountWays(char[] state, int pos, int group, int[] groups, Dictionary<string, long> memo, ref int tmp)
    {
        if (pos == state.Length)
        {
            //Console.WriteLine(++tmp + "<) " + new string(state));
            var (_, valid) = CountGroupsFound();
            return !valid ? 0 : 1;
        }
        
        
        if (state[pos] != '?')
        {
            return CountWays(state, pos + 1, group, groups, memo, ref tmp);
        }

        var count = 0L;
        state[pos] = '.';
        count += CountWays(state, pos + 1, group, groups, memo, ref tmp);
        state[pos] = '#';
        count += CountWays(state, pos + 1, group, groups, memo, ref tmp);
        state[pos] = '?';
        return count;

        (List<int> groups, bool isValid) CountGroupsFound()
        {
            var groupsFound = new List<int>();
            for (var i = 0; i < state.Length; i++)
            {
                if (state[i] == '#')
                {
                    group++;
                }
                else
                {
                    if (group > 0)
                    {
                        groupsFound.Add(group);
                    }

                    group = 0;
                }
            }

            if (group > 0)
            {
                groupsFound.Add(group);
            }

            if (groupsFound.Count != groups.Length)
            {
                return (groupsFound, false);
            }

            return groupsFound.Where((t, i) => t != groups[i]).Any() ? (groupsFound, false) : (groupsFound, true);
        }
    }

    
}