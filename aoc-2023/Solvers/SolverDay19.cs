namespace aoc_2023.Solvers;

public class SolverDay19 : Solver
{
    [PuzzleInput("19-02")]
    public override void Solve(string[] input)
    {
        var isParts = false;
        var workflows = new Dictionary<string, string[]>();
        var parts = new List<Dictionary<char, int>>();
        foreach (var line in input)
        {
            if (line == "") isParts = true;
            else if (!isParts)
            {
                var key = line[..line.IndexOf('{')];
                var instructions = line[(line.IndexOf('{') + 1)..^1];
                var rules = instructions.Split(',');
                workflows.Add(key, rules);
            }
            else parts.Add(FromString(line));
        }
        var answer = 0;

        foreach (var part in parts)
        {
            Console.WriteLine("Processing part: " + string.Join(",", part.Select(kv => $"{kv.Key} {kv.Value}")));
            var workflowName = "in";
            var processed = false;
            while (!processed)
            {
                Console.WriteLine("Workflow: " + workflowName);
                var workflow = workflows[workflowName];
                for (var i = 0; i < workflow.Length; i++)
                {
                    var rule = workflow[i];
                    var (applied, next) = process(rule, part);
                    if (applied)
                    {
                        if (next is "R" or "A")
                        {
                            processed = true;
                            if (next == "A") answer += part.Select(kv => kv.Value).Sum();
                            break;
                        }
                        workflowName = next;
                        break;
                    }
                }
            }
        }

        Console.WriteLine("Answer: " + answer);

        (bool applied, string next) process(string rule, Dictionary<char ,int> part)
        {
            if (rule.Contains(':'))
            {
                var separator = rule.IndexOf(':');
                var key = rule[0];
                var op = rule[1];
                var dest = rule[(separator + 1)..];
                var value = int.Parse(rule[2..separator]);
                switch (op)
                {
                    case '<' :
                        if (part[key] < value) return (true, dest);
                        break;
                    case '>' :
                        if (part[key] > value) return (true, dest);
                        break;
                }
                        
            }
            else return (true, rule);
            return (false, "");
        }
    }
    
    private static Dictionary<char, int> FromString(string line)
    {
        return line[1..^1].Split(',')
            .Select(x => x.Split('='))
            .ToDictionary(x => x[0][0], x => int.Parse(x[1]));
    }
}