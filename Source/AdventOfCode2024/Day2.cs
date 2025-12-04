using System.Text;

namespace AdventOfCode2024;

public class Day2 : Day
{
    public long RunTask1(string[] input)
    {
        var safeReportCount = 0;
        foreach (var line in input)
        {
           var lineArray = line.Split(" ");
           
           var lastDigit = int.Parse(lineArray[0]);
           var isDecreasing = false;
           var isIncreasing = false;
           for (var index = 1; index < lineArray.Length; index++)
           {
               var currentDigit = int.Parse(lineArray[index]);
               if (index == 1)
               {
                   isDecreasing = lastDigit > currentDigit;
                   isIncreasing =lastDigit < currentDigit;

                   if (!isIncreasing && !isDecreasing)
                       break;
               }

               if (Math.Abs(lastDigit - currentDigit) > 3)
                   break;

               if (isIncreasing && currentDigit <= lastDigit)
                   break;
               if (isDecreasing && currentDigit >= lastDigit)
                   break;

               if (index == lineArray.Length - 1)
                   safeReportCount++;

               lastDigit = currentDigit;
           }
        }
        
        return safeReportCount;
    }
    
    public long RunTask2(string[] input)
    {
        var safeReportCount = 0;
        foreach (var line in input)
        {
            var freshLines = CreateLinesToProcess(line);
            var baseLineValid = ProcessLine(freshLines[0]) == 1;
            var validLinePresent = false;

            if (!baseLineValid)
            {
                for (var i = 1; i < freshLines.Count; i++)
                {
                    validLinePresent = ProcessLine(freshLines[i], probCount: 1) == 1;
                    if (validLinePresent)
                        break;
                }
            }
            
            if (baseLineValid || validLinePresent)
                safeReportCount += 1;
        }
        
        return safeReportCount;
    }


    private List<List<string>> CreateLinesToProcess(string line)
    {
        var lines = new List<List<string>>();
        var ses = line.Split(" ").ToList();
        lines.Add(ses);
        
        for (var index = 0; index < ses.Count; index++)
        {
            var newArray = line.Split(" ").ToList();
            newArray.RemoveAt(index);
            lines.Add(newArray);
        }
        
        return lines;
    }
    
    
    
    
    private int ProcessLine(List<string> line, int probCount = 0)
    {
        var lastDigit = int.Parse(line[0]);
        var isDecreasing = false;
        var isIncreasing = false;
        var problemDetected = probCount != 0;
        var problemCounter = probCount;

        for (var index = 1; index < line.Count; index++)
        {
            if (problemCounter > 1)
                break;
            
            var currentDigit = int.Parse(line[index]);
            if (index == 1)
            {
                var isEqual = lastDigit == currentDigit;
                isDecreasing = lastDigit > currentDigit;
                isIncreasing = lastDigit < currentDigit;
            
                if (isEqual)
                {
                    problemCounter += 1; 
                    continue;
                }
            }

            if (CheckIsInCorrectDirection(isIncreasing, currentDigit, lastDigit))
            {
                problemCounter += 1; 
                continue;
            }
            
            if (Math.Abs(lastDigit - currentDigit) > 3)
            {
                problemCounter += 1; 
                continue;
            }
                
            lastDigit = currentDigit;
        }

        if (problemCounter > 1)
        {

            var sb = new StringBuilder();
            foreach (var character in line)
            {
                sb.Append(character + " ");
            }
            // Console.WriteLine($"Unsafe:{sb}");
            return 0;
        }
        
        return 1;
    }

    private bool CheckIsInCorrectDirection(bool increasing, int current, int last)
    {
        // Can't be equal
        if (last == current)
            return true;
        // If direction is different return false
        switch (increasing)
        {
            case true when last > current:
            case false when last < current:
                return true;
        }

        return false;
    }
  
}