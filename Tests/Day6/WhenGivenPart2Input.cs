using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day6;

[ExcludeFromCodeCoverage]
public class WhenGivenPart2Input
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
            Numbers = [356, 24, 1],
            Method = Method.Product
        },
        new Problem {
            Numbers = [8, 248, 369],
            Method = Method.Sum
        },
        new Problem {
            Numbers = [175, 581, 32],
            Method = Method.Product
        },
        new Problem {
            Numbers = [4, 431, 623],
            Method = Method.Sum
        },
    ];

    [Fact]
    public void ThenInputIsParsed()
    {
        var problems = _sut.ParsePart2(Input);

        problems.ShouldBeEquivalentTo(expected);
    }
}