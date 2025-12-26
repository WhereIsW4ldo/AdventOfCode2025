namespace Domain.Interfaces;

public interface IDay1 : IDay
{
    int GetCurrentValue();

    int GetNumberOfTimesZero();

    (int TurnValue, Direction Direction) ParseInput(string input);

    void UpdateValue(int turnValue, Direction direction);
}

public enum Direction {
    Left,
    Right
}
