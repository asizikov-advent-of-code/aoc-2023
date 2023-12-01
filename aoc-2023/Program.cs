using aoc_2023;
using aoc_2023.Solvers;

var dayNumberOverride = "";
var dayNumber = dayNumberOverride == "" ? DateTime.Now.Day.ToString("00") : dayNumberOverride;

var solver = SolversProvider.Get(dayNumber);
var dataFileName = solver.FileName;

Console.WriteLine($"Processing {dataFileName} for day {dayNumber}");
Console.WriteLine("----------------");

var input = DataProvider.ReadLines(dataFileName);
solver.Solve(input);

Console.WriteLine("--------------");

