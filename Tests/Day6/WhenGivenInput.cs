using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day6;

[ExcludeFromCodeCoverage]
public class WhenGivenInput
{
    private readonly IDay6 _sut = new Domain.Implementations.Day6();
    private const string Input = """
        123 328  51 64 
         45 64  387 23 
          6 98  215 314
        *   +   *   +
        """;
    private readonly Problem[] expected = [
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

    [Fact]
    public void ThenInputParsed()
    {
        var problems = _sut.Parse(Input);

        problems.Length.ShouldBe(4);
        problems.ShouldBeEquivalentTo(expected);
    }
}