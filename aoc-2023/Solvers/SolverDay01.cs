namespace aoc_2023.Solvers;

public class SolverDay01 : Solver
{
    [PuzzleInput("01-02")]
    public override void Solve(string[] input)
    {
        var trie = new Trie();
        new [] {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"}
            .Select((w, i) => (w, i + 1))
            .ToList()
            .ForEach(t => trie.Add(t.w, t.Item2));
        
        var answer = input.Select(l => ToCalibrationValue(l.AsSpan())).Sum();
        
        Console.WriteLine($"Answer: {answer}");
        
        int ToCalibrationValue(ReadOnlySpan<char> line)
        {
            var value = 0;
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    value = line[i] - '0';
                    break;
                }
                
                var (val, found) = trie.Get(line[i..]);
                if (!found) continue;
                value = val;
                break;
            }
            
            for (var i = line.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    value = value * 10 + (line[i] - '0');
                    break;
                }
                var (val, found) = trie.Get(line[i..]);
                if (!found) continue;
                value = value * 10 + val;
                break;
            }
            Console.WriteLine("line: " + new string(line) + " value: " + value);
            return value;
        }
    }
    
    private class Trie
    {
        private readonly Node root = new Node();
        
        public void Add(string word, int value)
        {
            var node = root;
            foreach (var c in word)
            {
                node = node.GetOrAddChild(c);
            }
            node.Value = value;
        }
        
        public (int val, bool found) Get(ReadOnlySpan<char> word)
        {
            var node = root;
            foreach (var c in word)
            {
                node = node.GetChild(c);
                if (node == null)
                {
                    return (0, false);
                }
                if (node.Value is not null)
                {
                    return (node.Value.Value, true);
                }
            }
            return (0, false);
        }
        
        private class Node
        {
            private readonly Dictionary<char, Node> children = new();
            
            public int? Value { get; set; }
            
            public Node GetOrAddChild(char c)
            {
                if (!children.TryGetValue(c, out var child))
                {
                    child = new Node();
                    children.Add(c, child);
                }
                return child;
            }
            
            public Node? GetChild(char c)
            {
                children.TryGetValue(c, out var child);
                return child;
            }
        }
    }
}