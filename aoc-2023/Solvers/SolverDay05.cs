namespace aoc_2023.Solvers;

public class SolverDay05 : Solver
{
    [PuzzleInput("05-01")]
    public override void Solve(string[] input)
    {
        var seedsToProcess = new List<(long start, long end)[]>(); 
        var (mapSource, mapTarget) = (0, 0);
        foreach (var line in input)
        {
            if (line is "")
            {
                for (var i = 0; i < seedsToProcess.Count; i++)
                {
                    if (seedsToProcess[i][mapTarget].start == 0)
                    {
                        seedsToProcess[i][mapTarget].start = seedsToProcess[i][mapSource].start;
                        seedsToProcess[i][mapTarget].end = seedsToProcess[i][mapSource].end;
                    }
                }
            }
            else if (line.StartsWith("seeds:"))
            {
                var seeds = line.Split(":")[1].Trim().Split(" ").Select(long.Parse).ToArray();
                for (var i = 0; i < seeds.Length -1; i+=2)
                {
                    var (seedRangeStart, seedRangeLength) = (seeds[i], seeds[i+1]);
                    seedsToProcess.Add(new (long start, long end)[] {(seedRangeStart, seedRangeStart + seedRangeLength - 1 ), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0,0)});
                }
                Console.WriteLine($"Seeds: {string.Join(", ", seedsToProcess.Select(s => s[0]))}");
            }
            else if (char.IsDigit(line[0]))
            {
                var mapping = line.Split(" ").Select(long.Parse).ToArray();
                var (target, source, size) = (mapping[0], mapping[1], mapping[2]);
                var (sourceStart, sourceEnd) = (source, source + size - 1);
                var (targetStart, targetEnd) = (target, target + size - 1);
                
                var tempSeeds = new List<(long start, long end)[]>();
                foreach (var seed in seedsToProcess)
                {
                    if (seed[mapSource].end < sourceStart || seed[mapSource].start > sourceEnd)
                    {
                        tempSeeds.Add(seed);
                        continue;
                    }
                    
                    if (seed[mapSource].start >= sourceStart && seed[mapSource].end <= sourceEnd)
                    {
                        seed[mapTarget].start = targetStart + seed[mapSource].start - sourceStart;
                        seed[mapTarget].end = targetStart + seed[mapSource].end - sourceStart;
                        tempSeeds.Add(seed);
                        continue;
                    }
                    
                    if (seed[mapSource].start < sourceStart)
                    {
                        var newSeed = seed.ToArray();
                        newSeed[mapSource].end = sourceStart - 1;
                        tempSeeds.Add(newSeed);
                    }
                    
                    if (seed[mapSource].end > sourceEnd)
                    {
                        var newSeed = seed.ToArray();
                        newSeed[mapSource].start = sourceEnd + 1;
                        tempSeeds.Add(newSeed);
                    }
                    
                    seed[mapTarget].start = targetStart + Math.Max(seed[mapSource].start, sourceStart) - sourceStart;
                    seed[mapTarget].end = targetStart + Math.Min(seed[mapSource].end, sourceEnd) - sourceStart;
                    tempSeeds.Add(seed);
 
                }
                seedsToProcess = tempSeeds;
            }
            else if (line.StartsWith("seed-to-soil map:")) (mapSource, mapTarget) = (0, 1);
            else if (line.StartsWith("soil-to-fertilizer map:")) (mapSource, mapTarget) = (1, 2);
            else if (line.StartsWith("fertilizer-to-water map:")) (mapSource, mapTarget) = (2, 3);
            else if (line.StartsWith("water-to-light map:")) (mapSource, mapTarget) = (3, 4);
            else if (line.StartsWith("light-to-temperature map:")) (mapSource, mapTarget) = (4, 5);
            else if (line.StartsWith("temperature-to-humidity map:")) (mapSource, mapTarget) = (5, 6);
            else if (line.StartsWith("humidity-to-location map:")) (mapSource, mapTarget) = (6, 7);
            else
                Console.WriteLine($"Unknown input: {line}");
            
            Console.WriteLine(line);
        }

        for (var i = 0; i < seedsToProcess.Count; i++)
        {
            if (seedsToProcess[i][mapTarget].start == 0)
            {
                seedsToProcess[i][mapTarget].start = seedsToProcess[i][mapSource].start;
                seedsToProcess[i][mapTarget].end = seedsToProcess[i][mapSource].end;
            }
        }
        
        Console.WriteLine(seedsToProcess.Min(x => x[7].start));
    }
}