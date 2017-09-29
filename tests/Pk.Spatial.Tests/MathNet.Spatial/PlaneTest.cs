using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MathNet.Spatial.Euclidean;
using Pk.Spatial.Tests;
using Shouldly;
using Xunit;

namespace MathNet.Spatial.UnitTests.Euclidean
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class PlaneTest
  {
    const string X = "1; 0 ; 0";
    const string Z = "0; 0; 1";
    const string NegativeZ = "0; 0; -1";
    const string ZeroPoint = "0; 0; 0";


    [Theory]
    [InlineData("p:{0, 0, 0} v:{1, 0, 0}", new double[]
                                           {
                                             0,
                                             0,
                                             0
                                           }, new double[]
                                              {
                                                1,
                                                0,
                                                0
                                              })]
    [InlineData("1, 0, 0, 0", new double[]
                              {
                                0,
                                0,
                                0
                              }, new double[]
                                 {
                                   1,
                                   0,
                                   0
                                 })]
    public void ParseTest(string s, double[] pds, double[] vds)
    {
      var plane = Plane.Parse(s);
      AssertGeometry.AreEqual(new Point3D(pds), plane.RootPoint);
      AssertGeometry.AreEqual(new Vector3D(vds), plane.Normal);
    }


    [Theory]
    [InlineData(ZeroPoint, "p:{0, 0, 0} v:{0, 0, 1}", ZeroPoint)]
    [InlineData(ZeroPoint, "p:{0, 0, -1} v:{0, 0, 1}", "0; 0;-1")]
    [InlineData(ZeroPoint, "p:{0, 0, 1} v:{0, 0, -1}", "0; 0; 1")]
    [InlineData("1; 2; 3", "p:{0, 0, 0} v:{0, 0, 1}", "1; 2; 0")]
    public void ProjectPointOnTest(string ps, string pls, string eps)
    {
      var plane = Plane.Parse(pls);
      var projectedPoint = plane.Project(Point3D.Parse(ps));
      var expected = Point3D.Parse(eps);
      AssertGeometry.AreEqual(expected, projectedPoint, float.Epsilon);
    }


    private void ProjectPoint(Point3D pointToProject, Point3D planeRootPoint, UnitVector3D planeNormal,
                              Point3D projectedresult)
    {
      var plane = new Plane(planeNormal, planeRootPoint);
      var projectOn = plane.Project(pointToProject);
      AssertGeometry.AreEqual(projectedresult, projectOn, float.Epsilon);
    }


    [Theory]
    [InlineData(ZeroPoint, Z, ZeroPoint, 0)]
    [InlineData(ZeroPoint, Z, "1; 2; 0", 0)]
    [InlineData(ZeroPoint, Z, "1; -2; 0", 0)]
    [InlineData(ZeroPoint, Z, "1; 2; 3", 3)]
    [InlineData(ZeroPoint, Z, "-1; 2; -3", -3)]
    [InlineData(ZeroPoint, NegativeZ, ZeroPoint, 0)]
    [InlineData(ZeroPoint, NegativeZ, "1; 2; 1", -1)]
    [InlineData(ZeroPoint, NegativeZ, "1; 2; -1", 1)]
    [InlineData("0; 0; -1", NegativeZ, ZeroPoint, -1)]
    [InlineData("0; 0; 1", NegativeZ, ZeroPoint, 1)]
    [InlineData(ZeroPoint, X, "1; 0; 0", 1)]
    [InlineData("188,6578; 147,0620; 66,0170", Z, "118,6578; 147,0620; 126,1170", 60.1)]
    public void SignedDistanceToPoint(string prps, string pns, string ps, double expected)
    {
      var plane = new Plane(UnitVector3D.Parse(pns), Point3D.Parse(prps));
      var p = Point3D.Parse(ps);
      expected.ShouldBe(plane.SignedDistanceTo(p), 1E-6);
    }


    [Theory]
    [InlineData(ZeroPoint, Z, ZeroPoint, Z, 0)]
    [InlineData(ZeroPoint, Z, "0;0;1", Z, 1)]
    [InlineData(ZeroPoint, Z, "0;0;-1", Z, -1)]
    [InlineData(ZeroPoint, NegativeZ, "0;0;-1", Z, 1)]
    public void SignedDistanceToOtherPlane(string prps, string pns, string otherPlaneRootPointString,
                                           string otherPlaneNormalString, double expectedValue)
    {
      var plane = new Plane(UnitVector3D.Parse(pns), Point3D.Parse(prps));
      var otherPlane = new Plane(UnitVector3D.Parse(otherPlaneNormalString), Point3D.Parse(otherPlaneRootPointString));
      expectedValue.ShouldBe(plane.SignedDistanceTo(otherPlane), 1E-6);
    }


    [Theory]
    [InlineData(ZeroPoint, Z, ZeroPoint, Z, 0)]
    [InlineData(ZeroPoint, Z, ZeroPoint, X, 0)]
    [InlineData(ZeroPoint, Z, "0;0;1", X, 1)]
    public void SignedDistanceToRay(string prps, string pns, string rayThroughPointString, string rayDirectionString,
                                    double expectedValue)
    {
      var plane = new Plane(UnitVector3D.Parse(pns), Point3D.Parse(prps));
      var otherPlane = new Ray3D(Point3D.Parse(rayThroughPointString), UnitVector3D.Parse(rayDirectionString));
      expectedValue.ShouldBe(plane.SignedDistanceTo(otherPlane), 1E-6);
    }


    [Theory]
    [InlineData("p:{0, 0, 0} v:{0, 0, 1}", "p:{0, 0, 0} v:{0, 1, 0}", "0, 0, 0", "-1, 0, 0")]
    [InlineData("p:{0, 0, 2} v:{0, 0, 1}", "p:{0, 0, 0} v:{0, 1, 0}", "0, 0, 2", "-1, 0, 0")]
    public void InterSectionWithPlaneTest(string pl1s, string pl2s, string eps, string evs)
    {
      var plane1 = Plane.Parse(pl1s);
      var plane2 = Plane.Parse(pl2s);
      var intersections = new[]
                          {
                            plane1.IntersectionWith(plane2),
                            plane2.IntersectionWith(plane1)
                          };
      foreach (var intersection in intersections)
      {
        AssertGeometry.AreEqual(Point3D.Parse(eps), intersection.ThroughPoint);
        AssertGeometry.AreEqual(UnitVector3D.Parse(evs), intersection.Direction);
      }
    }


    [Theory]
    [InlineData("p:{0, 0, 0} v:{0, 0, 1}", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, 0", "0, 0, 0")]
    public void InterSectionWithPlaneTest_BadArgument(string pl1s, string pl2s, string eps, string evs)
    {
      var plane1 = Plane.Parse(pl1s);
      var plane2 = Plane.Parse(pl2s);

      Assert.Throws<ArgumentException>(() => plane1.IntersectionWith(plane2));
      Assert.Throws<ArgumentException>(() => plane2.IntersectionWith(plane1));
    }


    [Theory]
    [InlineData("p:{0, 0, 0} v:{1, 0, 0}", "p:{0, 0, 0} v:{0, 1, 0}", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, 0")]
    [InlineData("p:{0, 0, 0} v:{-1, 0, 0}", "p:{0, 0, 0} v:{0, 1, 0}", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, 0")]
    [InlineData("p:{20, 0, 0} v:{1, 0, 0}", "p:{0, 0, 0} v:{0, 1, 0}", "p:{0, 0, -30} v:{0, 0, 1}", "20, 0, -30")]
    [InlineData("1, 1, 0, -12", "-1, 1, 0, -12", "0, 0, 1, -5", "0, 16.970563, 5")]
    public void PointFromPlanes(string pl1s, string pl2s, string pl3s, string eps)
    {
      var plane1 = Plane.Parse(pl1s);
      var plane2 = Plane.Parse(pl2s);
      var plane3 = Plane.Parse(pl3s);
      var points = new[]
                   {
                     Plane.PointFromPlanes(plane1, plane2, plane3),
                     Plane.PointFromPlanes(plane2, plane1, plane3),
                     Plane.PointFromPlanes(plane1, plane3, plane2),
                     Plane.PointFromPlanes(plane2, plane3, plane1),
                     Plane.PointFromPlanes(plane3, plane2, plane1),
                     Plane.PointFromPlanes(plane3, plane1, plane2)
                   };
      var expected = Point3D.Parse(eps);
      foreach (var point in points)
      {
        AssertGeometry.AreEqual(expected, point);
      }
    }


    [Theory]
    [InlineData("p:{0, 0, 0} v:{0, 0, 1}")]
    public void BinaryRountrip(string pls)
    {
      var plane = Plane.Parse(pls);
      using (var ms = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, plane);
        ms.Flush();
        ms.Position = 0;
        var roundTrip = (Plane) formatter.Deserialize(ms);
        AssertGeometry.AreEqual(plane, roundTrip);
      }
    }


    [Fact]
    public void CtorTest()
    {
      var plane1 = new Plane(new Point3D(0, 0, 3), UnitVector3D.ZAxis);
      var plane2 = new Plane(0, 0, 3, -3);
      var plane3 = new Plane(UnitVector3D.ZAxis, 3);
      var plane4 = new Plane(new Point3D(0, 0, 3), new Point3D(5, 3, 3), new Point3D(-2, 1, 3));
      AssertGeometry.AreEqual(plane1, plane2);
      AssertGeometry.AreEqual(plane1, plane3);
      AssertGeometry.AreEqual(plane1, plane4);
    }


    [Fact]
    public void InterSectionPointDifferentOrderTest()
    {
      var plane1 = new Plane(new UnitVector3D(0.8, 0.3, 0.01), new Point3D(20, 0, 0));
      var plane2 = new Plane(new UnitVector3D(0.002, 1, 0.1), new Point3D(0, 0, 0));
      var plane3 = new Plane(new UnitVector3D(0.5, 0.5, 1), new Point3D(0, 0, -30));
      var pointFromPlanes1 = Plane.PointFromPlanes(plane1, plane2, plane3);
      var pointFromPlanes2 = Plane.PointFromPlanes(plane2, plane1, plane3);
      var pointFromPlanes3 = Plane.PointFromPlanes(plane3, plane1, plane2);
      AssertGeometry.AreEqual(pointFromPlanes1, pointFromPlanes2, 1E-10);
      AssertGeometry.AreEqual(pointFromPlanes3, pointFromPlanes2, 1E-10);
    }


    [Fact]
    public void MirrorPointTest()
    {
      var plane = new Plane(UnitVector3D.ZAxis, new Point3D(0, 0, 0));
      var point3D = new Point3D(1, 2, 3);
      Point3D mirrorAbout = plane.MirrorAbout(point3D);
      AssertGeometry.AreEqual(new Point3D(1, 2, -3), mirrorAbout, float.Epsilon);
    }


    [Fact]
    public void ProjectLineOnTest()
    {
      var unitVector = UnitVector3D.ZAxis;
      var rootPoint = new Point3D(0, 0, 1);
      var plane = new Plane(unitVector, rootPoint);

      var line = new Line3D(new Point3D(0, 0, 0), new Point3D(1, 0, 0));
      var projectOn = plane.Project(line);
      AssertGeometry.AreEqual(new Line3D(new Point3D(0, 0, 1), new Point3D(1, 0, 1)), projectOn, float.Epsilon);
    }


    [Fact]
    public void ProjectVectorOnTest()
    {
      var unitVector = UnitVector3D.ZAxis;
      var rootPoint = new Point3D(0, 0, 1);
      var plane = new Plane(unitVector, rootPoint);
      var vector = new Vector3D(1, 0, 0);
      var projectOn = plane.Project(vector);
      AssertGeometry.AreEqual(new Vector3D(1, 0, 0), projectOn.Direction, float.Epsilon);
      AssertGeometry.AreEqual(new Point3D(0, 0, 1), projectOn.ThroughPoint, float.Epsilon);
    }


    [Fact]
    public void SignOfD()
    {
      var plane1 = new Plane(UnitVector3D.ZAxis, new Point3D(0, 0, 100));
      ((double) -100).ShouldBe(plane1.D);
    }
  }
}
