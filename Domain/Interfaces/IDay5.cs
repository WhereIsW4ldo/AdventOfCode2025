namespace Domain.Interfaces;

public interface IDay5 : IDay
{
    (ICollection<long> ranges, ICollection<long> ids) ParseInput(string input);

    Task SolvePart2(string inputFilePath);
}