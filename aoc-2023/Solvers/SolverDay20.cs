namespace aoc_2023.Solvers;

public class SolverDay20 : ISolver
{
    [PuzzleInput("20-02")]
    public void Solve(string[] input)
    {
        var graph = new Dictionary<string, string[]>();
        var (conjunctions, flipFlops) = (new Dictionary<string, Dictionary<string ,bool>>(),new Dictionary<string, bool>() );

        foreach (var line in input)
        {
            var parts = line.Split(" -> ");
            var (machine, connections) = (parts[0], parts[1].Split(", "));
            var sanitizedName = machine[0] is '%' or '&' ? machine[1..] : machine;

            if (machine[0] is '%') flipFlops.TryAdd(sanitizedName, false); // off
            else if (machine[0] is '&') conjunctions.Add(sanitizedName, []);

            graph.Add(sanitizedName, connections);
        }

        foreach (var key in graph.Keys)
        {
            foreach (var next in graph[key].Where(next => conjunctions.ContainsKey(next)))
            {
                conjunctions[next].Add(key, true); // low
            }
        }

        var (informationCollected, buttonPressed) = (false, 0L);
        var memory = conjunctions["nc"]
            .ToDictionary<KeyValuePair<string, bool>, string, long>(inputs => inputs.Key, inputs => 0);

        while (!informationCollected) sendSignal(++buttonPressed);

        Console.WriteLine($"Answer: {memory.Values.Aggregate(Lcm)}");

        void sendSignal(long iteration)
        {
            var q = new Queue<(string target, string sender, bool isLow)>();
            q.Enqueue(("broadcaster", "button", true));

            while (q.Count > 0)
            {
                var (target,sender, isLow) = q.Dequeue();

                if (target is "broadcaster") FanOut(target, isLow);
                else if (flipFlops.TryGetValue(target, out var isOn))
                {
                    if (!isOn && !isLow) continue;
                    if (!isLow) continue;

                    flipFlops[target] = !flipFlops[target];
                    FanOut(target, !flipFlops[target]);
                }
                else if (conjunctions.TryGetValue(target, out var machine))
                {
                    machine[sender] = isLow;

                    if (target is "nc" && !isLow)
                    {
                        if (memory[sender] is 0 ) memory[sender] = iteration;
                        if (memory.All(x => x.Value != 0)) informationCollected = true;
                    }

                    FanOut(target, machine.Values.All(x => !x));
                }

                void FanOut(string sender, bool signal) => Array.ForEach(graph[sender],next => q.Enqueue((next, sender, signal)));
            }
        }

        long Lcm (long a, long b) => Math.Abs(a * b) / Gcd(a, b);
        long Gcd (long a, long b) => b == 0 ? a : Gcd(b, a % b);
    }
}