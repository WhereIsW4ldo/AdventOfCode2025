using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day1;

[ExcludeFromCodeCoverage]
public class Day1Tests
{
    private readonly IDay1 _sut = new Domain.Implementations.Day1();

    [Theory]
    [InlineData("R2", 2, Direction.Right)]
    [InlineData("L3", 3, Direction.Left)]
    [InlineData("R25", 25, Direction.Right)]
    [InlineData("L37", 37, Direction.Left)]
    public void ParseInput_Parses_TurnValue_And_Direction(string input, int expectedTurnValue, Direction expectedDirection)
    {
        var (turnValue, direction) = _sut.ParseInput(input);

        turnValue.ShouldBe(expectedTurnValue);
        direction.ShouldBe(expectedDirection);
    }

    [Theory]
    [InlineData(Direction.Left, 50, 0)]
    [InlineData(Direction.Left, 51, -1)]
    [InlineData(Direction.Right, 51, 1)]
    [InlineData(Direction.Right, 75, 25)]
    [InlineData(Direction.Left, 500, -50)]
    public void UpdateValue_Individually_Updates_Value(Direction direction, int turnValue, int expectedValue)
    {
        _sut.UpdateValue(turnValue, direction);
        _sut.GetCurrentValue().ShouldBe(expectedValue);
    }
}