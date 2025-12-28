namespace Domain.Interfaces;

public interface IDay6 : IDay
{
    Problem[] Parse(string input);
    long Calculate(Problem[] problems);

    Problem[] ParsePart2(string input);
    Task SolvePart2(string inputFileName);
}

public record Problem
{
    public Method Method { get; set; }
    public long[] Numbers { get; set; } = [];
}

public enum Method
{
    Sum,
    Product
}