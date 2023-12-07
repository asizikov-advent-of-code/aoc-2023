using System.Security.Cryptography;
using System.Text;

namespace aoc_2023.Solvers;

public class SolverDay201502 : Solver
{
    public override string FileName { get; } = "2015-02-01";

    public override void Solve(string[] input)
    {
        var answer = 0;

        foreach (var str in input)
        {
            var (hasPair, hasLetterBetween) = (false, false);
            for (var i = 0; i < str.Length - 2; i++)
            {
                if (str[i] == str[i + 2]) hasLetterBetween = true;
                if (str.Substring(i + 2).Contains(str.Substring(i, 2))) hasPair = true;
                
            }

            if (hasPair && hasLetterBetween) answer++;
        }

        Console.WriteLine(answer);
    }
}