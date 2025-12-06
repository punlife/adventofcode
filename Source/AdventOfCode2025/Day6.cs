using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2025;

public class Day6 : Day
{
    public long RunTask1(string[] input)
    {
        return GetProblems(input)
            .Select(p => p.Calculate())
            .Sum();
    }

    public long RunTask2(string[] input)
    {
        return GetProblemsElectricBoogaloo(input)
            .Select(p => p.Calculate())
            .Sum();
    }
    
    private List<Problem> GetProblems(string[] input)
    {
        var problemList = new List<Problem>();
        for (var j = 0; j < input.Length; j++)
        {
            var values = Regex.Replace(input[j], @"\s+", " ").Trim().Split(" ");
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];
                // Create problem objects in first line
                if (j == 0)
                {
                    var newProblem = new Problem();
                    newProblem.Values.Add(long.Parse(value));
                    problemList.Add(newProblem);
                }
                // If final line, treat as operation
                else if (j == input.Length - 1)
                {
                    problemList[i].Operation = values[i];
                }
                else
                {
                    problemList[i].Values.Add(long.Parse(value));
                }
            }
        }

        return problemList;
    }
    
    private List<Problem> GetProblemsElectricBoogaloo(string[] input)
    {
        var problemList = new List<Problem>();
        var lineLength = input[0].Length;
        var lineCount = input.Length;
        var currentProblem = new Problem();
        
        void FinalizeProblem()
        {
            problemList.Add(currentProblem);
            currentProblem = new Problem();
        }
        
        for (var i = 0; i < lineLength; i++)
        {
            var sb = new StringBuilder();
            for (var y = 0; y < lineCount - 1; y++)
            {
                var character = input[y][i];
                if (char.IsDigit(character))
                    sb.Append(character);
            }
            var completeValue = sb.ToString();
            
            if (!completeValue.Equals(string.Empty))
            {
                currentProblem.Values.Add(long.Parse(completeValue));
                if (i == lineLength - 1) //Final line handling
                    FinalizeProblem();
            }
            else
                FinalizeProblem();
        }
        
        // Handle operations
        var values = Regex.Replace(input[lineCount-1], @"\s+", " ").Trim().Split(" ");
        for (var i = 0; i < values.Length; i++)
            problemList[i].Operation = values[i];

        return problemList;
    }
}

class Problem()
{
    public List<long> Values { get; } = [];
    public string Operation { get; set; }

    public long Calculate() => Operation switch
    {
        "+" => Values.Sum(),
        "*" => Values.Aggregate((current, next) => current * next),
        _ => throw new ArgumentException($"Unknown operation: {Operation}")
    };
}