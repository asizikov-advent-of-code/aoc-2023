using System.Globalization;

namespace aoc_2023.Solvers;

public class SolverDay18 : ISolver
{
    [PuzzleInput("18-02")]
    public void Solve(string[] input)
    {
        var coordinates = new List<(long r, long c)>
        {
            (0, 0)
        };
        
        foreach (var instruction in input)
        {
            var parts = instruction.Split(' ');
            var encoded = parts[2].TrimStart('(').TrimEnd(')');
            
            var distance = long.Parse(encoded[1..^1], NumberStyles.HexNumber);
            
            var (r, c) = coordinates[^1];
 
            var newCoordinate =  encoded[^1] switch
            {
                '0' => (r, c + distance),
                '1' => (r + distance, c),
                '2' => (r, c - distance),
                '3' => (r - distance, c),
                _ => throw new Exception("Unknown operation: " + encoded[^1])
            };
            coordinates.Add(newCoordinate);
        }

        var (area, perimeter) = (0L, 0L);
        for (var i = 0; i < coordinates.Count; i++)
        {
            var (r1, c1) = coordinates[i];
            var (r2, c2) = coordinates[(i + 1) % coordinates.Count];
            var distance = Math.Abs(r1 - r2) + Math.Abs(c1 - c2);
            
            perimeter += distance;
            area += r1 * c2 - r2 * c1;
        }

        Console.WriteLine("Answer: " +(Math.Abs(area / 2) + perimeter/2  + 1)); 
    }
}