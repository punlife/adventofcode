using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day3 : Day
{
    public long RunTask1(string[] problemInput)
    {
        var sum = 0;
        foreach (var input in problemInput)
        {
            var pattern = @"mul(\(\d{1,3},\d{1,3}\))";
            var matchCollection = Regex.Matches(input, pattern);

            for (var index = 0; index < matchCollection.Count; index++)
            {
                var match = matchCollection[index];
                var valueArray = match.Groups[1].Value
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(',');
                
                var a = int.Parse(valueArray[0]);
                var b = int.Parse(valueArray[1]);
                sum += a * b;
            }
        }
        
        return sum;
    }
    
    public long RunTask2(string[] problemInput)
    {
        var sum = 0;
        var newInput = PrepareSafeInstructions(problemInput);
        
        
        foreach (var input in newInput)
        {
            var pattern = @"mul(\(\d{1,3},\d{1,3}\))";
            var matchCollection = Regex.Matches(input, pattern);

            for (var index = 0; index < matchCollection.Count; index++)
            {
                var match = matchCollection[index];
                var valueArray = match.Groups[1].Value
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(',');
                
                var a = int.Parse(valueArray[0]);
                var b = int.Parse(valueArray[1]);
                sum += a * b;
            }
        }
        
        return sum;
    }

    private List<string> PrepareSafeInstructions(string[] input)
    {
        var newInstructions = new List<string>();
        var active = true;
        foreach (var stringValue in input)
        {
            var substring = new StringBuilder();
            for (var index = 0; index < stringValue.Length; index++)
            {
                substring.Append(stringValue[index]);
                if (substring.ToString().Contains("don't()") && active)
                {
                    active = false;
                    newInstructions.Add(substring.ToString());
                    substring.Clear();
                    continue;
                }
                
                if (substring.ToString().Contains("do()"))
                {
                    if (active)
                    {
                        newInstructions.Add(substring.ToString());
                    }
                    
                    active = true;
                    substring.Clear();
                    continue;
                }
                
                if (index == stringValue.Length - 1 && active)
                {
                    newInstructions.Add(substring.ToString());
                }
            }
            
        }
        
        return newInstructions;
    }
}