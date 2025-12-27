using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day2;

[ExcludeFromCodeCoverage]
public class WhenGivenInput
{
    private readonly IDay2 _sut = new Domain.Implementations.Day2();
    private const string Input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

    [Fact]
    public void ShouldReturnCorrectRanges()
    {
        var ranges = _sut.GetRanges(Input);

        ranges.Length.ShouldBe(11);

        ranges[0].ShouldBe(new IdRange(11, 22));
        ranges[1].ShouldBe(new IdRange(95, 115));
        ranges[2].ShouldBe(new IdRange(998, 1012));
        ranges[3].ShouldBe(new IdRange(1188511880, 1188511890));
        ranges[4].ShouldBe(new IdRange(222220, 222224));
        ranges[5].ShouldBe(new IdRange(1698522, 1698528));
        ranges[6].ShouldBe(new IdRange(446443, 446449));
        ranges[7].ShouldBe(new IdRange(38593856, 38593862));
        ranges[8].ShouldBe(new IdRange(565653, 565659));
        ranges[9].ShouldBe(new IdRange(824824821, 824824827));
        ranges[10].ShouldBe(new IdRange(2121212118, 2121212124));
    }
}