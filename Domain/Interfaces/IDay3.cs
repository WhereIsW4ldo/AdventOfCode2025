namespace Domain.Interfaces;

public interface IDay3 : IDay
{
    long GetHighestJolts(string input, int numberOfSelections);
    (int Index, int Value) GetHighestNumber(string input);
}