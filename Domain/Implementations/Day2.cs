using Domain.Interfaces;

namespace Domain.Implementations;

public class Day2 : IDay2
{
    public IdRange[] GetRanges(string input)
    {
        var stringRanges = input.Split(',');

        var rangesList = new List<IdRange>();

        foreach (var stringRange in stringRanges)
        {
            var parts = stringRange.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);

            rangesList.Add(new IdRange(start, end));
        }

        return rangesList.ToArray();
    }

    public long[] GetInvalidIdsFromRange(IdRange idRange)
    {
        var invalidIds = new List<long>();

        for (var i = idRange.Start; i <= idRange.End; i++)
        {
            if (!IsValidId(i))
                invalidIds.Add(i);
        }

        return invalidIds.ToArray();
    }

    public bool IsValidId(long id)
    {
        var stringId = id.ToString();

        for (var i = 1; i < stringId.Length; i++)
        {
            var currentNumber = stringId[..i];
            var splitNumber = stringId.Split(currentNumber, StringSplitOptions.RemoveEmptyEntries);

            if (splitNumber.Length == 0) return false;
        }

        return true;
    }

    public async Task Solve(string inputFilePath)
    {
        var text = await File.ReadAllTextAsync(inputFilePath);

        var ranges = GetRanges(text);


        var invalidIds = ranges
            .Select(GetInvalidIdsFromRange)
            .ToArray();

        var sum = invalidIds
            .SelectMany(i => i)
            .Sum();

        Console.WriteLine($"Sum of invalid ids: {sum}");
    }
}