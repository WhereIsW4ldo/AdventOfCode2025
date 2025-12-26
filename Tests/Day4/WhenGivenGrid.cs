using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces;
using Shouldly;
using Xunit;

namespace Tests.Day4;

[ExcludeFromCodeCoverage]
public class WhenGivenGrid
{
    private readonly IDay4 _sut = new Domain.Implementations.Day4();

    [Fact]
    public void ThenItShould_ReturnTheCorrectNumberOfAccessibleRolls()
    {
        var result = _sut.GetAccessiblePaperRollCount(Input);

        result.ShouldBe(13);
    }

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
