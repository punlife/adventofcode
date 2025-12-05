using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AdventOfCode2025;
using CsvHelper;

namespace AdventOfCode2025;

public class Day2 : Day
{
    public long RunTask1(string[] input)
    {
        var invalidCount = 0l;
        using var reader = new StringReader(input[0]);
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            while (csvReader.Read())
            {
                var idValues = csvReader.GetField(0)?.Split('-');
                var startId = long.Parse(idValues![0]);
                var endId = long.Parse(idValues![1]);
                invalidCount += CheckIds(startId, endId);
            }
        }

        return invalidCount;
    }

    public long RunTask2(string[] input)
    {
        var invalidCount = 0l;

        using var reader = new StringReader(input[0]);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        while (csvReader.Read())
        {
            var idValues = csvReader.GetField(0)?.Split('-');
            var startId = long.Parse(idValues![0]);
            var endId = long.Parse(idValues![1]);
            invalidCount += CheckIds2(startId, endId);
        }
        return invalidCount;
    }

    private long CheckIds(long currentValue, long endValue)
    {
        var invalidCount = 0l;
        for (var i = currentValue; i <= endValue; i++)
        {
            var stringValue = i.ToString();
            var stringLength = stringValue.Length;
            var halfStringLength = stringLength / 2;

            var charactersEqual = true;
            for (var j = 0; j < halfStringLength; j++)
            {
                if (!stringValue[j].Equals(stringValue[halfStringLength + j]))
                {
                    charactersEqual = false;
                    break;
                }
            }

            if (charactersEqual)
            {
                invalidCount++;
            }
        }

        return invalidCount;
    }

    private long CheckIds2(long currentValue, long endValue)
    {
        var invalidCount = 0l;
        for (var i = currentValue; i <= endValue; i++)
        {
            var stringValue = i.ToString();
            var anyFalseIds = CheckAllSubstrings(stringValue);

            if (anyFalseIds)
            {
                Interlocked.Add(ref invalidCount, i);
            }
        }

        ;

        return invalidCount;
    }

    private bool CheckAllSubstrings(string value)
    {
        var valueLength = value.Length;
        var factors = GetFactorsOfTheLength(valueLength);

        var strings = new Dictionary<string, int>();
        foreach (var factor in factors)
        {
            var substringLength = factor == 1
                ? factor
                : valueLength / factor;
            for (var i = 0; i < valueLength; i += substringLength)
            {
                var substring = value.Substring(i, substringLength);
                if (!strings.TryAdd(substring, 1))
                    strings[substring]++;
            }
        }

        return strings.Any(s => s.Key.Length * s.Value == valueLength);
    }

    private List<int> GetFactorsOfTheLength(int length)
    {
        var factors = new List<int>();
        for (var i = 1; i < length; i++)
        {
            if (length % i == 0)
                factors.Add(i);
        }

        return factors;
    }
}