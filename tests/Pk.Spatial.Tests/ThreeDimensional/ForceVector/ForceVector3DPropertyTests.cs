using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace Pk.Spatial.Tests.ThreeDimensional.ForceVector
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class ForceVector3DPropertyTests
  {
    [Fact]
    public void CheckConstructionMethods()
    {
      var vectorUnderTest = new ForceVector3D(Force.FromNewtons(1.1), Force.FromNewtons(2.2), Force.FromNewtons(3.3));

      vectorUnderTest.X.Newtons.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Y.Newtons.ShouldBe(2.2, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Z.Newtons.ShouldBe(3.3, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Magnitude.Newtons.ShouldBe(4.1, Tolerance.ToWithinOneTenth);

      vectorUnderTest = ForceVector3D.From(3.3, 1.1, 2.2, ForceUnit.PoundForce);

      vectorUnderTest.X.PoundsForce.ShouldBe(3.3, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Y.PoundsForce.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Z.PoundsForce.ShouldBe(2.2, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Magnitude.PoundsForce.ShouldBe(4.1, Tolerance.ToWithinOneTenth);

      vectorUnderTest = ForceVector3D.FromNewtons(3, 4, 0);

      vectorUnderTest.X.Newtons.ShouldBe(3, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Y.Newtons.ShouldBe(4, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Z.Newtons.ShouldBe(0, Tolerance.ToWithinOneTenth);
      vectorUnderTest.Magnitude.Newtons.ShouldBe(5, Tolerance.ToWithinOneTenth);

      vectorUnderTest = ForceVector3D.From(new UnitVector3D(5.0, 20.0, 30.1), Force.FromKiloPonds(1.1));

      vectorUnderTest.Magnitude.KiloPonds.ShouldBe(1.1, Tolerance.ToWithinOneTenth);
      vectorUnderTest.X.KiloPonds.ShouldBe(0.150755, Tolerance.ToWithinOneHundredth);
      vectorUnderTest.Y.KiloPonds.ShouldBe(0.60302, Tolerance.ToWithinOneHundredth);
      vectorUnderTest.Z.KiloPonds.ShouldBe(0.907546, Tolerance.ToWithinOneHundredth);

      vectorUnderTest = ForceVector3D.From(new Vector3D(5.0, 6.0, 7.0), ForceUnit.Kilonewton);

      vectorUnderTest.X.Kilonewtons.ShouldBe(5, Tolerance.ToWithinUnitsNetError);
      vectorUnderTest.Y.Kilonewtons.ShouldBe(6, Tolerance.ToWithinUnitsNetError);
      vectorUnderTest.Z.Kilonewtons.ShouldBe(7, Tolerance.ToWithinUnitsNetError);
    }


    [Fact]
    public void DefaultsToNoMagnitude()
    {
      var vectorUnderTest = new ForceVector3D();

      vectorUnderTest.X.Newtons.ShouldBe(0, Tolerance.ToWithinUnitsNetError);
      vectorUnderTest.Y.Newtons.ShouldBe(0, Tolerance.ToWithinUnitsNetError);
      vectorUnderTest.Z.Newtons.ShouldBe(0, Tolerance.ToWithinUnitsNetError);
      vectorUnderTest.Magnitude.Newtons.ShouldBe(0, Tolerance.ToWithinUnitsNetError);
    }


    [Fact]
    public void GetsVector3DOfDesiredUnits()
    {
      var vectorUnderTest = ForceVector3D.From(10.0, 30.1, -85.0, ForceUnit.Kilonewton);

      var result = vectorUnderTest.FreezeTo(ForceUnit.Newton);

      result.X.ShouldBe(10000.0, Tolerance.ToWithinUnitsNetError);
      result.Y.ShouldBe(30100.0, Tolerance.ToWithinUnitsNetError);
      result.Z.ShouldBe(-85000.0, Tolerance.ToWithinUnitsNetError);
    }
  }
}
