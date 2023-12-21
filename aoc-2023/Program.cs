using System.Diagnostics;
using aoc_2023;
using aoc_2023.Solvers;

var dayNumberOverride = "";
var symbols = new[] { "🎄", "☃️", "🎅🏻", "🎁", "🛷", "📅", "❄️", "☃️" };

var dayNumber = dayNumberOverride == "" ? DateTime.Now.Day.ToString("00") : dayNumberOverride;

var (solver, dataFileName) = SolversProvider.Get(dayNumber);

Console.WriteLine($"Processing {dataFileName} for day {dayNumber}");
Console.WriteLine(string.Concat(Enumerable.Repeat(symbols[ DateTime.Now.Day % symbols.Length], 20)));

var sw = Stopwatch.StartNew();

var input = PuzzleInputReader.ReadLines(dataFileName);
Console.WriteLine("Input loaded in " + sw.ElapsedMilliseconds + " ms");

sw.Restart();

solver.Solve(input);

Console.WriteLine("Elapsed: " + sw.ElapsedMilliseconds + " ms");
Console.WriteLine(string.Concat(Enumerable.Repeat(symbols[ (DateTime.Now.Day + 2) % symbols.Length], 20)));


