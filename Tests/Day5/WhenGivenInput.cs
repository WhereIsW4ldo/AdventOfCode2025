using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces;
using Shouldly;
using Xunit;

namespace Tests.Day5;

[ExcludeFromCodeCoverage]
public class WhenGivenInput
{
  private readonly IDay5 _sut = new Domain.Implementations.Day5();
  private const string Input = """
                                3-5
                                10-14
                                16-20
                                12-18


                                1
                                5
                                8
                                11
                                17
                                32
                              """;

  [Fact]
  public void ThenRangesAndIdsAreReturned()
  {
    var (validIds, ids) = _sut.ParseInput(Input);

    validIds.Count.ShouldBe(14);
    ids.Count.ShouldBe(6);
  }
}
