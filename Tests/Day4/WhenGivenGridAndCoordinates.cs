using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day4;

[ExcludeFromCodeCoverage]
public class WhenGivenGridAndCoordinates
{
    private readonly IDay4 _sut = new Domain.Implementations.Day4();

    private readonly bool[][] _grid =
    [
        [false, false, true, true, false, true, true, true, true, false],
        [true, true, true, false, true, false, true, false, true, true],
        [true, true, true, true, true, false, true, false, true, true],
        [true, false, true, true, true, true, false, false, true, false],
        [true, true, false, true, true, true, true, false, true, true],
        [false, true, true, true, true, true, true, true, false, true],
        [false, true, false, true, false, true, false, true, true, true],
        [true, false, true, true, true, false, true, true, true, true],
        [false, true, true, true, true, true, true, true, true, false],
        [true, false, true, false, true, true, true, false, true, false]
    ];

    [Theory]
    [InlineData(0, 0, AccessiblePaperRoll.NotAPaperRoll)]
    [InlineData(0, 1, AccessiblePaperRoll.NotAPaperRoll)]
    [InlineData(0, 2, AccessiblePaperRoll.Accessible)]
    [InlineData(0, 3, AccessiblePaperRoll.Accessible)]
    [InlineData(1, 1, AccessiblePaperRoll.Inaccessible)]
    public void ThenItShould_MarkTheRollAsAccessibleOrNot(int row, int column, AccessiblePaperRoll expected)
    {
        var result = _sut.IsAccessiblePaperRoll(_grid, row, column);

        result.ShouldBe(expected);
    }
}