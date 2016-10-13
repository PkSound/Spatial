using System;
using Shouldly;
using Xunit;

namespace Pk.Spatial.Tests.Displacement3D
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Displacement3DOperatorTests
  {
    [Fact]
    public void RotatesAboutAxisByAngle() { throw new NotImplementedException(); }


    [Fact]
    public void ZeroLengthDisplacementsAreAllEqual()
    {
      var displacementUnderTest = new Spatial.Displacement3D();
      var other = new Spatial.Displacement3D();

      (displacementUnderTest == other).ShouldBeTrue();
    }
  }
}
