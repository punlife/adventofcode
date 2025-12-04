using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public class Day4 : Day
{
    
    public long RunTask1(string[] input)
    {
        var xmasCount = 0;
        var pattern = @"(XMAS)";
        var reversedPattern = @"(SAMX)";
        // var newInput = FlattenThe2dArray(input);

        
        // foreach (var problemInput in newInput)
        // {
        //     var stringInput = new string(problemInput);
        //     var matchCollection = Regex.Matches(stringInput, pattern);
        //     var reversedMatchCollection = Regex.Matches(stringInput, reversedPattern);
        //     xmasCount += matchCollection.Count;
        //     xmasCount += reversedMatchCollection.Count;
        // }

        var rowLength = input.Length;
        var columnLenght = input.Length;

        return xmasCount;
    }

    public long RunTask2(string[] input)
    {
        return 0;
    }


    private List<char[]> FlattenThe2dArray(string[] input)
    {

        var flatList = new List<char[]>();
        var characterArray = new List<char[]>();
        var lineLength = input[0].ToCharArray().Length;
        foreach (var line in input)
        {
            var charArray = line.ToCharArray();
            characterArray.Add(charArray);
            flatList.Add(charArray);
        }
        
        // Vertical
        for (var x = 0; x < lineLength; x++)
        {
            var tempCharList = new List<char>();
            var sb = new StringBuilder();
            for (var y = 0; y < characterArray.Count; y++)
            {
                var character = characterArray[y][x];
                tempCharList.Add(character);
                sb.Append(character);
            }
            flatList.Add(tempCharList.ToArray());
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }
        
        
        
        for (var x = 0; x < lineLength; x++)
        {
            var tempCharList = new List<char>();
            var sb = new StringBuilder();
            
            flatList.Add(tempCharList.ToArray());
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }

        return flatList;
    }
    
}