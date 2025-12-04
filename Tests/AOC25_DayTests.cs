using Day = AdventOfCode2025.Day;
using Day1 = AdventOfCode2025.Day1;
using Day2 = AdventOfCode2025.Day2;
using Day3 = AdventOfCode2025.Day3;
using Day4 = AdventOfCode2025.Day4;

namespace Tests;

public class Aoc25Tests
{
   [SetUp]
    public void Setup()
    {
    }
    
    [TestCaseSource(nameof(DayTestData))]
    public void DayTest(Day day, long expectedResultTask1, long expectedResultTask2)
    {
        var testDataPath = Path.Combine(Environment.CurrentDirectory, @"TestData2025", $"{day.GetType().Name}.txt");
        var testData = File.ReadAllLines(testDataPath);
        Assert.Multiple(() =>
        {
            Assert.That(day.RunTask1(testData), Is.EqualTo(expectedResultTask1));
            Assert.That(day.RunTask2(testData), Is.EqualTo(expectedResultTask2));
        });
    }
    
    private static object[] DayTestData =
    {
        new object[] { new Day1(), 3, 6 },
        new object[] { new Day2(), 1227775554, 4174379265 },
        new object[] { new Day3(), 357, 3121910778619 },
        new object[] { new Day4(), 13, 43 }
    };
}