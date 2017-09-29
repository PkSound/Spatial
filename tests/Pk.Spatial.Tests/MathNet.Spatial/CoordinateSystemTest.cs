using System;
using MathNet.Spatial.Euclidean;
using Pk.Spatial.Tests;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace MathNet.Spatial.UnitTests.Euclidean
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class CoordinateSystemTest
  {
    const string X = "1; 0 ; 0";
    const string Y = "0; 1; 0";
    const string Z = "0; 0; 1";
    const string NegativeX = "-1; 0; 0";
    const string NegativeY = "0; -1; 0";
    const string NegativeZ = "0; 0; -1";
    private const string ZeroPoint = "0; 0; 0";


    [Theory]
    [InlineData("1, 2, 3", "4, 5, 6", "7, 8, 9", "-1, -2, -3")]
    public void ConstructorTest(string ps, string xs, string ys, string zs)
    {
      var origin = Point3D.Parse(ps);
      var xAxis = Vector3D.Parse(xs);
      var yAxis = Vector3D.Parse(ys);
      var zAxis = Vector3D.Parse(zs);
      var css = new[]
                {
                  new CoordinateSystem(origin, xAxis, yAxis, zAxis),
                  new CoordinateSystem(xAxis, yAxis, zAxis, origin)
                };
      foreach (var cs in css)
      {
        AssertGeometry.AreEqual(origin, cs.Origin);
        AssertGeometry.AreEqual(xAxis, cs.XAxis);
        AssertGeometry.AreEqual(yAxis, cs.YAxis);
        AssertGeometry.AreEqual(zAxis, cs.ZAxis);
      }
    }


    [Theory]
    [InlineData("o:{1, 2e-6, -3} x:{1, 2, 3} y:{3, 3, 3} z:{4, 4, 4}", new[]
                                                                       {
                                                                         1,
                                                                         2e-6,
                                                                         -3
                                                                       }, new double[]
                                                                          {
                                                                            1,
                                                                            2,
                                                                            3
                                                                          }, new double[]
                                                                             {
                                                                               3,
                                                                               3,
                                                                               3
                                                                             }, new double[]
                                                                                {
                                                                                  4,
                                                                                  4,
                                                                                  4
                                                                                })]
    public void ParseTests(string s, double[] ops, double[] xs, double[] ys, double[] zs)
    {
      var cs = CoordinateSystem.Parse(s);
      AssertGeometry.AreEqual(new Point3D(ops), cs.Origin);
      AssertGeometry.AreEqual(new Vector3D(xs), cs.XAxis);
      AssertGeometry.AreEqual(new Vector3D(ys), cs.YAxis);
      AssertGeometry.AreEqual(new Vector3D(zs), cs.ZAxis);
    }


    [Theory]
    [InlineData("1, 2, 3", "90°", "0, 0, 1", "-2, 1, 3")]
    [InlineData("1, 2, 3", "90°", "0, 0, -1", "2, -1, 3")]
    [InlineData("1, 2, 3", "-90°", "0, 0, 1", "2, -1, 3")]
    [InlineData("1, 2, 3", "180°", "0, 0, 1", "-1, -2, 3")]
    [InlineData("1, 2, 3", "270°", "0, 0, 1", "2, -1, 3")]
    [InlineData("1, 2, 3", "90°", "1, 0, 0", "1, -3, 2")]
    [InlineData("1, 2, 3", "-90°", "1, 0, 0", "1, 3, -2")]
    [InlineData("1, 2, 3", "90°", "-1, 0, 0", "1, 3, -2")]
    [InlineData("1, 2, 3", "90°", "0, 1, 0", "3, 2, -1")]
    [InlineData("1, 2, 3", "-90°", "0, 1, 0", "-3, 2, 1")]
    public void RotationAroundVector(string ps, string @as, string vs, string eps)
    {
      var p = Point3D.Parse(ps);
      var angle = Angle.Parse(@as);
      var coordinateSystems = new[]
                              {
                                CoordinateSystem.Rotation(angle, UnitVector3D.Parse(vs)),
                                CoordinateSystem.Rotation(angle, Vector3D.Parse(vs)),
                                CoordinateSystem.Rotation(angle.Degrees, AngleUnit.Degree, Vector3D.Parse(vs)),
                                CoordinateSystem.Rotation(angle.Radians, AngleUnit.Radian, Vector3D.Parse(vs))
                              };
      var expected = Point3D.Parse(eps);
      foreach (var coordinateSystem in coordinateSystems)
      {
        var rotatedPoint = coordinateSystem.Transform(p);
        //Console.WriteLine(coordinateSystem.ToString());
        AssertGeometry.AreEqual(expected, rotatedPoint);
      }
    }


    [Theory]
    [InlineData("0°", "0°", "0°", "1, 2, 3", "1, 2, 3")]
    [InlineData("90°", "0°", "0°", "1, 2, 3", "-2, 1, 3")]
    [InlineData("-90°", "0°", "0°", "1, 2, 3", "2, -1, 3")]
    [InlineData("0°", "90°", "0°", "1, 2, 3", "3, 2, -1")]
    [InlineData("0°", "-90°", "0°", "1, 2, 3", "-3, 2, 1")]
    [InlineData("0°", "0°", "90°", "1, 2, 3", "1, -3, 2")]
    [InlineData("0°", "0°", "-90°", "1, 2, 3", "1, 3, -2")]
    public void RotationYawPitchRoll(string yaws, string pitchs, string rolls, string ps, string eps)
    {
      var p = Point3D.Parse(ps);
      var yaw = Angle.Parse(yaws);
      var pitch = Angle.Parse(pitchs);
      var roll = Angle.Parse(rolls);
      var coordinateSystems = new[]
                              {
                                CoordinateSystem.Rotation(yaw, pitch, roll),
                                CoordinateSystem.Rotation(yaw.Degrees, pitch.Degrees, roll.Degrees, AngleUnit.Degree),
                                CoordinateSystem.Rotation(yaw.Radians, pitch.Radians, roll.Radians, AngleUnit.Radian)
                              };
      var expected = Point3D.Parse(eps);
      foreach (var coordinateSystem in coordinateSystems)
      {
        var rotatedPoint = coordinateSystem.Transform(p);
        //Console.WriteLine(coordinateSystem.ToString());
        AssertGeometry.AreEqual(expected, rotatedPoint);
      }
    }


    [Theory]
    [InlineData("1, 2, 3", "0, 0, 1", "1, 2, 4")]
    [InlineData("1, 2, 3", "0, 0, -1", "1, 2, 2")]
    [InlineData("1, 2, 3", "0, 0, 0", "1, 2, 3")]
    [InlineData("1, 2, 3", "0, 1, 0", "1, 3, 3")]
    [InlineData("1, 2, 3", "0, -1, 0", "1, 1, 3")]
    [InlineData("1, 2, 3", "1, 0, 0", "2, 2, 3")]
    [InlineData("1, 2, 3", "-1, 0, 0", "0, 2, 3")]
    public void Translation(string ps, string vs, string eps)
    {
      var p = Point3D.Parse(ps);
      var cs = CoordinateSystem.Translation(Vector3D.Parse(vs));
      var tp = cs.Transform(p);
      Console.WriteLine(cs.ToString());
      AssertGeometry.AreEqual(Point3D.Parse(eps), tp);
    }


    [Theory]
    [InlineData(X, X, null)]
    [InlineData(X, X, X)]
    [InlineData(X, X, Y)]
    [InlineData(X, X, Z)]
    [InlineData(X, NegativeX, null)]
    [InlineData(X, NegativeX, Z)]
    [InlineData(X, NegativeX, Y)]
    [InlineData(X, Y, null)]
    [InlineData(X, Z, null)]
    [InlineData(Y, Y, null)]
    [InlineData(Y, Y, X)]
    [InlineData(Y, NegativeY, null)]
    [InlineData(Y, NegativeY, X)]
    [InlineData(Y, NegativeY, Z)]
    [InlineData(Z, NegativeZ, null)]
    [InlineData(Z, NegativeZ, X)]
    [InlineData(Z, NegativeZ, Y)]
    [InlineData("1, 2, 3", "-1, 0, -1", null)]
    public void RotateToTest(string v1s, string v2s, string @as)
    {
      UnitVector3D? axis = string.IsNullOrEmpty(@as) ? (UnitVector3D?) null : UnitVector3D.Parse(@as);
      UnitVector3D v1 = UnitVector3D.Parse(v1s);
      UnitVector3D v2 = UnitVector3D.Parse(v2s);
      CoordinateSystem actual = CoordinateSystem.RotateTo(v1, v2, axis);
      Console.WriteLine(actual);
      var rv = actual.Transform(v1);
      AssertGeometry.AreEqual(v2, rv);
      actual = CoordinateSystem.RotateTo(v2, v1, axis);
      rv = actual.Transform(v2);
      AssertGeometry.AreEqual(v1, rv);
    }


    [Theory]
    [InlineData("1; -5; 3", "1; -5; 3", "o:{0, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    public void TransformPoint(string ps, string eps, string css)
    {
      var p = Point3D.Parse(ps);
      CoordinateSystem cs = CoordinateSystem.Parse(css);
      Point3D actual = p.TransformBy(cs);
      var expected = Point3D.Parse(eps);
      AssertGeometry.AreEqual(expected, actual, float.Epsilon);
    }


    [Theory]
    [InlineData("1; 2; 3", "1; 2; 3", "o:{0, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    [InlineData("1; 2; 3", "1; 2; 3", "o:{3, 4, 5} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    public void TransformVector(string vs, string evs, string css)
    {
      var v = Vector3D.Parse(vs);
      CoordinateSystem cs = CoordinateSystem.Parse(css);
      Vector3D actual = cs.Transform(v);
      var expected = Vector3D.Parse(evs);
      AssertGeometry.AreEqual(expected, actual);
    }


    [Theory]
    [InlineData("o:{0, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}", "o:{0, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    [InlineData("o:{0, 0, 0} x:{10, 0, 0} y:{0, 1, 0} z:{0, 0, 1}", "o:{1, 0, 0} x:{0.1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")
    ]
    [InlineData("o:{0, 0, 0} x:{10, 0, 0} y:{0, 1, 0} z:{0, 0, 1}", "o:{1, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    [InlineData("o:{1, 2, -7} x:{10, 0, 0} y:{0, 1, 0} z:{0, 0, 1}", "o:{0, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    [InlineData("o:{1, 2, -7} x:{10, 0.1, 0} y:{0, 1.2, 0.1} z:{0.1, 0, 1}",
       "o:{2, 5, 1} x:{0.1, 2, 0} y:{0.2, -1, 0} z:{0, 0.4, 1}")]
    public void SetToAlignCoordinateSystemsTest(string fcss, string tcss)
    {
      var fcs = CoordinateSystem.Parse(fcss);
      var tcs = CoordinateSystem.Parse(tcss);

      var css = new[]
                {
                  CoordinateSystem.SetToAlignCoordinateSystems(fcs.Origin, fcs.XAxis, fcs.YAxis, fcs.ZAxis, tcs.Origin,
                                                               tcs.XAxis, tcs.YAxis, tcs.ZAxis),
                  CoordinateSystem.CreateMappingCoordinateSystem(fcs, tcs)
                };
      foreach (var cs in css)
      {
        var aligned = cs.Transform(fcs);
        AssertGeometry.AreEqual(tcs.Origin, aligned.Origin);

        AssertGeometry.AreEqual(tcs.XAxis, aligned.XAxis);

        AssertGeometry.AreEqual(tcs.YAxis, aligned.YAxis);

        AssertGeometry.AreEqual(tcs.ZAxis, aligned.ZAxis);
      }
    }


    [Theory]
    [InlineData(X, Y, Z)]
    [InlineData(NegativeX, Y, Z)]
    [InlineData(NegativeX, Y, null)]
    [InlineData(X, Y, null)]
    [InlineData(X, Y, "0,0,1")]
    [InlineData("1,-1, 1", "0, 1, 1", null)]
    [InlineData(X, Y, Z)]
    [InlineData(X, Z, Y)]
    public void SetToRotateToTest(string vs, string vts, string axisString)
    {
      var v = UnitVector3D.Parse(vs);
      var vt = UnitVector3D.Parse(vts);
      UnitVector3D? axis = null;
      if (axisString != null)
      {
        axis = UnitVector3D.Parse(axisString);
      }
      CoordinateSystem cs = CoordinateSystem.RotateTo(v, vt, axis);
      var rv = cs.Transform(v);
      AssertGeometry.AreEqual(vt, rv);

      CoordinateSystem invert = cs.Invert();
      Vector3D rotateBack = invert.Transform(rv);
      AssertGeometry.AreEqual(v, rotateBack);

      cs = CoordinateSystem.RotateTo(vt, v, axis);
      rotateBack = cs.Transform(rv);
      AssertGeometry.AreEqual(v, rotateBack);
    }


    [Theory]
    [InlineData("o:{1, 2, -7} x:{10, 0, 0} y:{0, 1, 0} z:{0, 0, 1}", "o:{0, 0, 0} x:{1, 0, 0} y:{0, 1, 0} z:{0, 0, 1}")]
    public void Transform(string cs1s, string cs2s)
    {
      var cs1 = CoordinateSystem.Parse(cs1s);
      var cs2 = CoordinateSystem.Parse(cs2s);
      var actual = cs1.Transform(cs2);
      var expected = new CoordinateSystem(cs1.Multiply(cs2));
      AssertGeometry.AreEqual(expected, actual);
    }


    [Fact]
    public void TransformUnitVector()
    {
      var cs = CoordinateSystem.Rotation(90, AngleUnit.Degree, UnitVector3D.ZAxis);
      var uv = UnitVector3D.XAxis;
      var actual = cs.Transform(uv);
      AssertGeometry.AreEqual(UnitVector3D.YAxis, actual);
    }
  }
}
