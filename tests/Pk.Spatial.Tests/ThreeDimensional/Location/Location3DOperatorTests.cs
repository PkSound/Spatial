using Shouldly;
using UnitsNet;
using Xunit;

namespace Pk.Spatial.Tests.ThreeDimensional.Location
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Location3DOperatorTests
  {
    [Fact]
    public void DifferentLocationsAreUnequal()
    {
      var some = new Location3D();
      var other = Location3D.From(1, 0, 0, Length.BaseUnit);

      (some != other).ShouldBeTrue();
      (some == other).ShouldBeFalse();
    }


    [Fact]
    public void DifferentOriginInstancesAreEqual()
    {
      var some = new Location3D();
      var other = new Location3D();

      (some == other).ShouldBeTrue();
      (some != other).ShouldBeFalse();
    }


    [Fact]
    public void EqualityAgainstObject()
    {
      new Location3D().Equals(new Displacement3D()).ShouldBeFalse();

      var d = new Location3D();
      d.Equals((object) d).ShouldBeTrue();

      var other = Location3D.From(1, 2, 3, Length.BaseUnit);
      d.Equals((object) other).ShouldBeFalse();

      d.Equals(null).ShouldBeFalse();
    }


    [Fact]
    public void EqualLocationsShouldHaveSameHashCode()
    {
      var some = new Location3D();
      var other = new Location3D();

      some.GetHashCode().ShouldBe(other.GetHashCode());
    }


    [Fact]
    public void GivesDisplacementTo()
    {
      var some = Location3D.From(1, -1, 10, Length.BaseUnit);
      var other = Location3D.From(4, 3, 2, Length.BaseUnit);

      var result = some.DisplacementTo(other);
      result.ShouldBeOfType<Displacement3D>();
      result.X.As(Length.BaseUnit).ShouldBe(3);
      result.Y.As(Length.BaseUnit).ShouldBe(4);
      result.Z.As(Length.BaseUnit).ShouldBe(-8);
    }


    [Fact]
    public void Location3DLessDisplacement3DisALocation3D()
    {
      var some = new Location3D();
      var other = Displacement3D.From(4, 3, 2, Length.BaseUnit);

      var result = some - other;
      result.ShouldBeOfType<Location3D>();
      result.X.As(Length.BaseUnit).ShouldBe(-4);
      result.Y.As(Length.BaseUnit).ShouldBe(-3);
      result.Z.As(Length.BaseUnit).ShouldBe(-2);
    }


    [Fact]
    public void Location3DLessLocation3DisADisplacement3D()
    {
      var some = Location3D.From(1, 1, 1, Length.BaseUnit);
      var other = Location3D.From(4, 3, 2, Length.BaseUnit);

      var result = other - some;
      result.ShouldBeOfType<Displacement3D>();
      result.X.As(Length.BaseUnit).ShouldBe(3);
      result.Y.As(Length.BaseUnit).ShouldBe(2);
      result.Z.As(Length.BaseUnit).ShouldBe(1);
    }


    [Fact]
    public void Location3DPlusDisplacement3DisALocation3D()
    {
      var location = new Location3D();
      var displacement = Displacement3D.From(4, 3, 2, Length.BaseUnit);

      var result = location + displacement;
      result.ShouldBeOfType<Location3D>();
      result.X.As(Length.BaseUnit).ShouldBe(4);
      result.Y.As(Length.BaseUnit).ShouldBe(3);
      result.Z.As(Length.BaseUnit).ShouldBe(2);
    }


    [Fact]
    public void UnequalLocationsShouldHaveDifferentHashCode()
    {
      var some = new Location3D();
      var other = Location3D.From(1, 0, 0, Length.BaseUnit);
      some.GetHashCode().ShouldNotBe(other.GetHashCode());
    }
  }
}
