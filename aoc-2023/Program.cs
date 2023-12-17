using System.Diagnostics;
using aoc_2023;
using aoc_2023.Solvers;

var dayNumberOverride = "";
var dayNumber = dayNumberOverride == "" ? DateTime.Now.Day.ToString("00") : dayNumberOverride;

var (solver, dataFileName) = SolversProvider.Get(dayNumber);

var sw = Stopwatch.StartNew();

Console.WriteLine($"Processing {dataFileName} for day {dayNumber}");
Console.WriteLine("----------------");

var input = DataProvider.ReadLines(dataFileName);
Console.WriteLine("Input loaded in " + sw.ElapsedMilliseconds + " ms");
sw.Restart();

solver.Solve(input);

Console.WriteLine("Elapsed: " + sw.ElapsedMilliseconds + " ms");
Console.WriteLine("----------------");

