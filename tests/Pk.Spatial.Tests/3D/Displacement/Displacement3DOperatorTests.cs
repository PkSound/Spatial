using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using Xunit;

namespace Pk.Spatial.Tests._3D.Displacement
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Displacement3DOperatorTests
  {
    [Fact]
    public void DifferentLengthDisplacementsAreUnequal()
    {
      var displacementUnderTest = new Displacement3D();
      var other = new Displacement3D(1, 0, 0);

      (displacementUnderTest != other).ShouldBeTrue();
      (displacementUnderTest == other).ShouldBeFalse();
    }


    [Fact]
    public void EqualDisplacementsShouldHaveSameHashCode()
    {
      var displacementUnderTest = new Displacement3D();
      var other = new Displacement3D();

      displacementUnderTest.GetHashCode().ShouldBe(other.GetHashCode());
    }


    [Fact]
    public void RotatesAboutAxisByNegativeAngle()
    {
      //Starting from a displacement pointing purely in the X direction
      var displacementUnderTest = new Displacement3D(UnitVector3D.XAxis, Length.FromMeters(1.0));

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
      var displacementUnderTest = new Displacement3D(UnitVector3D.XAxis, Length.FromMeters(1.0));

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
    public void UnequalDisplacementsShouldHaveSameHashCode()
    {
      var displacementUnderTest = new Displacement3D();
      var other = new Displacement3D(1, 0, 0);
      displacementUnderTest.GetHashCode().ShouldNotBe(other.GetHashCode());
    }


    [Fact]
    public void ZeroLengthDisplacementsAreEqual()
    {
      var displacementUnderTest = new Displacement3D();
      var other = new Displacement3D();

      (displacementUnderTest == other).ShouldBeTrue();
      (displacementUnderTest != other).ShouldBeFalse();
    }
  }
}
