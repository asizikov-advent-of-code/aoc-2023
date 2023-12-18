using aoc_2023.Extensions;

namespace aoc_2023.Solvers;

public class SolverDay18 : Solver
{
    [PuzzleInput("18-02")]
    public override void Solve(string[] input)
    {
        var answer = 0;

        var edges = new HashSet<(int r, int c)>(new[] { (0, 0) });
        
        var coordinates = new List<(int r, int c)>
        {
            (0, 0)
        };
        
        foreach (var instruction in input)
        {
            // line format: R 6 (#70c710)
            var parts = instruction.Split(' ');
            var (operation, distance, color) = (parts[0], parts[1], parts[2]);
            
            for (var i = 0; i < int.Parse(distance); i++)
            {
                var last = coordinates.Last();
                var (r, c) = last;
                var newCoordinate = operation switch
                {
                    "R" => (r, c + 1),
                    "L" => (r, c - 1),
                    "U" => (r - 1, c),
                    "D" => (r + 1, c),
                    _ => throw new Exception("Unknown operation: " + operation)
                };
                coordinates.Add(newCoordinate);
                edges.Add(newCoordinate);
            }
        }


        
        Console.WriteLine(coordinates.Join());
        
        var top = coordinates.Max(c => c.r);
        var bottom = coordinates.Min(c => c.r);
        var left = coordinates.Min(c => c.c);
        var right = coordinates.Max(c => c.c);
        
        // normalize coordinates to 0,0
        coordinates = coordinates.Select(c => (c.r - bottom, c.c - left)).ToList();
        edges = edges.Select(c => (c.r - bottom, c.c - left)).ToHashSet();


        
        Console.WriteLine($"Top: {top}, bottom: {bottom}, left: {left}, right: {right}");
        
        var grid = new char[top - bottom + 1, right - left + 1];
        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (edges.Contains((r,c)))
                {
                    grid[r, c] = '#';
                }
                else
                {
                    grid[r, c] = '.';
                }
            }
        }
        
        var colored = new HashSet<(int r, int c)>();
        //top left corner
        floodFill((0, 0));
        //top right corner
        floodFill((0, grid.GetLength(1) - 1));
        //bottom left corner
        floodFill((grid.GetLength(0) - 1, 0));
        //bottom right corner
        floodFill((grid.GetLength(0) - 1, grid.GetLength(1) - 1));
        
        
        // print grid
        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (colored.Contains((r, c)))
                {   
                    Console.Write('*');
                }else Console.Write(grid[r, c]);
            }
            Console.WriteLine();
        }

        var girdArea = (top - bottom + 1) * (right - left + 1);
        answer = girdArea - colored.Count;
        
        Console.WriteLine($"Answer: {answer}");

        void floodFill((int r, int c) pos)
        {
            if (pos.c < 0 || pos.c >= grid.GetLength(1) || pos.r < 0 || pos.r >= grid.GetLength(0))
            {
                return;
            }
            if (grid[pos.r, pos.c] == '#')
            {
                return;
            }
            
            if (colored.Contains(pos))
            {
                return;
            }
            
            colored.Add(pos);

            foreach (var dir in new [] { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                floodFill((pos.r + dir.Item1, pos.c + dir.Item2));
            }
        }
    }
}