using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AdventOfCode2025;

namespace AdventOfCode2025;

public class Day1 : Day
{
    private int task2Counter = 0;
  
    
    
    public long RunTask1(string[] input)
    {
        var target = 0;
        var targetHitCount = 0;
        var currentValue = 50;
      
        foreach (var line in input)
        {
            var goLeft = line.Substring(0, 1).Equals("L");
            var shift = int.Parse(line.Substring(1));
            currentValue = CountTillValue(currentValue, shift, goLeft);
            if (currentValue == target)
                targetHitCount++;
        }

        return targetHitCount;
    }

    public long RunTask2(string[] input)
    {
        var target = 0;
        var targetHitCount = 0;
        var currentValue = 50;

        foreach (var line in input)
        {
            var goLeft = line.Substring(0, 1).Equals("L");
            var shift = int.Parse(line.Substring(1));
            currentValue = CountTillValue2(currentValue, shift, goLeft);
        }

        return task2Counter;
    }

    private int CountTillValue(int currentValue, int shift, bool shiftLeft)
    {
        for (var i = shift; i > 0; i--)
        {
            if (shiftLeft)
            {
                currentValue = currentValue == 0 ? 99 : currentValue - 1;
            }
            else
            {
                currentValue = currentValue == 99 ? 0 : currentValue + 1;
            }
        }

        return currentValue;
    }
    
    private int CountTillValue2(int currentValue, int shift, bool shiftLeft)
    {
        for (var i = shift; i > 0; i--)
        {
            if (currentValue == 0)
                task2Counter += 1;
            
            if (shiftLeft)
            {
                if (currentValue == 0)
                {
                    currentValue = 99;
                }
                else 
                    currentValue -= 1;
            }
            else
            {
                if (currentValue == 99)
                {
                    currentValue = 0;
                }
                else
                    currentValue += 1;
                
            }
        }

        return currentValue;
    }
    
}