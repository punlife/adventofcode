using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AdventOfCode2025;

namespace AdventOfCode2025;

public class Day2 : Day
{
    public long RunTask1(string[] input)
    {
        var csv = input[0].Split(",");
        var invalidCount = 0l;
        foreach (var value in csv)
        {
            var idValues = value.Split('-');
            var startId = long.Parse(idValues[0]);
            var endId = long.Parse(idValues[1]);
            invalidCount += CheckIds(startId, endId);
        }

        return invalidCount;
    }

    public long RunTask2(string[] input)
    {
        var csv = input[0].Split(",");
        var invalidCount = 0l;
        var invalidArray = new List<long>();
        foreach (var value in csv)
        {
            var idValues = value.Split('-');
            var startId = long.Parse(idValues[0]);
            var endId = long.Parse(idValues[1]);
            var currentInvalidIds = CheckIds2(startId, endId);
            invalidArray.AddRange(currentInvalidIds);
        }

        return invalidArray.Sum();
    }

    private long CheckIds(long currentValue, long endValue)
    {
        var invalidCount = 0l;
        for (var i = currentValue; i <= endValue; i++)
        {
            var stringValue = i.ToString();
            var stringLength =  stringValue.Length;
            
            var leftSide = stringValue.Substring(0, stringLength/2);
            var rightSide = stringValue.Substring(stringLength/2);

            if (leftSide.Equals(rightSide))
            {
                invalidCount += i;
            }
        }

        return invalidCount;
    }
    
    private List<long> CheckIds2(long currentValue, long endValue)
    {
        var invalidArray = new List<long>();
        var invalidCount = 0l;
        for (var i = currentValue; i <= endValue; i++)
        {
            var stringValue = i.ToString();
            var anyFalseIds = CheckAllSubstrings(stringValue);

            if (anyFalseIds)
            {
                invalidArray.Add(i);
            }
        }

        return invalidArray;
    }

    private bool CheckAllSubstrings(string value)
    {
        var valueLength = value.Length;
        var strings = new Dictionary<string, int>();
        var characterDictionary = new Dictionary<string, int>();
        
        void CheckSubstrings(string valueToCheck)
        {
            var length = valueToCheck.Length;
            for (var i = 0; i < length; i++) {
                var substring = valueToCheck[..(i+1)];
                if (!strings.TryAdd(substring, 1))
                    strings[substring]++;
            } 
        }
        
        foreach (var c in value)
        {
            if (!characterDictionary.TryAdd(c.ToString(), 1))
                characterDictionary[c.ToString()]++;
            
        }
        var leftSide = value.Substring(0, valueLength/2);
        var rightSide = value.Substring(valueLength/2);
        
        CheckSubstrings(leftSide);
        CheckSubstrings(rightSide);

        var allSameCharacter = characterDictionary.Any(s => s.Value == valueLength);
        
        return allSameCharacter || strings
            .Where(s => s.Key.Length * s.Value == valueLength)
            .Any(s => s.Value > 1);
    }
    
}