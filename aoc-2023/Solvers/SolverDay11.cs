namespace aoc_2023.Solvers;

public class SolverDay11 : ISolver
{
    [PuzzleInput("11-01")]
    public void Solve(string[] input)
    {
        var (galaxies, graph) = ToGraph(input);
        var distances = new Dictionary<Node, Dictionary<Node, long>>();
        foreach (var galaxy in galaxies)
        {
            var cost = FindDistances(galaxy);
            distances.Add(galaxy, cost);
        }

        var pairs = new List<(Node g, long dist)>();
        var galaxiesList = galaxies.ToList();
        for (var i = 0; i < galaxiesList.Count; i++)
        {
            for (var j = i + 1; j < galaxiesList.Count; j++)
            {
                pairs.Add((galaxiesList[i], distances[galaxiesList[i]][galaxiesList[j]]));
            }
        }

        Console.WriteLine("Sum: " + pairs.Sum(p => p.dist));

        Dictionary<Node, long> FindDistances(Node galaxy)
        {
            var queue = new PriorityQueue<Node, long>();
            var distances = graph.ToDictionary<Node, Node, long>(nodes => nodes, nodes => int.MaxValue);

            distances[galaxy] = 0;
            queue.Enqueue(galaxy, 0L);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                foreach (var (neighbour, cost) in current.Neighbours)
                {
                    if (neighbour is null) continue;
                    var alt = distances[current] + cost;
                    if (alt < distances[neighbour])
                    {
                        distances[neighbour] = alt;
                        queue.Enqueue(neighbour, alt);
                    }
                }
            }

            return distances;
        }
    }

    private (HashSet<Node> galaxies, List<Node> graph) ToGraph(string[] input)
    {
        var directions = new (int r, int c)[] {(1, 0), (-1, 0), (0, 1), (0, -1)};
        var graph = new List<Node>();
        var coordToNode = new Dictionary<(int r, int c), Node>();

        var galaxies = new HashSet<Node>();
        var (rowsToExpand, colsToExpand) = (
            new HashSet<int>(Enumerable.Range(0, input.Length)),
            new HashSet<int>(Enumerable.Range(0, input[0].Length))
            );

        for (var r = 0; r < input.Length; r++)
        {
            for (var c = 0; c < input[0].Length; c++)
            {
                var node = new Node((r, c), input[r][c] == '#');
                coordToNode.Add((r, c), node);
                graph.Add(node);
                if (!node.IsGalaxy) continue;

                galaxies.Add(node);
                rowsToExpand.Remove(r);
                colsToExpand.Remove(c);
            }
        }

        foreach (var node in graph)
        {
            foreach (var dir in directions)
            {
                (int r, int c) next = (node.Position.r + dir.r, node.Position.c + dir.c);
                if (next.r < 0 || next.r >= input.Length || next.c < 0 || next.c >= input[0].Length)
                {
                    node.Neighbours.Add((null, 0));
                    continue;
                }
                var cost = 0;
                const int mult = 1000_000;
                if (rowsToExpand.Contains(next.r)) cost += mult;
                if (colsToExpand.Contains(next.c)) cost += mult;
                node.Neighbours.Add((coordToNode[next], cost == 0 ? 1 : cost));
            }
        }

        return (galaxies, graph);
    }

    private class Node((int r, int c) position, bool isGalaxy)
    {
        public (int r, int c) Position { get; set; } = position;
        public bool IsGalaxy { get; set; } = isGalaxy;
        public List<(Node? n, int cost)> Neighbours { get; set; } = new(4);
    }
}