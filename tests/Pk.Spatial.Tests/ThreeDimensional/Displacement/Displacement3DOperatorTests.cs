using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using Xunit;

namespace Pk.Spatial.Tests.ThreeDimensional.Displacement
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Displacement3DOperatorTests
  {
    [Fact]
    public void AngleTo()
    {
      var angle =
          Displacement3D.From(1, 1, 0, StandardUnits.Length).AngleTo(Displacement3D.From(10, 0, 0, StandardUnits.Length));
      angle.Degrees.ShouldBe(45, Tolerance.ToWithinOneHundredth);

      angle = Displacement3D.From(0, 1, 1, StandardUnits.Length).AngleTo(UnitVector3D.ZAxis);
      angle.Degrees.ShouldBe(45, Tolerance.ToWithinOneHundredth);
    }


    [Fact]
    public void DifferentLengthDisplacementsAreUnequal()
    {
      var some = new Displacement3D();
      var other = Displacement3D.From(1, 0, 0, StandardUnits.Length);

      (some != other).ShouldBeTrue();
      (some == other).ShouldBeFalse();
    }


    [Fact]
    public void Displacement3DLessDisplacement3DisADisplacement3D()
    {
      var some = Displacement3D.From(2, 5, 10, StandardUnits.Length);
      var other = Displacement3D.From(3, 7, 8, StandardUnits.Length);

      var result = some + other;
      result.ShouldBeOfType<Displacement3D>();
      result.X.As(StandardUnits.Length).ShouldBe(5);
      result.Y.As(StandardUnits.Length).ShouldBe(12);
      result.Z.As(StandardUnits.Length).ShouldBe(18);
    }


    [Fact]
    public void Displacement3DPlusDisplacement3DisADisplacement3D()
    {
      var some = Displacement3D.From(3, 7, 8, StandardUnits.Length);
      var other = Displacement3D.From(2, 5, 10, StandardUnits.Length);

      var result = some - other;
      result.ShouldBeOfType<Displacement3D>();
      result.X.As(StandardUnits.Length).ShouldBe(1);
      result.Y.As(StandardUnits.Length).ShouldBe(2);
      result.Z.As(StandardUnits.Length).ShouldBe(-2);
    }


    [Fact]
    public void EqualDisplacementsShouldHaveSameHashCode()
    {
      var some = new Displacement3D();
      var other = new Displacement3D();

      some.GetHashCode().ShouldBe(other.GetHashCode());
    }


    [Fact]
    public void EqualityAgainstObject()
    {
      new Displacement3D().Equals(new Location3D()).ShouldBeFalse();

      var d = new Displacement3D();
      d.Equals((object) d).ShouldBeTrue();

      var other = Displacement3D.From(1, 2, 3, StandardUnits.Length);
      d.Equals((object) other).ShouldBeFalse();

      d.Equals(null).ShouldBeFalse();
    }


    [Fact]
    public void RotatesAboutAxisByNegativeAngle()
    {
      //Starting from a displacement pointing purely in the X direction
      var displacementUnderTest = Displacement3D.From(UnitVector3D.XAxis, Length.FromMeters(1.0));

      //Rotation by 90 degrees about z axis gives new vector pointing in Y direction
      var result1 = displacementUnderTest.Rotate(UnitVector3D.ZAxis, Angle.FromDegrees(-90));
      result1.X.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result1.Y.Meters.ShouldBe(-1, Tolerance.ToWithinOneTenth);
      result1.Z.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);

      //Rotation again by 90 degrees about x axis gives new vector pointing in Z direction
      var result2 = result1.Rotate(UnitVector3D.XAxis, Angle.FromDegrees(-90));
      result2.X.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Y.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Z.Meters.ShouldBe(1, Tolerance.ToWithinOneTenth);

      //Finally rotation again by 90 degrees about y axis gives new vector pointing in X direction
      var result3 = result2.Rotate(UnitVector3D.YAxis, Angle.FromDegrees(-90));
      result3.X.Meters.ShouldBe(-1, Tolerance.ToWithinOneTenth);
      result3.Y.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result3.Z.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void RotatesAboutAxisByPositiveAngle()
    {
      //Starting from a displacement pointing purely in the X direction
      var displacementUnderTest = Displacement3D.From(UnitVector3D.XAxis, Length.FromMeters(1.0));

      //Rotation by 90 degrees about z axis gives new vector pointing in Y direction
      var result1 = displacementUnderTest.Rotate(UnitVector3D.ZAxis, Angle.FromDegrees(90));
      result1.X.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result1.Y.Meters.ShouldBe(1, Tolerance.ToWithinOneTenth);
      result1.Z.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);

      //Rotation again by 90 degrees about x axis gives new vector pointing in Z direction
      var result2 = result1.Rotate(UnitVector3D.XAxis, Angle.FromDegrees(90));
      result2.X.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Y.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Z.Meters.ShouldBe(1, Tolerance.ToWithinOneTenth);

      //Finally rotation again by 90 degrees about y axis gives new vector pointing in X direction
      var result3 = result2.Rotate(UnitVector3D.YAxis, Angle.FromDegrees(90));
      result3.X.Meters.ShouldBe(1, Tolerance.ToWithinOneTenth);
      result3.Y.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result3.Z.Meters.ShouldBe(0, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void UnequalDisplacementsShouldHaveDifferentHashCode()
    {
      var some = new Displacement3D();
      var other = Displacement3D.From(1, 0, 0, StandardUnits.Length);
      some.GetHashCode().ShouldNotBe(other.GetHashCode());
    }


    [Fact]
    public void ZeroLengthDisplacementsAreEqual()
    {
      var some = new Displacement3D();
      var other = new Displacement3D();

      (some == other).ShouldBeTrue();
      (some != other).ShouldBeFalse();
    }
  }
}
