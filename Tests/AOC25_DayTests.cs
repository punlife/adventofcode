using AdventOfCode2025;

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
        new object[] { new Day4(), 13, 43 },
        new object[] { new Day5(), 3, 14 },
        new object[] { new Day6(), 4277556, 3263827 }
    };
}