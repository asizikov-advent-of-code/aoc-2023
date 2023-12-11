namespace aoc_2023.Solvers;

public class SolverDay11 : Solver
{
    public override string FileName => "11-02";

    public override void Solve(string[] input)
    {
        var (galaxies, coordToNode) = ToGraph(input);

        var distances = new Dictionary<(int r, int c), Dictionary<(int r, int c), long>>();
        foreach (var galaxy in galaxies)
        {
            var cost = FindDistances(galaxy);
            distances.Add(galaxy, cost);
        }
        
        var processed = new HashSet<(int r, int c)>();
        var pairs = new List<((int r, int c) g, long dist)>();
        foreach (var galaxy in galaxies)
        {
            foreach (var other in galaxies)
            {
                if (galaxy == other) continue;
                if (processed.Contains(other)) continue;
                pairs.Add((galaxy, distances[galaxy][other]));
            }
        
            processed.Add(galaxy);
        }
        
        Console.WriteLine("Sum: " + pairs.Sum(p => p.dist));

        Dictionary<(int r, int c), long> FindDistances((int r, int c) galaxy)
        {
            var queue = new PriorityQueue<Node, long>();
            var distances = coordToNode.Values.ToDictionary<Node, (int r, int c), long>(nodes => nodes.Position, nodes => int.MaxValue);

            distances[galaxy] = 0;
            queue.Enqueue(coordToNode[galaxy], 0L);
            
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                foreach (var (neighbour, cost) in current.Neighbours)
                {
                    if (neighbour == null) continue;
                    var alt = distances[current.Position] + cost;
                    if (alt < distances[neighbour.Position])
                    {
                        distances[neighbour.Position] = alt;
                        queue.Enqueue(neighbour, alt);
                    }
                }
            }

            return distances;
        }
    }

    private (HashSet<(int r, int c)> galaxies, Dictionary<(int r, int c), Node> coordToNode) ToGraph(string[] input)
    {
        var directions = new (int r, int c)[] {(1, 0), (-1, 0), (0, 1), (0, -1)};
        var coordToNode = new Dictionary<(int r, int c), Node>();
        var rowsToExpand = new HashSet<int>();
        var colsToExpand = new HashSet<int>();
        
        var galaxies = new HashSet<(int r, int c)>();
        
        for (var r = 0; r < input.Length; r++)
        {
            for (var c = 0; c < input[0].Length; c++)
            {
                var node = new Node
                {
                    Position = (r, c),
                    IsGalaxy = input[r][c] == '#'
                };
                coordToNode.Add((r, c), node);
                if (node.IsGalaxy) galaxies.Add((r, c));
            }
        }
        
        for (var c = 0; c < input[0].Length; c++)
        {
            var shouldExpand = true;
            foreach (var t in input)
            {
                if (t[c] == '#') shouldExpand = false;
            }
            if (shouldExpand) colsToExpand.Add(c);
        }
        
        for (var r = 0; r < input.Length; r++)
        {
            var shouldExpand = input[r].All(x => x != '#');
            if (shouldExpand) rowsToExpand.Add(r);
        }
        
        
        foreach (var node in coordToNode.Values)
        {
            var neighbours = new List<(Node? n, int cost)>();
            foreach (var dir in directions)
            {
                (int r, int c) next = (node.Position.r + dir.r, node.Position.c + dir.c);
                if (next.r < 0 || next.r >= input.Length || next.c < 0 || next.c >= input[0].Length)
                {
                    neighbours.Add((null, 0));
                    continue;
                }
                var cost = 0;
                const int mult = 1000_000;
                if (rowsToExpand.Contains(next.r)) cost += mult;
                if (colsToExpand.Contains(next.c)) cost += mult;
                neighbours.Add((coordToNode[next], cost == 0 ? 1 : cost));
            }

            node.Neighbours = neighbours.ToArray();
        }

        return (galaxies, coordToNode);
    }
    
    private class Node
    {
        public (int r, int c) Position { get; set; }
        public bool IsGalaxy { get; set; }
        public (Node? n, int cost)[] Neighbours { get; set; } = new (Node? n, int cost)[4];
    }
}