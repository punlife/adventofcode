using AdventOfCode2024;

namespace Tests;

public class AOC24_Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [TestCaseSource(nameof(DayTestData))]
    public void DayTest(Day day, long expectedResultTask1, long expectedResultTask2)
    {
        var testDataPath = Path.Combine(Environment.CurrentDirectory, @"TestData2024", $"{day.GetType().Name}.txt");
        var testData = File.ReadAllLines(testDataPath);
        Assert.Multiple(() =>
        {
            Assert.That(day.RunTask1(testData), Is.EqualTo(expectedResultTask1));
            Assert.That(day.RunTask2(testData), Is.EqualTo(expectedResultTask2));
        });
    }
    
    private static object[] DayTestData =
    {
        new object[] { new Day1(), 11, 31 },
        new object[] { new Day2(), 2, 14 },
        new object[] { new Day3(), 161, 168 },
        new object[] { new Day4(), 18, 168 }
    };
    
}