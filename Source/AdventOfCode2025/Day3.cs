namespace AdventOfCode2025;

public class Day3 : Day
{
    public long RunTask1(string[] input)
    {
        var joltageCount = 0l;
        foreach (var value in input)
        {
            var joltage = GetJoltagesButBetter(value, 2);
            joltageCount += joltage;
        }

        return joltageCount;
    }
    
    public long RunTask2(string[] input)
    {
        var joltageCount = 0l;
        foreach (var value in input)
        {
            var joltage = GetJoltagesButBetter(value, 12);
            joltageCount += joltage;
        }
        return joltageCount;
    }

    private long GetJoltages(string value)
    {
        long ConvertTupleToLong(Tuple<int, int> tuple)
        {
            return long.Parse($"{tuple.Item1}{tuple.Item2}");
        }
        
        var stringValue = value.ToString();
        var previousHighestJoltage = new Tuple<int, int>(0,0);
        var currentHighestJoltage = new Tuple<int, int>(0,0);
        
        for (var i = 0; i < stringValue.Length; i++)
        {
            var digit = int.Parse(stringValue[i].ToString());
            if (i+1 < stringValue.Length && digit > currentHighestJoltage.Item1)
            {
                previousHighestJoltage = currentHighestJoltage;
                currentHighestJoltage = new Tuple<int, int>(digit, 0);
                continue;
            }
            if (digit > currentHighestJoltage.Item2)
            {
                currentHighestJoltage = new Tuple<int, int>(currentHighestJoltage.Item1, digit);
                continue;
            }
        }
        var previousJoltage = ConvertTupleToLong(previousHighestJoltage);
        var currentJoltage = ConvertTupleToLong(currentHighestJoltage);
        
        return previousJoltage > currentJoltage 
            ? previousJoltage 
            : currentJoltage;
    }
    
    private long GetJoltagesButBetter(string initialValue, int digitCount)
    {
        var digits = new List<int>();
        var remainingDigitsToFind = digitCount;
        var value = initialValue;
        while (remainingDigitsToFind > 0)
        {
            var valueLength = value.Length;
            var currentHighest = 0;
            var indexOfHighestDigit = 0;
            for (var i = 0; i < valueLength; i++)
            {
                var digit = int.Parse(value[i].ToString());
                if (digit > currentHighest)
                {
                    currentHighest = digit;
                    indexOfHighestDigit = i;
                }
                if (!(i + remainingDigitsToFind < valueLength))
                {
                    break;
                }
            }
            // Add to digits, decrement counter, create new substring
            digits.Add(currentHighest);
            remainingDigitsToFind--;
            if (remainingDigitsToFind > 0)
                value = value.Substring(indexOfHighestDigit+1);
        }

        var joltage = long.Parse(string.Join("", digits));
        return joltage;
    }
}