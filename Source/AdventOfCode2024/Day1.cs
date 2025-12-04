namespace AdventOfCode2024;

public class Day1 : Day
{
    private List<int> leftList = new List<int>();
    private List<int> rightList = new List<int>();
    private Dictionary<int, int> occuranceCount = new Dictionary<int, int>();

    public long RunTask1(string[] input)
    {
        foreach (var line in input)
        {
           var lineArray = line.Split(" ");
           var lineArraySize = lineArray.Length;
           leftList.Add(int.Parse(lineArray[0]));
           rightList.Add(int.Parse(lineArray[lineArraySize-1]));
        }

        leftList.Sort();
        rightList.Sort();

        return ReturnSumOfEachLineDifference();
    }
    
    public long RunTask2(string[] input)
    {
        foreach (var line in input)
        {
            var lineArray = line.Split(" ");
            var lineArraySize = lineArray.Length;
            leftList.Add(int.Parse(lineArray[0]));
            var rightValue = int.Parse(lineArray[lineArraySize - 1]);
            rightList.Add(rightValue);

            if (occuranceCount.ContainsKey(rightValue))
                occuranceCount[rightValue] += 1;
            else
                occuranceCount.Add(rightValue, 1);   
        }
        leftList.Sort();

        return ReturnProductOfEachLineDifference();
    }

    private long ReturnSumOfEachLineDifference()
    {
        return leftList.Select((t, i) => Math.Abs(t - rightList[i])).Sum();
    }
    
    private long ReturnProductOfEachLineDifference()
    {
        var sum = 0l;
        foreach (var value in leftList)
        {
            if (occuranceCount.TryGetValue(value, out var count))
                sum += value * count;

        }
        return sum;
    }
}