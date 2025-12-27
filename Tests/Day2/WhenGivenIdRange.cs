using Domain.Interfaces;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests.Day2;

[ExcludeFromCodeCoverage]
public class WhenGivenIdRange
{
    private readonly IDay2 _sut = new Domain.Implementations.Day2();

    [Theory]
    [InlineData(11, 22, new long[] { 11, 22 })]
    [InlineData(95, 115, new long[] { 99, 111 })]
    [InlineData(998, 1012, new long[] { 999, 1010 })]
    [InlineData(1188511880, 1188511890, new long[] { 1188511885 })]
    [InlineData(222220, 222224, new long[] { 222222 })]
    [InlineData(1698522, 1698528, new long[] { })]
    [InlineData(446443, 446449, new long[] { 446446 })]
    [InlineData(38593856, 38593862, new long[] { 38593859 })]
    public void ReturnsInvalidIds(int start, int end, long[] expectedInvalidIds)
    {
        var idRange = new IdRange(start, end);

        var invalidIds = _sut.GetInvalidIdsFromRange(idRange);

        invalidIds.ShouldBe(expectedInvalidIds);
    }
}