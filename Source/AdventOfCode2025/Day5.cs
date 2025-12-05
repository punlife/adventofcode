namespace AdventOfCode2025;

public class Day5 : Day
{
    public long RunTask1(string[] input)
    {
        var freshIngredients = 0;
        var idsToCheck = new List<long>();
        var ranges = new List<IngredientRange>();
        var rangesLoaded = false;
        foreach (var line in input)
        {
            if (line.Equals(string.Empty))
            {
                rangesLoaded = true;
                continue;
            }

            if (!rangesLoaded)
            {
                var range = line.Split("-");
                ranges.Add(new IngredientRange(long.Parse(range[0]), long.Parse(range[1])));
            }
            else
            {
                idsToCheck.Add(long.Parse(line));
            }
        }
        foreach (var id in idsToCheck)
        {
            if (ranges.Any(range => range.IsNumberWithinRange(id)))
            {
                freshIngredients++;
            }
        }

        return freshIngredients;
    }

    public long RunTask2(string[] input)
    {
        var initialRanges = input.TakeWhile(line => !line.Equals(string.Empty))
            .Select(line => line.Split("-"))
            .Select(range => new IngredientRange(long.Parse(range[0]), long.Parse(range[1])))
            .OrderBy(r => r.LowerBound)
            .ToList();

        var rangesUpdated = false;
        var updatedRanges = new List<IngredientRange>();
        var ranges = initialRanges;
        do
        {
            if (updatedRanges.Any())
                ranges = updatedRanges;

            rangesUpdated = false;
            updatedRanges = GetCombinedRanges(ranges);

            if (ranges.Count != updatedRanges.Count)
                rangesUpdated = true;
            
        } while (rangesUpdated);

        return updatedRanges
            .Select(r => r.GetCountBetween())
            .Sum();
    }

    private List<IngredientRange> GetCombinedRanges(List<IngredientRange> ranges)
    {
        var updatedRanges = new List<IngredientRange>();
        for (var i = 0; i < ranges.Count; i++)
        {
            var currentRange = ranges[i];
            if (i + 1 == ranges.Count)
            {
                updatedRanges.Add(currentRange);
                break;
            }
            var nextRange = ranges[i + 1];
            if (currentRange.IsOverlapping(nextRange))
            {
                updatedRanges.Add(currentRange.GetCombinedRange(nextRange));
                i++;
                continue;
            }

            updatedRanges.Add(currentRange);
        }

        return updatedRanges;
    }

    class IngredientRange(long lowerBound, long upperBound)
    {
        public bool IsNumberWithinRange(long number)
        {
            return LowerBound <= number && number <= UpperBound;
        }
        
        public long GetCountBetween()
        {
            return UpperBound - LowerBound + 1;
        }

        public bool IsOverlapping(IngredientRange other)
        {
            return LowerBound <= other.UpperBound && other.LowerBound <= UpperBound;
        }

        public IngredientRange GetCombinedRange(IngredientRange other)
        {
            return new IngredientRange(Math.Min(LowerBound, other.LowerBound), Math.Max(UpperBound, other.UpperBound));
        }

        public long LowerBound { get; set; } = lowerBound;
        public long UpperBound { get; set; } = upperBound;
    }
}