using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces;
using Shouldly;
using Xunit;

namespace Tests.Day4;

[ExcludeFromCodeCoverage]
public class WhenGivenInput
{
    private readonly IDay4 _sut = new Domain.Implementations.Day4();

    [Fact]
    public void ThenItShould_GetTheCorrectDimensions()
    {
        var grid = Execute;

        grid.Length.ShouldBe(10);
        grid[0].Length.ShouldBe(10);
    }

    [Fact]
    public void ThenItShould_HaveCorrectValues()
    {
        var grid = Execute;

        grid[0].ShouldBe([false, false, true, true, false, true, true, true, true, false]);
        grid[1].ShouldBe([true, true, true, false, true, false, true, false, true, true]);
        grid[2].ShouldBe([true, true, true, true, true, false, true, false, true, true]);
        grid[3].ShouldBe([true, false, true, true, true, true, false, false, true, false]);
        grid[4].ShouldBe([true, true, false, true, true, true, true, false, true, true]);
        grid[5].ShouldBe([false, true, true, true, true, true, true, true, false, true]);
        grid[6].ShouldBe([false, true, false, true, false, true, false, true, true, true]);
        grid[7].ShouldBe([true, false, true, true, true, false, true, true, true, true]);
        grid[8].ShouldBe([false, true, true, true, true, true, true, true, true, false]);
        grid[9].ShouldBe([true, false, true, false, true, true, true, false, true, false]);
    }

    private bool[][] Execute => _sut.ParseInput(Input);

    private const string Input = """
                                 ..@@.@@@@.
                                 @@@.@.@.@@
                                 @@@@@.@.@@
                                 @.@@@@..@.
                                 @@.@@@@.@@
                                 .@@@@@@@.@
                                 .@.@.@.@@@
                                 @.@@@.@@@@
                                 .@@@@@@@@.
                                 @.@.@@@.@.
                                 """;
}
