using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace Pk.Spatial.Tests.ThreeDimensional.Location
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Location3DPropertyTests
  {
    [Fact]
    public void ConstructionUsingLengths()
    {
      var locationUnderTest = new Location3D(Length.FromMeters(3), Length.FromMeters(2), Length.FromMeters(1));

      locationUnderTest.X.Meters.ShouldBe(3);
      locationUnderTest.Y.Meters.ShouldBe(2);
      locationUnderTest.Z.Meters.ShouldBe(1);
    }


    [Fact]
    public void GetsDisplacementFromOrigin()
    {
      var locationUnderTest = Location3D.From(3, 80, -92, Length.BaseUnit);

      var displacementFromOrigin = locationUnderTest.DisplacementFromOrigin();
      displacementFromOrigin.X.As(Length.BaseUnit).ShouldBe(3);
      displacementFromOrigin.Y.As(Length.BaseUnit).ShouldBe(80);
      displacementFromOrigin.Z.As(Length.BaseUnit).ShouldBe(-92);
    }


    [Fact]
    public void GetsPoint3DOfDesiredUnits()
    {
      var locationUnderTest = Location3D.From(10.0, 30.1, -85.0, LengthUnit.Kilometer);

      var result = locationUnderTest.FreezeTo(LengthUnit.Mile);
      result.X.ShouldBe(6.2, Tolerance.ToWithinOneTenth);
      result.Y.ShouldBe(18.7, Tolerance.ToWithinOneTenth);
      result.Z.ShouldBe(-52.8, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void OriginIsAt000()
    {
      var locationUnderTest = Location3D.Origin;

      locationUnderTest.X.ShouldBe(Length.Zero);
      locationUnderTest.Y.ShouldBe(Length.Zero);
      locationUnderTest.Z.ShouldBe(Length.Zero);
    }


    [Fact]
    public void StaticFactoryMethodChecks()
    {
      var locationUnderTest = Location3D.Origin;

      locationUnderTest.X.Meters.ShouldBe(0.0);
      locationUnderTest.Y.Meters.ShouldBe(0.0);
      locationUnderTest.Z.Meters.ShouldBe(0.0);

      locationUnderTest = Location3D.FromMeters(1.1, 2.2, 3.3);

      locationUnderTest.X.Meters.ShouldBe(1.1);
      locationUnderTest.Y.Meters.ShouldBe(2.2);
      locationUnderTest.Z.Meters.ShouldBe(3.3);

      locationUnderTest = Location3D.FromMeters(new Point3D(1.1, 2.2, 3.3));

      locationUnderTest.X.Meters.ShouldBe(1.1);
      locationUnderTest.Y.Meters.ShouldBe(2.2);
      locationUnderTest.Z.Meters.ShouldBe(3.3);

      locationUnderTest = Location3D.From(new Point3D(5.0, 6.0, 7.0), LengthUnit.Centimeter);

      locationUnderTest.X.As(LengthUnit.Decimeter).ShouldBe(0.5, Tolerance.ToWithinOneHundredth);
      locationUnderTest.Y.As(LengthUnit.Decimeter).ShouldBe(0.6, Tolerance.ToWithinOneHundredth);
      locationUnderTest.Z.As(LengthUnit.Decimeter).ShouldBe(0.7, Tolerance.ToWithinOneHundredth);
    }
  }
}
