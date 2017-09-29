﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MathNet.Spatial.Euclidean;
using Pk.Spatial.Tests;
using Shouldly;
using Xunit;

namespace MathNet.Spatial.UnitTests.Euclidean
{
  [Trait(TestConstants.CategoryName, TestConstants.UnitTestsTag)]
  public class Point3DTests
  {
    [Theory]
    [InlineData("1, 2, 3", "1, 2, 3", 1e-4, true)]
    [InlineData("1, 2, 3", "4, 5, 6", 1e-4, false)]
    public void Equals(string p1s, string p2s, double tol, bool expected)
    {
      var p1 = Point3D.Parse(p1s);
      var p2 = Point3D.Parse(p2s);
      expected.ShouldBe(p1 == p2);
      expected.ShouldBe(p1.Equals(p2));
      expected.ShouldBe(p1.Equals((object) p2));
      expected.ShouldBe(object.Equals(p1, p2));
      expected.ShouldBe(p1.Equals(p2, tol));
      expected.ShouldNotBe(p1 != p2);
    }


    [Theory]
    [InlineData("0, 0, 0", "0, 0, 1", "0, 0, 0.5")]
    [InlineData("0, 0, 1", "0, 0, 0", "0, 0, 0.5")]
    [InlineData("0, 0, 0", "0, 0, 0", "0, 0, 0")]
    [InlineData("1, 1, 1", "3, 3, 3", "2, 2, 2")]
    [InlineData("-3, -3, -3", "3, 3, 3", "0, 0, 0")]
    public void MidPoint(string p1s, string p2s, string eps)
    {
      var p1 = Point3D.Parse(p1s);
      var p2 = Point3D.Parse(p2s);
      var ep = Point3D.Parse(eps);
      Point3D mp = Point3D.MidPoint(p1, p2);
      AssertGeometry.AreEqual(ep, mp, 1e-9);
      var centroid = Point3D.Centroid(p1, p2);
      AssertGeometry.AreEqual(ep, centroid, 1e-9);
    }


    [Theory]
    [InlineData("p:{0, 0, 0} v:{0, 0, 1}", "p:{0, 0, 0} v:{0, 1, 0}", "p:{0, 0, 0} v:{1, 0, 0}", "0, 0, 0")]
    [InlineData("p:{0, 0, 5} v:{0, 0, 1}", "p:{0, 4, 0} v:{0, 1, 0}", "p:{3, 0, 0} v:{1, 0, 0}", "3, 4, 5")]
    public void FromPlanes(string pl1s, string pl2s, string pl3s, string eps)
    {
      var plane1 = Plane.Parse(pl1s);
      var plane2 = Plane.Parse(pl2s);
      var plane3 = Plane.Parse(pl3s);
      var p1 = Point3D.IntersectionOf(plane1, plane2, plane3);
      var p2 = Point3D.IntersectionOf(plane2, plane1, plane3);
      var p3 = Point3D.IntersectionOf(plane2, plane3, plane1);
      var p4 = Point3D.IntersectionOf(plane3, plane1, plane2);
      var p5 = Point3D.IntersectionOf(plane3, plane2, plane1);
      var ep = Point3D.Parse(eps);
      foreach (var p in new[]
                        {
                          p1,
                          p2,
                          p3,
                          p4,
                          p5
                        })
      {
        AssertGeometry.AreEqual(ep, p);
      }
    }


