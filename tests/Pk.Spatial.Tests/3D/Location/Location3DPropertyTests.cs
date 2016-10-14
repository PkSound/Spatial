using Shouldly;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace Pk.Spatial.Tests._3D.Location
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
    public void ConstructsUsingDoublesAndALengthUnit()
    {
      var locationUnderTest = new Location3D(1.1, 2.2, 3.3, LengthUnit.Mile);

      locationUnderTest.X.Miles.ShouldBe(1.1);
      locationUnderTest.Y.Miles.ShouldBe(2.2);
      locationUnderTest.Z.Miles.ShouldBe(3.3);
    }

    [Fact]
    public void GetsPoint3DOfDesiredUnits()
    {
      var locationUnderTest = new Location3D(10.0, 30.1, -85.0, LengthUnit.Kilometer);

      var result = locationUnderTest.Freeze(LengthUnit.Mile);
      result.X.ShouldBe(6.2, Tolerance.ToWithinOneTenth);
      result.Y.ShouldBe(18.7, Tolerance.ToWithinOneTenth);
      result.Z.ShouldBe(-52.8, Tolerance.ToWithinOneTenth);
    }


    [Fact]
    public void DefaultsDoublesToStandardLengthUnit()
    {
      var locationUnderTest = new Location3D(1.1, 2.2, 3.3);

      locationUnderTest.X.As(StandardUnits.Length).ShouldBe(1.1);
      locationUnderTest.Y.As(StandardUnits.Length).ShouldBe(2.2);
      locationUnderTest.Z.As(StandardUnits.Length).ShouldBe(3.3);
    }


    [Fact]
    public void OriginIsAt000()
    {
      var locationUnderTest = Location3D.Origin;

      locationUnderTest.X.ShouldBe(Length.Zero);
      locationUnderTest.Y.ShouldBe(Length.Zero);
      locationUnderTest.Z.ShouldBe(Length.Zero);
    }
  }
}
