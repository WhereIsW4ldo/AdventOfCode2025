using Domain.Interfaces;

namespace Domain.Implementations;

public class Day3 : IDay3
{
    public long GetHighestJolts(string input, int numberOfSelections)
    {
        var total = 0L;

        var lastIndex = 0;
        for (var i = 1; i <= numberOfSelections; i++)
        {
            var numberInput = input[lastIndex..^(numberOfSelections - i)];
            var (index, digit) = GetHighestNumber(numberInput);
            total += digit * (long)Math.Pow(10, numberOfSelections - i);
            lastIndex += index + 1;
        }

        return total;
    }

    public (int Index, int Value) GetHighestNumber(string input)
    {
        var highestNumber = 0;
        var highestIndex = 0;

        for (var i = 0; i < input.Length; i++)
        {
            int currentNumber = input[i] - '0';

            if (currentNumber == 9)
                return (i, currentNumber);

            if (currentNumber > highestNumber)
            {
                highestNumber = currentNumber;
                highestIndex = i;
            }
        }

        return (highestIndex, highestNumber);
    }

    public async Task Solve(string inputFilePath)
    {
        var lines = File.ReadLinesAsync(inputFilePath);

        var total = await lines
            .Select(input => GetHighestJolts(input, 12))
            .SumAsync();

        Console.WriteLine($"Total: {total}");
    }
}