    [Theory]
    [InlineData("0, 0, 0", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, 0")]
    [InlineData("0, 0, 1", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, -1")]
    public void MirrorAbout(string ps, string pls, string eps)
    {
      var p = Point3D.Parse(ps);
      var p2 = Plane.Parse(pls);
      var actual = p.MirrorAbout(p2);

      var ep = Point3D.Parse(eps);
      AssertGeometry.AreEqual(ep, actual);
    }


    [Theory]
    [InlineData("0, 0, 0", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, 0")]
    [InlineData("0, 0, 1", "p:{0, 0, 0} v:{0, 0, 1}", "0, 0, 0")]
    [InlineData("0, 0, 1", "p:{0, 10, 0} v:{0, 1, 0}", "0, 10, 1")]
    public void ProjectOnTests(string ps, string pls, string eps)
    {
      var p = Point3D.Parse(ps);
      var p2 = Plane.Parse(pls);
      var actual = p.ProjectOn(p2);

      var ep = Point3D.Parse(eps);
      AssertGeometry.AreEqual(ep, actual);
    }


    [Theory]
    [InlineData("1, 2, 3", "1, 0, 0", "2, 2, 3")]
    [InlineData("1, 2, 3", "0, 1, 0", "1, 3, 3")]
    [InlineData("1, 2, 3", "0, 0, 1", "1, 2, 4")]
    public void AddVector(string ps, string vs, string eps)
    {
      Point3D p = Point3D.Parse(ps);
      var actuals = new[]
                    {
                      p + Vector3D.Parse(vs),
                      p + UnitVector3D.Parse(vs)
                    };
      var expected = Point3D.Parse(eps);
      foreach (var actual in actuals)
      {
        expected.ShouldBe(actual);
      }
    }


    [Theory]
    [InlineData("1, 2, 3", "1, 0, 0", "0, 2, 3")]
    [InlineData("1, 2, 3", "0, 1, 0", "1, 1, 3")]
    [InlineData("1, 2, 3", "0, 0, 1", "1, 2, 2")]
    public void SubtractVector(string ps, string vs, string eps)
    {
      Point3D p = Point3D.Parse(ps);
      var actuals = new[]
                    {
                      p - Vector3D.Parse(vs),
                      p - UnitVector3D.Parse(vs)
                    };
      var expected = Point3D.Parse(eps);
      foreach (var actual in actuals)
      {
        expected.ShouldBe(actual);
      }
    }


    [Theory]
    [InlineData("1, 2, 3", "4, 8, 16", "-3, -6, -13")]
    public void SubtractPoint(string p1s, string p2s, string evs)
    {
      Point3D p1 = Point3D.Parse(p1s);
      Point3D p2 = Point3D.Parse(p2s);

      var expected = Vector3D.Parse(evs);
      expected.ShouldBe(p1 - p2);
    }


    [Theory]
    [InlineData("0,0,0", "1,0,0", 1)]
    [InlineData("1,1,1", "2,1,1", 1)]
    public void DistanceTo(string p1s, string p2s, double d)
    {
      var p1 = Point3D.Parse(p1s);
      var p2 = Point3D.Parse(p2s);

      d.ShouldBe(p1.DistanceTo(p2), 1e-6);
      d.ShouldBe(p2.DistanceTo(p1), 1e-6);
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
    [InlineData("-1.0 ; 2.5;3.3", new[]
                                  {
                                    -1,
                                    2.5,
                                    3.3
                                  })]
    [InlineData("-1 ; -2;-3", new double[]
                              {
                                -1,
                                -2,
                                -3
                              })]
    public void ParseTest(string pointAsString, double[] expectedPoint)
    {
      Point3D point3D = Point3D.Parse(pointAsString);
      Point3D expected = new Point3D(expectedPoint);
      AssertGeometry.AreEqual(expected, point3D, 1e-9);
    }


    [Theory]
    [InlineData("-1 ; 2;-3")]
    public void ToVectorAndBack(string ps)
    {
      Point3D p = Point3D.Parse(ps);
      AssertGeometry.AreEqual(p, p.ToVector3D().ToPoint3D(), 1e-9);
    }


    [Theory]
    [InlineData("-2, 0, 1e-4", null, "(-2, 0, 0.0001)", 1e-4)]
    public void ToString(string vs, string format, string expected, double tolerance)
    {
      var p = Point3D.Parse(vs);
      string actual = p.ToString(format);
      expected.ShouldBe(actual);
      AssertGeometry.AreEqual(p, Point3D.Parse(actual), tolerance);
    }


    [Fact]
    public void BinaryRountrip()
    {
      var v = new Point3D(1, 2, 3);
      using (var ms = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, v);
        ms.Flush();
        ms.Position = 0;
        var roundTrip = (Point3D) formatter.Deserialize(ms);
        AssertGeometry.AreEqual(v, roundTrip);
      }
    }


    [Fact]
    public void Ctor()
    {
      var actuals = new[]
                    {
                      new Point3D(1, 2, 3),
                      new Point3D(new[]
                                  {
                                    1,
                                    2,
                                    3.0
                                  })
                    };
      foreach (var actual in actuals)
      {
        ((double) 1).ShouldBe(actual.X, 1e-6);
        ((double) 2).ShouldBe(actual.Y, 1e-6);
        ((double) 3).ShouldBe(actual.Z, 1e-6);
      }
      Assert.Throws<ArgumentException>(() => new Point3D(new[]
                                                         {
                                                           1.0,
                                                           2,
                                                           3,
                                                           4
                                                         }));
    }


    [Fact]
    public void ToDenseVector()
    {
      var p = new Point3D(1, 2, 3);
      var denseVector = p.ToVector();
      ((double) 3).ShouldBe(denseVector.Count);
      ((double) 1).ShouldBe(denseVector[0], 1e-6);
      ((double) 2).ShouldBe(denseVector[1], 1e-6);
      ((double) 3).ShouldBe(denseVector[2], 1e-6);
    }
  }
}
