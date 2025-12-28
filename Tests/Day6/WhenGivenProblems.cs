using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day6;

[ExcludeFromCodeCoverage]
public class WhenGivenProblems
{
    private readonly IDay6 _sut = new Domain.Implementations.Day6();
    private readonly Problem[] Input = [
        new Problem {
            Numbers = [123, 45, 6],
            Method = Method.Product
        },
        new Problem {
            Numbers = [328, 64, 98],
            Method = Method.Sum
        },
        new Problem {
            Numbers = [51, 387, 215],
            Method = Method.Product
        },
        new Problem {
            Numbers = [64, 23, 314],
            Method = Method.Sum
        },
    ];
    private const long Expected = 4277556;

    [Fact]
    public void ThenCalculationsAreCorrect()
    {
        var result = _sut.Calculate(Input);

        result.ShouldBe(Expected);
    }
}