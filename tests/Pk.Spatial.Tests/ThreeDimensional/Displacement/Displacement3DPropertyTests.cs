using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace Pk.Spatial.Tests.ThreeDimensional.Displacement
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Displacement3DPropertyTests
  {
    [Fact]
    public void CheckConstructionMethods()
    {
      var displacementUnderTest = new Displacement3D(Length.FromFeet(1.1), Length.FromFeet(2.2), Length.FromFeet(3.3));

      displacementUnderTest.X.Feet.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Y.Feet.ShouldBe(2.2, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Z.Feet.ShouldBe(3.3, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Magnitude.Feet.ShouldBe(4.1, Tolerance.ToWithinOneTenth);

      displacementUnderTest = Displacement3D.From(1.1, 2.2, 3.3, LengthUnit.Mile);

      displacementUnderTest.X.Miles.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Y.Miles.ShouldBe(2.2, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Z.Miles.ShouldBe(3.3, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Magnitude.Miles.ShouldBe(4.1, Tolerance.ToWithinOneTenth);

      displacementUnderTest = Displacement3D.FromMeters(1.1, 2.2, 3.3);

      displacementUnderTest.X.Meters.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Y.Meters.ShouldBe(2.2, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Z.Meters.ShouldBe(3.3, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Magnitude.Meters.ShouldBe(4.1, Tolerance.ToWithinOneTenth);

      displacementUnderTest = Displacement3D.From(new UnitVector3D(5.0, 20.0, 30.1), Length.FromMeters(1.1));

      displacementUnderTest.X.Meters.ShouldBe(0.2, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Y.Meters.ShouldBe(0.6, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Z.Meters.ShouldBe(0.9, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Magnitude.Meters.ShouldBe(1.1, Tolerance.ToWithinOneTenth);

      displacementUnderTest = Displacement3D.From(new Vector3D(5.0, 6.0, 7.0), LengthUnit.Centimeter);

      displacementUnderTest.X.As(LengthUnit.Decimeter).ShouldBe(0.5, Tolerance.ToWithinOneHundredth);
      displacementUnderTest.Y.As(LengthUnit.Decimeter).ShouldBe(0.6, Tolerance.ToWithinOneHundredth);
      displacementUnderTest.Z.As(LengthUnit.Decimeter).ShouldBe(0.7, Tolerance.ToWithinOneHundredth);
    }


    [Fact]
    public void DefaultsToNoMagnitude()
    {
      var displacementUnderTest = new Displacement3D();

      displacementUnderTest.Magnitude.ShouldBe(Length.Zero);
      displacementUnderTest.X.ShouldBe(Length.Zero);
      displacementUnderTest.Y.ShouldBe(Length.Zero);
      displacementUnderTest.Z.ShouldBe(Length.Zero);
    }


    [Fact]
    public void GetsVector3DOfDesiredUnits()
    {
      var displacementUnderTest = Displacement3D.From(10.0, 30.1, -85.0, LengthUnit.Kilometer);

      var result = displacementUnderTest.FreezeTo(LengthUnit.Mile);
      result.X.ShouldBe(6.2, Tolerance.ToWithinOneTenth);
      result.Y.ShouldBe(18.7, Tolerance.ToWithinOneTenth);
      result.Z.ShouldBe(-52.8, Tolerance.ToWithinOneTenth);
    }
  }
}
