namespace aoc_2023.Solvers;

public class SolverDay15 : ISolver
{
    [PuzzleInput("15-02")]
    public void Solve(string[] input)
    {
        var parts = input[0].Split(',');

        var boxes = new List<Lens>[256];
        for (var i = 0; i < 256; boxes[i++] = []);

        foreach (var part in parts)
        {
            var (box, pos, label) = (0, 0, "");
            for (; pos < part.Length; pos++)
            {
                var c = part[pos];
                if (c is  '=' or '-') break;
                box += c;
                box *= 17;
                box %=256;
                label += c;
            }

            var index = boxes[box].FindIndex(p => p.Tag == label);

            switch (part[pos])
            {
                case '-' when index is not -1: 
                    boxes[box].RemoveAt(index);
                    break;
                case '=' when index is -1:
                    boxes[box].Add(new Lens {Tag = label, Val = part[^1] - '0'});
                    break;
                case '=':
                    boxes[box][index].Val = part[^1] - '0';
                    break;
            }
        }

        var answer = 0;
        for (var i = 0; i < 256; i++)
        {
            if (boxes[i].Count == 0) continue;
            for (var j = 0; j < boxes[i].Count; j++)
            {
                answer += (i + 1) * (j + 1) * boxes[i][j].Val;
            }
        }

        Console.WriteLine("Answer: " + answer);
    }

    class Lens
    {
        public string Tag {get; init;} = string.Empty;
        public int Val {get;set;}
    };
}