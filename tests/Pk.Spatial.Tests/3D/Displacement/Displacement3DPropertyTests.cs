using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace Pk.Spatial.Tests._3D.Displacement
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Displacement3DPropertyTests
  {

    [Fact]
    public void CalculatedMagnitudeProperly()
    {
      var displacementUnderTest = new Displacement3D(5.0, 5.0, 5.0, StandardUnits.Length);

      displacementUnderTest.Magnitude.As(StandardUnits.Length).ShouldBe(8.7, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void ConstructUsingVector3D()
    {
      var locationUnderTest = new Displacement3D(new Vector3D(5.0, 6.0, 7.0), LengthUnit.Centimeter);

      locationUnderTest.X.As(LengthUnit.Decimeter).ShouldBe(0.5, Tolerance.ToWithinOneHundredth);
      locationUnderTest.Y.As(LengthUnit.Decimeter).ShouldBe(0.6, Tolerance.ToWithinOneHundredth);
      locationUnderTest.Z.As(LengthUnit.Decimeter).ShouldBe(0.7, Tolerance.ToWithinOneHundredth);
    }

    [Fact]
    public void ConstructsUsingDoublesAndALengthUnit()
    {
      var displacementUnderTest = new Displacement3D(1.1, 2.2, 3.3, LengthUnit.Mile);

      displacementUnderTest.X.Miles.ShouldBe(1.1);
      displacementUnderTest.Y.Miles.ShouldBe(2.2);
      displacementUnderTest.Z.Miles.ShouldBe(3.3);
      displacementUnderTest.Magnitude.Miles.ShouldBe(4.1, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void ConstructsUsingLengths()
    {
      var displacementUnderTest = new Displacement3D(Length.FromMeters(1.1), Length.FromMeters(2.2),
                                                    Length.FromMeters(3.3));

      displacementUnderTest.X.Meters.ShouldBe(1.1);
      displacementUnderTest.Y.Meters.ShouldBe(2.2);
      displacementUnderTest.Z.Meters.ShouldBe(3.3);
      displacementUnderTest.Magnitude.Meters.ShouldBe(4.1, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void ConstructsWithDirectionAndMagnitude()
    {
      var displacementUnderTest = new Displacement3D(new UnitVector3D(5.0, 20.0, 30.1), Length.FromMeters(1.1));

      displacementUnderTest.X.Meters.ShouldBe(0.2, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Y.Meters.ShouldBe(0.6, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Z.Meters.ShouldBe(0.9, Tolerance.ToWithinOneTenth);
      displacementUnderTest.Magnitude.Meters.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void DefaultsDoublesToStandardLengthUnit()
    {
      var displacementUnderTest = new Displacement3D(1.1, 2.2, 3.3, StandardUnits.Length);

      displacementUnderTest.X.As(StandardUnits.Length).ShouldBe(1.1);
      displacementUnderTest.Y.As(StandardUnits.Length).ShouldBe(2.2);
      displacementUnderTest.Z.As(StandardUnits.Length).ShouldBe(3.3);
      displacementUnderTest.Magnitude.As(StandardUnits.Length).ShouldBe(4.1, Tolerance.ToWithinOneTenth);
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
      var displacementUnderTest = new Displacement3D(10.0, 30.1, -85.0, LengthUnit.Kilometer);

      var result = displacementUnderTest.FreezeTo(LengthUnit.Mile);
      result.X.ShouldBe(6.2, Tolerance.ToWithinOneTenth);
      result.Y.ShouldBe(18.7, Tolerance.ToWithinOneTenth);
      result.Z.ShouldBe(-52.8, Tolerance.ToWithinOneTenth);
    }
  }
}
