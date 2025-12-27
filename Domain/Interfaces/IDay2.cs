namespace Domain.Interfaces;

public interface IDay2 : IDay
{
    IdRange[] GetRanges(string input);

    long[] GetInvalidIdsFromRange(IdRange idRange);

    bool IsValidId(long id);
}

public record IdRange(long Start, long End);