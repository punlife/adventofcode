// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using AdventOfCode2024;

var days = GetDayList();
Console.WriteLine("-----------------------------------------------Advent of Code:2024-----------------------------------------------");
foreach (var day in days)
{
    var inputData = GetTestDataPath(day.GetType().Name);
    Console.WriteLine($"------------------------------- {day.GetType().Name} -------------------------------");
    RunTask(day, inputData, true);
    RunTask(day, inputData, false);
}

static void RunTask(Day day, string[] inputData, bool task1)
{
    var stopwatchTask = Stopwatch.StartNew();
    var result = task1 ? day.RunTask1(inputData) : day.RunTask2(inputData);
    var stringTask = task1 ? "Task 1" : "Task 2";
    stopwatchTask.Stop();

    Console.WriteLine("{0} | Execution time: {1}\t | Result: {2}", stringTask, OutputTime(stopwatchTask.Elapsed), result);
}

static List<Day> GetDayList() => [new Day1(), new Day2(), new Day3(), new Day4()];

static string[] GetTestDataPath(string name)
{
    var path = Path.Combine(Environment.CurrentDirectory, @"Data2024", $"{name}.txt");
    return File.ReadAllLines(path);
}

static string OutputTime(TimeSpan duration)
{
    return $"{duration.TotalMilliseconds}ms";
}