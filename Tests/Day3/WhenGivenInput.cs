using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day3;

[ExcludeFromCodeCoverage]
public class WhenGivenInput
{
    private readonly IDay3 _sut = new Domain.Implementations.Day3();

    [Theory]
    [InlineData("1234567890", 9)]
    [InlineData("123456111", 6)]
    public void ThenItShouldReturnTheHighestNumber(string input, int expected)
    {
        var (index, value) = _sut.GetHighestNumber(input);

        index.ShouldBe(input.IndexOf(expected.ToString(), StringComparison.Ordinal));
        value.ShouldBe(expected);
    }
}