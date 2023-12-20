namespace aoc_2023.Solvers;

public class SolverDay20 : Solver
{
    [PuzzleInput("20-02")]
    public override void Solve(string[] input)
    {
        var graph = new Dictionary<string, List<string>>();
        var conjunctions = new Dictionary<string, Dictionary<string ,bool>>();
        var flipFlops = new Dictionary<string, bool>();

        foreach (var line in input)
        {
            var parts = line.Split(" -> ");
            var machine = parts[0];
            var connections = parts[1].Split(", ");
            var sanitizedName = machine[0] is '%' or '&' ? machine[1..] : machine;
            
            switch (machine[0])
            {
                case '%':
                    flipFlops.TryAdd(sanitizedName, false); // off
                    break;
                case '&':
                    conjunctions.Add(sanitizedName, []);
                    break;
            }
            
            graph.TryAdd(sanitizedName, []);
            foreach (var connection in connections)
            {
                graph[sanitizedName].Add(connection);
            }
        }
        
        foreach (var key in graph.Keys)
        {
            foreach (var next in graph[key].Where(next => conjunctions.ContainsKey(next)))
            {
                conjunctions[next].TryAdd(key, true); // low
            }
        }

        var (informationCollected, buttonPressed) = (false, 0L);
        var memory = conjunctions["nc"]
            .ToDictionary<KeyValuePair<string, bool>, string, long>(inputs => inputs.Key, inputs => 0);
        
        while (!informationCollected)
        {
            buttonPressed++;
            sendSignal();
        }

        Console.WriteLine($": {memory.Values.Aggregate(Lcm)}");

        void sendSignal()
        {
            var q = new Queue<(string target, string sender, bool isLow)>();
            q.Enqueue(("broadcaster", "button", true));

            while (q.Count > 0)
            {
                var (target,sender, isLow) = q.Dequeue();
                if (target is "broadcaster")
                {
                    foreach (var next in graph[target])
                    {
                        q.Enqueue((next, target, isLow));
                    }
                }
                else if (flipFlops.TryGetValue(target, out var isOn))
                {
                    if (!isOn && !isLow) continue;
                    if (!isLow) continue;

                    flipFlops[target] = !flipFlops[target];
                    foreach (var next in graph[target])
                    {
                        q.Enqueue((next, target, !flipFlops[target]));
                    }
                }
                else if (conjunctions.TryGetValue(target, out var machine))
                {
                    machine[sender] = isLow;

                    if (target is "nc" && !isLow)
                    {
                        if (memory[sender] is 0 ) memory[sender] = buttonPressed;
                        if (memory.All(x => x.Value != 0)) informationCollected = true;
                    }
                    
                    var signal = machine.Values.All(x => x == false);
                    foreach (var next in graph[target])
                    {
                        q.Enqueue((next, target, signal));
                    }
                }
            }
        }

        long Lcm (long a, long b) => Math.Abs(a * b) / Gcd(a, b);
        long Gcd (long a, long b) => b == 0 ? a : Gcd(b, a % b);
    }
}

