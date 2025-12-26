using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces;
using Shouldly;
using Xunit;

namespace Tests.Day2;

[ExcludeFromCodeCoverage]
public class WhenGivenId
{
    private readonly IDay2 _sut = new Domain.Implementations.Day2();

    [Theory]
    [InlineData(12)]
    [InlineData(4568231)]
    [InlineData(8492456)]
    [InlineData(867321)]
    public void ThenValidIdIsDetected(long id)
    {
        _sut.IsValidId(id).ShouldBeTrue();
    }

    [Theory]
    [InlineData(11)]
    [InlineData(999)]
    [InlineData(1111)]
    [InlineData(222222)]
    [InlineData(824824824)]
    public void ThenInvalidIdIsDetected(long id)
    {
        _sut.IsValidId(id).ShouldBeFalse();
    }
}
