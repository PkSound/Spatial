using System;
using Shouldly;
using Xunit;

namespace Pk.Spatial.Tests._3D.Location
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Location3DOperatorTests
  {
    [Fact]
    public void Location3DPlusDisplacement3DisALocation3D()
    {
      var location = new Location3D();
      var displacement = new Displacement3D(4,3,2);

      var result = location + displacement;
      result.ShouldBeOfType<Location3D>();
      result.X.As(StandardUnits.Length).ShouldBe(4);
      result.Y.As(StandardUnits.Length).ShouldBe(3);
      result.Z.As(StandardUnits.Length).ShouldBe(2);
    }

    [Fact]
    public void Location3DLessLocation3DisADisplacement3D()
    {
      var some = new Location3D(1, 1, 1);
      var other = new Location3D(4, 3, 2);

      var result = other - some;
      result.ShouldBeOfType<Displacement3D>();
      result.X.As(StandardUnits.Length).ShouldBe(3);
      result.Y.As(StandardUnits.Length).ShouldBe(2);
      result.Z.As(StandardUnits.Length).ShouldBe(1);
    }


    [Fact]
    public void GivesDisplacementTo()
    {
      var some = new Location3D(1, -1, 10);
      var other = new Location3D(4, 3, 2);

      var result = some.DisplacementTo(other);
      result.ShouldBeOfType<Displacement3D>();
      result.X.As(StandardUnits.Length).ShouldBe(3);
      result.Y.As(StandardUnits.Length).ShouldBe(4);
      result.Z.As(StandardUnits.Length).ShouldBe(-8);

    }


    [Fact]
    public void Location3DLessDisplacement3DisALocation3D()
    {
      var some = new Location3D();
      var other = new Displacement3D(4, 3, 2);

      var result = some - other;
      result.ShouldBeOfType<Location3D>();
      result.X.As(StandardUnits.Length).ShouldBe(-4);
      result.Y.As(StandardUnits.Length).ShouldBe(-3);
      result.Z.As(StandardUnits.Length).ShouldBe(-2);
    }

    [Fact]
    public void DifferentLocationsAreUnequal()
    {
      var some = new Location3D();
      var other = new Location3D(1, 0, 0);

      (some != other).ShouldBeTrue();
      (some == other).ShouldBeFalse();
    }


    [Fact]
    public void EqualLocationsShouldHaveSameHashCode()
    {
      var some = new Location3D();
      var other = new Location3D();

      some.GetHashCode().ShouldBe(other.GetHashCode());
    }
    

    [Fact]
    public void UnequalLocationsShouldHaveDifferentHashCode()
    {
      var some = new Location3D();
      var other = new Location3D(1, 0, 0);
      some.GetHashCode().ShouldNotBe(other.GetHashCode());
    }


    [Fact]
    public void DifferentOriginInstancesAreEqual()
    {
      var some = new Location3D();
      var other = new Location3D();

      (some == other).ShouldBeTrue();
      (some != other).ShouldBeFalse();
    }

  }
}
