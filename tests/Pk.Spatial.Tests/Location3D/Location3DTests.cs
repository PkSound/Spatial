using MathNet.Spatial.Euclidean;
using Shouldly;
using UnitsNet;
using Xunit;

namespace Pk.Spatial.Tests
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
	public class Location3DTests
	{

		[Fact]
		public void ConstructionUsingDirectionAndMagnitude()
		{
			var locationUnderTest = Location3D.From(UnitVector3D.XAxis, Length.FromMeters(2));

			locationUnderTest.X.Meters.ShouldBe(2);
			locationUnderTest.Y.Meters.ShouldBe(0);
			locationUnderTest.Z.Meters.ShouldBe(0);
		}


		[Fact]
		public void ConstructionUsingLengths()
		{
			var locationUnderTest = new Location3D(Length.FromMeters(3), Length.FromMeters(2), Length.FromMeters(1));
			
			locationUnderTest.X.Meters.ShouldBe(3);
			locationUnderTest.Y.Meters.ShouldBe(2);
			locationUnderTest.Z.Meters.ShouldBe(1);
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
		public void ExposesXYZCoordinatesAsLengths()
		{
			var locationUnderTest = new Location3D();

			locationUnderTest.X.ShouldBe(Length.Zero);
			locationUnderTest.Y.ShouldBe(Length.Zero);
			locationUnderTest.Z.ShouldBe(Length.Zero);
		}
	}
}