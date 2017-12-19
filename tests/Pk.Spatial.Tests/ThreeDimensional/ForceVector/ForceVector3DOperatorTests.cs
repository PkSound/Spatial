using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using Xunit;

namespace Pk.Spatial.Tests.ThreeDimensional.ForceVector
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class ForceVector3DOperatorTests
  {
    [Fact]
    public void AngleTo()
    {
      var angle = ForceVector3D.From(1, 1, 0, Force.BaseUnit).AngleTo(ForceVector3D.From(10, 0, 0, Force.BaseUnit));
      angle.Degrees.ShouldBe(45, Tolerance.ToWithinOneHundredth);

      angle = ForceVector3D.From(0, 1, 1, Force.BaseUnit).AngleTo(UnitVector3D.ZAxis);
      angle.Degrees.ShouldBe(45, Tolerance.ToWithinOneHundredth);
    }


    [Fact]
    public void DifferentForceVectorsAreUnequal()
    {
      var some = new ForceVector3D();
      var other = ForceVector3D.From(1, 0, 0, Force.BaseUnit);

      (some != other).ShouldBeTrue();
      (some == other).ShouldBeFalse();
    }

    [Fact]
    public void DividingByAForceDecreasesMagnitude()
    {
      var forceVector = ForceVector3D.FromNewtons(3, 4, 5);
      var result = forceVector / Force.FromNewtons(4.7);
      result.Magnitude.Newtons.ShouldBe(forceVector.Magnitude.Newtons / 4.7, Tolerance.ToWithinOneHundredth);

      var normalized1 = forceVector.NormalizeToNewtons();
      var normalized2 = result.NormalizeToNewtons();

      normalized2.X.ShouldBe(normalized1.X, Tolerance.ToWithinUnitsNetError);
      normalized2.Y.ShouldBe(normalized1.Y, Tolerance.ToWithinUnitsNetError);
      normalized2.Z.ShouldBe(normalized1.Z, Tolerance.ToWithinUnitsNetError);
    }

    [Fact]
    public void DividingByAScalarDecreasesMagnitude()
    {
      var force = ForceVector3D.FromNewtons(3, 4, 5);
      var result = force/3.2;
      result.Magnitude.Newtons.ShouldBe(force.Magnitude.Newtons/3.2, Tolerance.ToWithinOneHundredth);

      var normalized1 = force.NormalizeToNewtons();
      var normalized2 = result.NormalizeToNewtons();

      normalized2.X.ShouldBe(normalized1.X, Tolerance.ToWithinUnitsNetError);
      normalized2.Y.ShouldBe(normalized1.Y, Tolerance.ToWithinUnitsNetError);
      normalized2.Z.ShouldBe(normalized1.Z, Tolerance.ToWithinUnitsNetError);
    }


    [Fact]
    public void EqualForceVectorsShouldHaveSameHashCode()
    {
      var some = new ForceVector3D();
      var other = new ForceVector3D();

      some.GetHashCode().ShouldBe(other.GetHashCode());
    }


    [Fact]
    public void EqualityAgainstObject()
    {
      new ForceVector3D().Equals(new Location3D()).ShouldBeFalse();

      var d = new ForceVector3D();
      d.Equals((object) d).ShouldBeTrue();

      var other = ForceVector3D.From(1, 2, 3, Force.BaseUnit);
      d.Equals((object) other).ShouldBeFalse();

      d.Equals(null).ShouldBeFalse();
    }


    [Fact]
    public void ForceVector3DLessForceVector3DisAForceVector3D()
    {
      var some = ForceVector3D.From(3, 7, 8, Force.BaseUnit);
      var other = ForceVector3D.From(2, 5, 10, Force.BaseUnit);

      var result = some - other;
      result.ShouldBeOfType<ForceVector3D>();
      result.X.As(Force.BaseUnit).ShouldBe(1);
      result.Y.As(Force.BaseUnit).ShouldBe(2);
      result.Z.As(Force.BaseUnit).ShouldBe(-2);
    }


    [Fact]
    public void ForceVector3DPlusForceVector3DisAForceVector3D()
    {
      var some = ForceVector3D.From(2, 5, 10, Force.BaseUnit);
      var other = ForceVector3D.From(3, 7, 8, Force.BaseUnit);

      var result = some + other;
      result.ShouldBeOfType<ForceVector3D>();
      result.X.As(Force.BaseUnit).ShouldBe(5);
      result.Y.As(Force.BaseUnit).ShouldBe(12);
      result.Z.As(Force.BaseUnit).ShouldBe(18);
    }


    [Fact]
    public void MultiplyingByAForceIncreasesMagnitude()
    {
      var forceVector = ForceVector3D.FromNewtons(3, 4, 5);
      var result = Force.FromNewtons(9.2) * forceVector;
      result.Magnitude.Newtons.ShouldBe(forceVector.Magnitude.Newtons * 9.2, Tolerance.ToWithinOneHundredth);

      var normalized1 = forceVector.NormalizeToNewtons();
      var normalized2 = result.NormalizeToNewtons();

      normalized2.X.ShouldBe(normalized1.X, Tolerance.ToWithinUnitsNetError);
      normalized2.Y.ShouldBe(normalized1.Y, Tolerance.ToWithinUnitsNetError);
      normalized2.Z.ShouldBe(normalized1.Z, Tolerance.ToWithinUnitsNetError);
    }

    [Fact]
    public void MultiplyingByAScalarIncreasesMagnitude()
    {
      var force = ForceVector3D.FromNewtons(3, 4, 5);
      var result = 7.5*force;
      result.Magnitude.Newtons.ShouldBe(force.Magnitude.Newtons*7.5, Tolerance.ToWithinOneHundredth);

      var normalized1 = force.NormalizeToNewtons();
      var normalized2 = result.NormalizeToNewtons();

      normalized2.X.ShouldBe(normalized1.X, Tolerance.ToWithinUnitsNetError);
      normalized2.Y.ShouldBe(normalized1.Y, Tolerance.ToWithinUnitsNetError);
      normalized2.Z.ShouldBe(normalized1.Z, Tolerance.ToWithinUnitsNetError);
    }


    [Fact]
    public void NegationInvertsSignOfXYZ()
    {
      var forceVector = ForceVector3D.FromNewtons(3, -4, 5);
      var result = forceVector.Negate();
      result.X.Newtons.ShouldBe(-3);
      result.Y.Newtons.ShouldBe(4);
      result.Z.Newtons.ShouldBe(-5);
      result.Magnitude.Newtons.ShouldBe(forceVector.Magnitude.Newtons, Tolerance.ToWithinUnitsNetError);

      var forceVector2 = ForceVector3D.FromNewtons(-5, 4, 3);
      var result2 = -forceVector2;
      result2.X.Newtons.ShouldBe(5);
      result2.Y.Newtons.ShouldBe(-4);
      result2.Z.Newtons.ShouldBe(-3);
      result2.Magnitude.Newtons.ShouldBe(forceVector2.Magnitude.Newtons, Tolerance.ToWithinUnitsNetError);
    }


    [Fact]
    public void RotatesAboutAxisByNegativeAngle()
    {
      //Starting from a ForceVector pointing purely in the X direction
      var forceVectorUnderTest = ForceVector3D.From(UnitVector3D.XAxis, Force.FromNewtons(1.0));

      //Rotation by 90 degrees about z axis gives new vector pointing in Y direction
      var result1 = forceVectorUnderTest.Rotate(UnitVector3D.ZAxis, Angle.FromDegrees(-90));
      result1.X.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result1.Y.Newtons.ShouldBe(-1, Tolerance.ToWithinOneTenth);
      result1.Z.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);

      //Rotation again by 90 degrees about x axis gives new vector pointing in Z direction
      var result2 = result1.Rotate(UnitVector3D.XAxis, Angle.FromDegrees(-90));
      result2.X.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Y.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Z.Newtons.ShouldBe(1, Tolerance.ToWithinOneTenth);

      //Finally rotation again by 90 degrees about y axis gives new vector pointing in X direction
      var result3 = result2.Rotate(UnitVector3D.YAxis, Angle.FromDegrees(-90));
      result3.X.Newtons.ShouldBe(-1, Tolerance.ToWithinOneTenth);
      result3.Y.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result3.Z.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void RotatesAboutAxisByPositiveAngle()
    {
      //Starting from a ForceVector pointing purely in the X direction
      var forceVectorUnderTest = ForceVector3D.From(UnitVector3D.XAxis, Force.FromPoundsForce(1.0));

      //Rotation by 90 degrees about z axis gives new vector pointing in Y direction
      var result1 = forceVectorUnderTest.Rotate(UnitVector3D.ZAxis, Angle.FromDegrees(90));
      result1.X.PoundsForce.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result1.Y.PoundsForce.ShouldBe(1, Tolerance.ToWithinOneTenth);
      result1.Z.PoundsForce.ShouldBe(0, Tolerance.ToWithinOneTenth);

      //Rotation again by 90 degrees about x axis gives new vector pointing in Z direction
      var result2 = result1.Rotate(UnitVector3D.XAxis, Angle.FromDegrees(90));
      result2.X.PoundsForce.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Y.PoundsForce.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result2.Z.PoundsForce.ShouldBe(1, Tolerance.ToWithinOneTenth);

      //Finally rotation again by 90 degrees about y axis gives new vector pointing in X direction
      var result3 = result2.Rotate(UnitVector3D.YAxis, Angle.FromDegrees(90));
      result3.X.PoundsForce.ShouldBe(1, Tolerance.ToWithinOneTenth);
      result3.Y.PoundsForce.ShouldBe(0, Tolerance.ToWithinOneTenth);
      result3.Z.PoundsForce.ShouldBe(0, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void UnequalForceVectorsShouldHaveDifferentHashCode()
    {
      var some = new ForceVector3D();
      var other = ForceVector3D.From(1, 0, 0, Force.BaseUnit);
      some.GetHashCode().ShouldNotBe(other.GetHashCode());
    }


    [Fact]
    public void ZeroForceVectorsAreEqual()
    {
      var some = new ForceVector3D();
      var other = new ForceVector3D();

      (some == other).ShouldBeTrue();
      (some != other).ShouldBeFalse();
    }
  }
}
