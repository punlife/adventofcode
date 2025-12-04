using System.Diagnostics;

namespace AdventOfCode2025;

class Program
{
    static void Main(string[] args)
    {
        var days = GetDayList();
        Console.WriteLine("[-----------------------------------------------Advent of Code:2025-----------------------------------------------]");
        foreach (var day in days)
        {
            var inputData = GetTestDataPath(day.GetType().Name);
            Console.WriteLine($"------------------------------- {day.GetType().Name} -------------------------------");
            
            
            RunTask(() => day.RunTask1(inputData), true);
            RunPerformanceTests(() => day.RunTask1(inputData));
            RunTask(() => day.RunTask2(inputData), false);
            RunPerformanceTests(() => day.RunTask2(inputData));
        }
    }

    static void RunTask(Func<long> taskToRun, bool task1)
    {
        var result = taskToRun.Invoke();
        var stringTask = task1 ? "Task 1" : "Task 2";
        Console.WriteLine("{0} | Result: {1}", stringTask, result);
    }
    
    static void RunPerformanceTests(Action taskToRun)
    {
        var performanceStatistics = RunPerformanceTestForTask(taskToRun);

        Console.WriteLine("Average Execution over {0} runs: {1}ms\t | Fastest Execution: {2}ms\t | Slowest Execution: {3}ms\t ",
            performanceStatistics.RunCount, performanceStatistics.AverageTime, performanceStatistics.MinTime, performanceStatistics.MaxTime);
        
    }

    static PerformanceStatistics RunPerformanceTestForTask(Action taskToRun)
    {
        var runCount = 100;
        var times = new List<long>();
        var maxTime = 0l;
        var minTime = long.MaxValue;
        for (var i = 0; i < runCount; i++)
        {
            var stopwatchTask = Stopwatch.StartNew();
            taskToRun.Invoke();
            stopwatchTask.Stop();
            
            var currentTime = stopwatchTask.ElapsedMilliseconds;
            times.Add(currentTime);
            if (maxTime < currentTime)
                maxTime = currentTime;
            if (minTime > currentTime)
                minTime = currentTime;
        }

        return new PerformanceStatistics()
        {
            MaxTime = maxTime,
            MinTime = minTime,
            AverageTime = times.Sum(t => t) / times.Count,
            RunCount = runCount
        };
    }
    
    static List<Day> GetDayList() => [new Day1(), new Day2(), new Day3(), new Day4()];

    static string[] GetTestDataPath(string name)
    {
        var path = Path.Combine(Environment.CurrentDirectory, @"Data2025", $"{name}.txt");
        return File.ReadAllLines(path);
    }

    static string OutputTime(TimeSpan duration)
    {
        return $"{duration.TotalMilliseconds}ms";
    }
}