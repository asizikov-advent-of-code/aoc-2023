using System.Security.Cryptography.X509Certificates;

namespace aoc_2023.Solvers;

public class SolverDay15 : Solver
{
    [PuzzleInput("15-02")]
    public override void Solve(string[] input)
    {
        var parts = input[0].Split(',');

        var boxes = new List<Lense>[256];
        for (var i = 0; i < 256; boxes[i++] = new());

        foreach (var part in parts)
        {
            var (box, pos, label) = (0, 0, "");
            for (; pos < part.Length; pos++)
            {
                var c = part[pos];
                if (c is  '=' or '-') break;
                box += (int)c;
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
                    boxes[box].Add(new Lense {Tag = label, Val = part[^1] - '0'});
                    break;
                case '=':
                    boxes[box][index].Val = part[^1] - '0';
                    break;
                default:
                    break;
            }
        }

        var answer = 0;
        for (var i = 0; i < 256; i++) 
        {
            if (boxes[i].Count == 0) continue;
            for (var j = 0; j < boxes[i].Count; j++) 
            {
                answer += (i+1) * (j + 1) * boxes[i][j].Val;
            }
        }
       Console.WriteLine("Answer: " + answer);
    }

    class Lense
    {
        public string Tag {get;set;} = string.Empty;
        public int Val {get;set;}
    };
}