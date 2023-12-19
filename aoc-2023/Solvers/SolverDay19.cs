namespace aoc_2023.Solvers;

public class SolverDay19 : Solver
{
    [PuzzleInput("19-02")]
    public override void Solve(string[] input)
    {
        var workflows = new Dictionary<string, string[]>();
        foreach (var line in input)
        {
            if (line == "") break;
            var key = line[..line.IndexOf('{')];
            var instructions = line[(line.IndexOf('{') + 1)..^1];
            var rules = instructions.Split(',');
            workflows.Add(key, rules);
        }
        var answer = 0L;

        simulate();

        Console.WriteLine("Answer: " + answer);

        void simulate()
        {
            var q = new Queue<(string wf, Dictionary<char, (int l, int r)>)>();
            q.Enqueue(("in", new Dictionary<char, (int l, int r)>
            {
                ['x'] = (1,4000),
                ['m'] = (1,4000),
                ['a'] = (1,4000),
                ['s'] = (1,4000)
            }));

            while (q.Count > 0)
            {
                var (workflow, parts) = q.Dequeue();

                switch (workflow)
                {
                    case "R":
                        continue;
                    case "A":
                        answer += parts.Aggregate(1L, (current, part) => current * (part.Value.r - part.Value.l + 1));
                        continue;
                }

                foreach (var rule in workflows[workflow])
                {
                    var results = process(rule, parts, workflow);
                    foreach (var result in results)
                    {
                        if (result.applied) q.Enqueue((result.next, result.slice));
                        else parts = result.slice;
                    }
                }
            }
        }

        List<(Dictionary<char, (int l, int r)> slice, string next, bool applied)> process(
            string rule,
            Dictionary<char, (int l, int r)> part,
            string workflow)
        {
            var left = new Dictionary<char, (int l, int r)>(part);
            var right = new Dictionary<char, (int l, int r)>(part);

            if (!rule.Contains(':')) return [(left, rule, true)];

            var separator = rule.IndexOf(':');
            var (key, op, dest, value) = (rule[0], rule[1], rule[(separator + 1)..], int.Parse(rule[2..separator]));

            var (l, r) = part[key];
            switch (op)
            {
                case '<' :
                    if (l > value) return [(left, workflow, false)];

                    left[key] = (left[key].l, value - 1);
                    right[key] = (value, right[key].r);
                    return
                    [
                        (left, dest, true),
                        (right, workflow, false)
                    ];
                case '>' :
                    if (r < value) return [(left, workflow, false)];

                    left[key] = (left[key].l, value);
                    right[key] = (value + 1, right[key].r);
                    return
                    [
                        (left, workflow, false),
                        (right, dest, true)
                    ];
            }
            return [(left, rule, true)];
        }
    }
}