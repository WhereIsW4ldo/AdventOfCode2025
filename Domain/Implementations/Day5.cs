using Domain.Interfaces;
using Domain.Utils;

namespace Domain.Implementations;

public class Day5 : IDay5
{
    public (ICollection<long> ranges, ICollection<long> ids) ParseInput(string input)
    {
      var lines = input.Split(['\n', '\r']);
      var parsingRanges = true;

      var validIds = new HashSet<long>();
      var ids = new HashSet<long>();
      foreach (var line in lines)
      {
        if (string.IsNullOrWhiteSpace(line.Trim()))
        {
          parsingRanges = false;
          continue;
        }

        // parsing ranges
        if (parsingRanges)
        {
          var numbers = line.Split('-');
          var lowerRange = long.Parse(numbers[0]);
          var higherRange = long.Parse(numbers[1]);

          for (var i = lowerRange; i <= higherRange; i++)
          {
            validIds.Add(i);
          }
        }

        // parsing ids
        if (!parsingRanges)
        {
          var newId = long.Parse(line);
          ids.Add(newId);
        }
      }

      return (validIds, ids);
    }

    public async Task Solve(string inputFilePath)
    {
      var validRanges = new List<(long lower, long upper)>();
      var ids = new HashSet<long>();

      var parsingRanges = true;
      await foreach (var line in File.ReadLinesAsync(inputFilePath))
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          parsingRanges = false;
          continue;
        }

        if (parsingRanges)
          ParseRange(validRanges, line);
        else
          ParseId(ids, line);
      }

      var freshIds = ids.Where(id => validRanges.Any(range => IsInRange(range, id)));

      Console.WriteLine($"Amount of fresh ids: {freshIds.Count()}");
    }

    public async Task SolvePart2(string inputFilePath)
    {
      var validRanges = new List<(long lower, long upper)>();

      await foreach (var line in File.ReadLinesAsync(inputFilePath))
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          break;
        }

        ParseRange(validRanges, line);
      }

      var realRanges = validRanges
        .Select(r => new Range { LowerBound = r.lower, UpperBound = r.upper })
        .ToArray();

      SortedList<long, Range> ranges = [];

      foreach (var range in realRanges)
      {
        if (ranges.Count == 0)
        {
            ranges.Add(range.LowerBound, range);
            continue;
        }

        var rangeToAdd = GetOrUpdateRange(ranges, range);

        if (rangeToAdd is not null)
          ranges.Add(rangeToAdd.LowerBound, rangeToAdd);
      }

      var totalAmount = ranges
        .Select(range => range.Value)
        .Sum(range => range.UpperBound - range.LowerBound + 1);

      Console.WriteLine($"Total elements in all ranges: {totalAmount}");
    }

    private static Range? GetOrUpdateRange(SortedList<long, Range> ranges, Range range)
    {
      var overlappingRanges = ranges
        .Where(r => OverLap(r.Value, range))
        .Select(r => r.Value)
        .ToArray();

      if (overlappingRanges.Length == 0)
        return range;

      var lowestBound = overlappingRanges.Concat([range]).Min(r => r.LowerBound);
      var uppestBound = overlappingRanges.Concat([range]).Max(r => r.UpperBound);

      foreach (var overlappingRange in overlappingRanges)
        ranges.Remove(overlappingRange.LowerBound);

      return new Range { LowerBound = lowestBound, UpperBound = uppestBound };
    }

    private static bool OverLap(Range range1, Range range2)
    {
      return range1.LowerBound.IsBetween(range2.LowerBound, range2.UpperBound)
        || range1.UpperBound.IsBetween(range2.LowerBound, range2.UpperBound)
        || range2.LowerBound.IsBetween(range1.LowerBound, range1.UpperBound)
        || range2.UpperBound.IsBetween(range1.LowerBound, range1.UpperBound);
    }

    private static bool IsInRange((long lower, long upper) validRange, long id)
    {
        return validRange.lower <= id && id <= validRange.upper;
    }


    private static void ParseId(HashSet<long> ids, string line)
    {
        var newId = long.Parse(line);
        ids.Add(newId);
    }

    private static void ParseRange(List<(long lower, long upper)> validRanges, string line)
    {
        var numbers = line.Split('-');
        var lowerRange = long.Parse(numbers[0]);
        var higherRange = long.Parse(numbers[1]);

        validRanges.Add((lowerRange, higherRange));
    }

    private record Range
    {
      public long LowerBound { get; set; }
      public long UpperBound { get; set; }
    }
}
