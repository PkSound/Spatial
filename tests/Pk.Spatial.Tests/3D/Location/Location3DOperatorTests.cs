using System;
using Shouldly;
using Xunit;

namespace Pk.Spatial.Tests._3D.Location
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Location3DOperatorTests
  {
    [Fact]
    public void Location3DPlusDisplacement3DisALocation3D() { throw new NotImplementedException(); }


    [Fact]
    public void DifferentLocationsAreUnequal()
    {
      var locationUnderTest = new Location3D();
      var other = new Location3D(1, 0, 0);

      (locationUnderTest != other).ShouldBeTrue();
      (locationUnderTest == other).ShouldBeFalse();
    }


    [Fact]
    public void EqualLocationsShouldHaveSameHashCode()
    {
      var locationUnderTest = new Location3D();
      var other = new Location3D();

      locationUnderTest.GetHashCode().ShouldBe(other.GetHashCode());
    }
    

    [Fact]
    public void UnequalLocationsShouldHaveSameHashCode()
    {
      var locationUnderTest = new Location3D();
      var other = new Location3D(1, 0, 0);
      locationUnderTest.GetHashCode().ShouldNotBe(other.GetHashCode());
    }


    [Fact]
    public void DifferentOriginInstancesAreEqual()
    {
      var locationUnderTest = new Location3D();
      var other = new Location3D();

      (locationUnderTest == other).ShouldBeTrue();
      (locationUnderTest != other).ShouldBeFalse();
    }

  }
}
