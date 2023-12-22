namespace aoc_2023.Solvers;

public class SolverDay22 : ISolver
{
    [PuzzleInput("22-02")]
    public void Solve(string[] input)
    {
        var bricks = new List<((int x, int y, int z) end1, (int x, int y, int z) end2)>();
        foreach (var line in input)
        {
            //1,0,1~1,2,1
            var parts = line.Split('~');
            var end1 = parts[0].Split(',').Select(int.Parse);
            var end2 = parts[1].Split(',').Select(int.Parse);
             (int x, int y, int z) t = (end1.ElementAt(0), end1.ElementAt(1), end1.ElementAt(2));
             (int x, int y, int z) t2 = (end2.ElementAt(0), end2.ElementAt(1), end2.ElementAt(2));
             bricks.Add((t, t2));
        }

    }
}