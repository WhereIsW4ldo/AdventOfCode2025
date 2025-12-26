using Domain.Interfaces;
using Shouldly;
using Xunit;

namespace Tests.Day1;

public class WhenStarting
{
    private readonly IDay1 _sut = new Domain.Implementations.Day1();

    [Fact]
    public void ShouldBeAt_50()
    {
        _sut.GetCurrentValue().ShouldBe(50);
    }
}
