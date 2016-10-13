using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using Xunit;

namespace Pk.Spatial.Tests._3D.Displacement
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Displacement3DPropertyTests
  {
    private const double MagnitudeTolerance = 0.00000000000001;
    private const double ComponentTolerance = 0.000001;


    [Fact]
    public void CalculatedMagnitudeProperly()
    {
      var displcementUnderTest = new Displacement3D(5, 5, 5);

      displcementUnderTest.Magnitude.Meters.ShouldBe(8.66025403784439, MagnitudeTolerance);
    }


    [Fact]
    public void ConstructsUsingExplicitLengths()
    {
      var displcementUnderTest = new Displacement3D(Length.FromMeters(1.1), Length.FromMeters(2.2),
                                                    Length.FromMeters(3.3));

      displcementUnderTest.X.Meters.ShouldBe(1.1);
      displcementUnderTest.Y.Meters.ShouldBe(2.2);
      displcementUnderTest.Z.Meters.ShouldBe(3.3);
      displcementUnderTest.Magnitude.Meters.ShouldBe(4.11582312545134, MagnitudeTolerance);
    }


    [Fact]
    public void ConstructsWithDirectionAndMagnitude()
    {
      var displcementUnderTest = new Displacement3D(new UnitVector3D(5, 20, 30.1), Length.FromMeters(1.1));

      displcementUnderTest.X.Meters.ShouldBe(0.150755, ComponentTolerance);
      displcementUnderTest.Y.Meters.ShouldBe(0.60302, ComponentTolerance);
      displcementUnderTest.Z.Meters.ShouldBe(0.907546, ComponentTolerance);
      displcementUnderTest.Magnitude.Meters.ShouldBe(1.1, MagnitudeTolerance);
    }


    [Fact]
    public void DefaultsDoublesToMeters()
    {
      var displcementUnderTest = new Displacement3D(1.1, 2.2, 3.3);

      displcementUnderTest.X.Meters.ShouldBe(1.1);
      displcementUnderTest.Y.Meters.ShouldBe(2.2);
      displcementUnderTest.Z.Meters.ShouldBe(3.3);
      displcementUnderTest.Magnitude.Meters.ShouldBe(4.11582312545134, MagnitudeTolerance);
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
  }
}
