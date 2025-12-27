using Domain.Interfaces;

namespace Domain.Implementations;

public class Day1 : IDay1
{
    private int Value
    {
        get;
        set
        {
            var numberOfTimesMovedOverZero = Math.Abs(value / 100);
            if (field * value < 0) numberOfTimesMovedOverZero++;

            field = value % 100;

            if (field == 0) _numberOfTimesZero++;
            if (field == 0 && numberOfTimesMovedOverZero == 0) numberOfTimesMovedOverZero++;

            _numberOfTimesOverZero += numberOfTimesMovedOverZero;
            NumberOfTimesMoved++;
        }
    }

    private int _numberOfTimesZero;
    private int _numberOfTimesOverZero;
    public int GetNumberOfTimesZero() => _numberOfTimesZero;
    public int GetNumberOfTimesMovedOverZero() => _numberOfTimesOverZero;

    public int NumberOfTimesMoved;

    public Day1()
    {
        Value = 50;
    }

    public int GetCurrentValue() => Value;

    public (int TurnValue, Direction Direction) ParseInput(string input)
    {
        var directionLetter = input[0];
        var turnValue = int.Parse(input[1..].Trim());

        return directionLetter switch
        {
            'L' => (turnValue, Direction.Left),
            'R' => (turnValue, Direction.Right),
            _ => throw new ArgumentOutOfRangeException(nameof(directionLetter), directionLetter, null)
        };
    }

    public void UpdateValue(int turnValue, Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                Value -= turnValue;
                break;
            case Direction.Right:
                Value += turnValue;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public async Task Solve(string inputFilePath)
    {
        var lines = File.ReadLinesAsync(inputFilePath);

        await foreach (var line in lines)
        {
            var (turnValue, direction) = ParseInput(line);
            UpdateValue(turnValue, direction);
        }

        Console.WriteLine($"Total amount stopped on 0: {_numberOfTimesZero}");
        Console.WriteLine($"Total amount moved over 0: {_numberOfTimesOverZero}");
    }
}