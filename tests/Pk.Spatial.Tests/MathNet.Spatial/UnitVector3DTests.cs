using System;
using MathNet.Spatial.Euclidean;
using Pk.Spatial.Tests;
using Shouldly;
using UnitsNet;
using Xunit;

namespace MathNet.Spatial.UnitTests.Euclidean
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class UnitVector3DTests
  {
    const string X = "1; 0 ; 0";
    const string Y = "0; 1; 0";
    const string Z = "0; 0; 1";
    const string NegativeX = "-1; 0; 0";
    const string NegativeY = "0; -1; 0";
    const string NegativeZ = "0; 0; -1";


    [Theory]
    [InlineData("1, 2, 3", "1, 2, 3", 1e-4, true)]
    [InlineData("1, 2, 3", "4, 5, 6", 1e-4, false)]
    public void Equals(string p1s, string p2s, double tol, bool expected)
    {
      var v1 = UnitVector3D.Parse(p1s);
      var v2 = UnitVector3D.Parse(p2s);
      expected.ShouldBe(v1 == v2);
      expected.ShouldBe(v1.Equals(v2));
      expected.ShouldBe(v1.Equals((object) v2));
      expected.ShouldBe(object.Equals(v1, v2));
      expected.ShouldNotBe(v1 != v2);
    }


    [Theory]
    [InlineData("1; 0 ; 0")]
    [InlineData("1; 1 ; 0")]
    [InlineData("1; -1 ; 0")]
    public void Orthogonal(string vs)
    {
      UnitVector3D v = UnitVector3D.Parse(vs);
      UnitVector3D orthogonal = v.Orthogonal;
      (orthogonal.DotProduct(v) < 1e-6).ShouldBeTrue();
    }


   [Theory]
    [InlineData("-2, 0, 1e-4", null, "(-0.99999999875, 0, 4.99999999375E-05)", 1e-4)]
    public void ToString(string vs, string format, string expected, double tolerance)
    {
      var v = UnitVector3D.Parse(vs);
      string actual = v.ToString(format);
      actual.ShouldBe(expected);
      AssertGeometry.AreEqual(v, UnitVector3D.Parse(actual), tolerance);
    }


    [Theory]
    [InlineData(X, Y, Z)]
    [InlineData(X, "1, 1, 0", Z)]
    [InlineData(X, NegativeY, NegativeZ)]
    [InlineData(Y, Z, X)]
    [InlineData(Y, "0.1, 0.1, 1", "1, 0, -0.1")]
    [InlineData(Y, "-0.1, -0.1, 1", "1, 0, 0.1")]
    public void CrossProductTest(string v1s, string v2s, string ves)
    {
      var vector1 = UnitVector3D.Parse(v1s);
      var vector2 = UnitVector3D.Parse(v2s);
      var expected = UnitVector3D.Parse(ves);
      UnitVector3D crossProduct = vector1.CrossProduct(vector2);
      AssertGeometry.AreEqual(expected, crossProduct, 1E-6);
    }


    [Theory]
    [InlineData(X, Y, Z, 90)]
    [InlineData(X, X, Z, 0)]
    [InlineData(X, NegativeY, Z, -90)]
    [InlineData(X, NegativeX, Z, 180)]
    public void SignedAngleToTest(string fromString, string toString, string axisString, double degreeAngle)
    {
      var fromVector = UnitVector3D.Parse(fromString);
      var toVector = UnitVector3D.Parse(toString);
      var aboutVector = UnitVector3D.Parse(axisString);
      degreeAngle.ShouldBe(fromVector.SignedAngleTo(toVector, aboutVector).Degrees, 1E-6);
    }


    [Theory]
    [InlineData("1; 0; 1", Y, "-1; 0; 1", "90°")]
    public void SignedAngleToArbitraryVector(string fromString, string toString, string axisString, string @as)
    {
      var fromVector = UnitVector3D.Parse(fromString);
      var toVector = UnitVector3D.Parse(toString);
      var aboutVector = UnitVector3D.Parse(axisString);
      var angle = Angle.Parse(@as);
      angle.Degrees.ShouldBe(fromVector.SignedAngleTo(toVector, aboutVector).Degrees, 1E-6);
    }


    [Theory]
    [InlineData(X, 5)]
    [InlineData(Y, 5)]
    [InlineData("1; 1; 0", 5)]
    [InlineData("1; 0; 1", 5)]
    [InlineData("0; 1; 1", 5)]
    [InlineData("1; 1; 1", 5)]
    [InlineData(X, 90)]
    [InlineData(Y, 90)]
    [InlineData("1; 1; 0", 90)]
    [InlineData("1; 0; 1", 90)]
    [InlineData("0; 1; 1", 90)]
    [InlineData("1; 1; 1", 90)]
    [InlineData("1; 0; 1", -90)]
    [InlineData("1; 0; 1", 180)]
    [InlineData("1; 0; 1", 0)]
    public void SignedAngleTo_RotationAroundZ(string vectorDoubles, double rotationInDegrees)
    {
      var vector = UnitVector3D.Parse(vectorDoubles);
      Angle angle = Angle.FromDegrees(rotationInDegrees);
      UnitVector3D rotated = new UnitVector3D(Matrix3D.RotationAroundZAxis(angle).Multiply(vector.ToVector()));
      var actual = vector.SignedAngleTo(rotated, UnitVector3D.Parse(Z));
      rotationInDegrees.ShouldBe(actual.Degrees, 1E-6);
    }


    [Theory]
    [InlineData(X, Z, 90, Y)]
    public void RotateTest(string vs, string avs, double deg, string evs)
    {
      var v = UnitVector3D.Parse(vs);
      var aboutvector = UnitVector3D.Parse(avs);
      var rotated = v.Rotate(aboutvector, Angle.FromDegrees(deg));
      var expected = UnitVector3D.Parse(evs);
      AssertGeometry.AreEqual(expected, rotated, 1E-6);
    }


    [Theory]
    [InlineData("X", X)]
    [InlineData("Y", Y)]
    [InlineData("Z", Z)]
    public void SignedAngleTo_Itself(string axisDummy, string aboutDoubles)
    {
      UnitVector3D vector = new UnitVector3D(1, 1, 1);
      UnitVector3D aboutVector = UnitVector3D.Parse(aboutDoubles);
      var angle = vector.SignedAngleTo(vector, aboutVector);
      ((double) 0).ShouldBe(angle.Degrees, 1E-6);
    }


    [Theory]
    [InlineData(X, Y, "90°")]
    [InlineData(Y, X, "90°")]
    [InlineData(X, Z, "90°")]
    [InlineData(Z, X, "90°")]
    [InlineData(Y, Z, "90°")]
    [InlineData(Z, Y, "90°")]
    [InlineData(X, X, "0°")]
    [InlineData(Y, Y, "0°")]
    [InlineData(Z, Z, "0°")]
    [InlineData(X, NegativeY, "90°")]
    [InlineData(Y, NegativeY, "180°")]
    [InlineData(Z, NegativeZ, "180°")]
    [InlineData("1; 1; 0", X, "45°")]
    [InlineData("1; 1; 0", Y, "45°")]
    [InlineData("1; 1; 0", Z, "90°")]
    [InlineData("2; 2; 0", "0; 0; 2", "90°")]
    [InlineData("1; 1; 1", X, "54.74°")]
    [InlineData("1; 1; 1", Y, "54.74°")]
    [InlineData("1; 1; 1", Z, "54.74°")]
    [InlineData("1; 0; 0", "1; 0; 0", "0°")]
    [InlineData("-1; -1; 1", "-1; -1; 1", "0°")]
    [InlineData("1; 1; 1", "-1; -1; -1", "180°")]
    public void AngleToTest(string v1s, string v2s, string ea)
    {
      var v1 = UnitVector3D.Parse(v1s);
      var v2 = UnitVector3D.Parse(v2s);
      var angles = new[]
                   {
                     v1.AngleTo(v2),
                     v2.AngleTo(v1)
                   };
      var expected = Angle.Parse(ea);
      foreach (var angle in angles)
      {
        expected.Radians.ShouldBe(angle.Radians, 1E-2);
      }
    }

    [Theory]
    [InlineData("1, -1, 10", 5, "0.4950737715, -0.4950737715, 4.950737715")]
    public void Scale(string vs, double s, string evs)
    {
      var v = UnitVector3D.Parse(vs);
      Vector3D actual = s*v;
      AssertGeometry.AreEqual(Vector3D.Parse(evs), actual, 1e-6);
    }


    [Theory]
    [InlineData("5;0;0")]
    [InlineData("-5;0;0")]
    [InlineData("-3;0;4")]
    public void Length(string vectorString)
    {
      var vector = UnitVector3D.Parse(vectorString);
      vector.Length.ShouldBe(1.0);
    }


    [Theory]
    [InlineData("1.0 , 2.5,3.3", new[]
                                 {
                                   1,
                                   2.5,
                                   3.3
                                 })]
    [InlineData("1,0 ; 2,5;3,3", new[]
                                 {
                                   1,
                                   2.5,
                                   3.3
                                 })]
    [InlineData("1.0 ; 2.5;3.3", new[]
                                 {
                                   1,
                                   2.5,
                                   3.3
                                 })]
    [InlineData("1.0,2.5,-3.3", new[]
                                {
                                  1,
                                  2.5,
                                  -3.3
                                })]
    [InlineData("1;2;3", new double[]
                         {
                           1,
                           2,
                           3
                         })]
    public void ParseTest(string vs, double[] ep)
    {
      UnitVector3D point3D = UnitVector3D.Parse(vs);
      UnitVector3D expected = new UnitVector3D(ep);
      AssertGeometry.AreEqual(point3D, expected, 1e-9);
    }


    [Theory]
    [InlineData(X, X, true)]
    [InlineData(X, NegativeX, true)]
    [InlineData(Y, Y, true)]
    [InlineData(Y, NegativeY, true)]
    [InlineData(Z, NegativeZ, true)]
    [InlineData(Z, Z, true)]
    [InlineData("1;-8;7", "1;-8;7", true)]
    [InlineData(X, "1;-8;7", false)]
    [InlineData("1;-1.2;0", Z, false)]
    public void IsParallelToTest(string vector1, string vector2, bool isParalell)
    {
      var firstVector = UnitVector3D.Parse(vector1);
      var secondVector = UnitVector3D.Parse(vector2);
      isParalell.ShouldBe(firstVector.IsParallelTo(secondVector, 1E-6));
    }


    [Theory]
    [InlineData("0,1,0", "0,1, 0", 1e-10, true)]
    [InlineData("0,1,0", "0,-1, 0", 1e-10, true)]
    [InlineData("0,1,0", "0,1, 1", 1e-10, false)]
    [InlineData("0,1,1", "0,1, 1", 1e-10, true)]
    [InlineData("0,1,-1", "0,-1, 1", 1e-10, true)]
    [InlineData("0,1,0", "0,1, 0.001", 1e-10, false)]
    [InlineData("0,1,0", "0,1, -0.001", 1e-10, false)]
    [InlineData("0,-1,0", "0,1, 0.001", 1e-10, false)]
    [InlineData("0,-1,0", "0,1, -0.001", 1e-10, false)]
    [InlineData("0,1,0", "0,1, 0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,1,0", "0,1, -0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,-1,0", "0,1, 0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,-1,0", "0,1, -0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,1,0.5", "0,-1, -0.5", 1e-10, true)]
    public void IsParallelToByDoubleTolerance(string v1s, string v2s, double tolerance, bool expected)
    {
      var v1 = UnitVector3D.Parse(v1s);
      var v2 = UnitVector3D.Parse(v2s);
      expected.ShouldBe(v1.IsParallelTo(v2, tolerance));
      expected.ShouldBe(v2.IsParallelTo(v1, tolerance));
    }


    [Theory]
    [InlineData("0,1,0", "0,1, 0", 1e-10, true)]
    [InlineData("0,1,0", "0,-1, 0", 1e-10, true)]
    [InlineData("0,1,0", "0,1, 1", 1e-10, false)]
    [InlineData("0,1,1", "0,1, 1", 1e-10, true)]
    [InlineData("0,1,-1", "0,-1, 1", 1e-10, true)]
    [InlineData("0,1,0", "0,1, 0.001", 1e-10, false)]
    [InlineData("0,1,0", "0,1, -0.001", 1e-10, false)]
    [InlineData("0,-1,0", "0,1, 0.001", 1e-10, false)]
    [InlineData("0,-1,0", "0,1, -0.001", 1e-10, false)]
    [InlineData("0,1,0", "0,1, 0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,1,0", "0,1, -0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,-1,0", "0,1, 0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,-1,0", "0,1, -0.001", 1e-6, true)] // These test cases demonstrate the effect of the tolerance
    [InlineData("0,1,0.5", "0,-1, -0.5", 1e-10, true)]
    public void IsParallelToUnitVectorByDoubleTolerance(string v1s, string v2s, double tolerance, bool expected)
    {
      var v1 = UnitVector3D.Parse(v1s);
      var v2 = UnitVector3D.Parse(v2s);
      expected.ShouldBe(v1.IsParallelTo(v2, tolerance));
      expected.ShouldBe(v2.IsParallelTo(v1, tolerance));
    }


    [Theory]
    [InlineData("0,1,0", "0,1, 0", 1e-4, true)]
    [InlineData("0,1,0", "0,-1, 0", 1e-4, true)]
    [InlineData("0,1,0", "0,1, 1", 1e-4, false)]
    [InlineData("0,1,1", "0,1, 1", 1e-4, true)]
    [InlineData("0,1,-1", "0,-1, 1", 1e-4, true)]
    [InlineData("0,1,0", "0,1, 0.001", 0.06, true)]
    [InlineData("0,1,0", "0,1, -0.001", 0.06, true)]
    [InlineData("0,-1,0", "0,1, 0.001", 0.06, true)]
    [InlineData("0,-1,0", "0,1, -0.001", 0.06, true)]
    [InlineData("0,1,0", "0,1, 0.001", 0.05, false)]
    [InlineData("0,1,0", "0,1, -0.001", 0.05, false)]
    [InlineData("0,-1,0", "0,1, 0.001", 0.05, false)]
    [InlineData("0,-1,0", "0,1, -0.001", 0.05, false)]
    [InlineData("0,1,0.5", "0,-1, -0.5", 1e-4, true)]
    public void IsParallelToByAngleTolerance(string v1s, string v2s, double degreesTolerance, bool expected)
    {
      var v1 = UnitVector3D.Parse(v1s);
      var v2 = UnitVector3D.Parse(v2s);
      expected.ShouldBe(v1.IsParallelTo(v2, Angle.FromDegrees(degreesTolerance)));
      expected.ShouldBe(v2.IsParallelTo(v1, Angle.FromDegrees(degreesTolerance)));
    }


    [Theory]
    [InlineData("0,1,0", "0,1, 0", 1e-4, true)]
    [InlineData("0,1,0", "0,-1, 0", 1e-4, true)]
    [InlineData("0,1,0", "0,1, 1", 1e-4, false)]
    [InlineData("0,1,1", "0,1, 1", 1e-4, true)]
    [InlineData("0,1,-1", "0,-1, 1", 1e-4, true)]
    [InlineData("0,1,0", "0,1, 0.001", 0.06, true)]
    [InlineData("0,1,0", "0,1, -0.001", 0.06, true)]
    [InlineData("0,-1,0", "0,1, 0.001", 0.06, true)]
    [InlineData("0,-1,0", "0,1, -0.001", 0.06, true)]
    [InlineData("0,1,0", "0,1, 0.001", 0.05, false)]
    [InlineData("0,1,0", "0,1, -0.001", 0.05, false)]
    [InlineData("0,-1,0", "0,1, 0.001", 0.05, false)]
    [InlineData("0,-1,0", "0,1, -0.001", 0.05, false)]
    [InlineData("0,1,0.5", "0,-1, -0.5", 1e-4, true)]
    public void IsParallelToUnitVectorByAngleTolerance(string v1s, string v2s, double degreesTolerance, bool expected)
    {
      var v1 = UnitVector3D.Parse(v1s);
      var v2 = UnitVector3D.Parse(v2s);
      expected.ShouldBe(v1.IsParallelTo(v2, Angle.FromDegrees(degreesTolerance)));
      expected.ShouldBe(v2.IsParallelTo(v1, Angle.FromDegrees(degreesTolerance)));
    }


    [Theory]
    [InlineData(X, X, false)]
    [InlineData(NegativeX, X, false)]
    [InlineData("-11;0;0", X, false)]
    [InlineData("1;1;0", X, false)]
    [InlineData(X, Y, true)]
    [InlineData(X, Z, true)]
    [InlineData(Y, X, true)]
    [InlineData(Y, Z, true)]
    [InlineData(Z, Y, true)]
    [InlineData(Z, X, true)]
    public void IsPerpendicularToTest(string v1s, string v2s, bool expected)
    {
      var v1 = UnitVector3D.Parse(v1s);
      var v2 = UnitVector3D.Parse(v2s);
      expected.ShouldBe(v1.IsPerpendicularTo(v2));
    }

    
    [Fact]
    public void Ctor()
    {
      var actuals = new[]
                    {
                      new UnitVector3D(1, 2, 3),
                      new UnitVector3D(new[]
                                   {
                                     1,
                                     2,
                                     3.0
                                   })
                    };
      foreach (var actual in actuals)
      {
        actual.X.ShouldBe(0.2672612419, 1e-6);
        actual.Y.ShouldBe(0.5345224838, 1e-6);
        actual.Z.ShouldBe(0.8017837257, 1e-6);
      }
      Assert.Throws<ArgumentException>(() => new UnitVector3D(new[]
                                                          {
                                                            1.0,
                                                            2,
                                                            3,
                                                            4
                                                          }));
    }


    [Fact]
    public void SignedAngleToBug()
    {
      var ninetyDegAngle = new UnitVector3D(0, 1, 0);
    }


    [Fact]
    public void ToDenseVector()
    {
      var v = new UnitVector3D(1, 2, 3);
      var denseVector = v.ToVector();
      denseVector.Count.ShouldBe(3);

      denseVector[0].ShouldBe(0.2672612419, 1e-6);
      denseVector[1].ShouldBe(0.5345224838, 1e-6);
      denseVector[2].ShouldBe(0.8017837257, 1e-6);}
  }
}
