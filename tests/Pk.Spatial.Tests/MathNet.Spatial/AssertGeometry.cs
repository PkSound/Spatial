using MathNet.Numerics.LinearAlgebra;
using MathNet.Spatial.Euclidean;
using Shouldly;

namespace MathNet.Spatial.UnitTests
{
  public static class AssertGeometry
  {
    public static void AreEqual(CoordinateSystem coordinateSystem, Point3D origin, Vector3D xAxis, Vector3D yAxis,
                                Vector3D zAxis, double tolerance = 1e-6)
    {
      AssertGeometry.AreEqual(xAxis, coordinateSystem.XAxis, tolerance);
      AssertGeometry.AreEqual(yAxis, coordinateSystem.YAxis, tolerance);
      AssertGeometry.AreEqual(zAxis, coordinateSystem.ZAxis, tolerance);
      AssertGeometry.AreEqual(origin, coordinateSystem.Origin, tolerance);

      AssertGeometry.AreEqual(new[]
                              {
                                xAxis.X,
                                xAxis.Y,
                                xAxis.Z,
                                0
                              }, coordinateSystem.Column(0).ToArray(), tolerance);
      AssertGeometry.AreEqual(new[]
                              {
                                yAxis.X,
                                yAxis.Y,
                                yAxis.Z,
                                0
                              }, coordinateSystem.Column(1).ToArray(), tolerance);
      AssertGeometry.AreEqual(new[]
                              {
                                zAxis.X,
                                zAxis.Y,
                                zAxis.Z,
                                0
                              }, coordinateSystem.Column(2).ToArray(), tolerance);
      AssertGeometry.AreEqual(new[]
                              {
                                origin.X,
                                origin.Y,
                                origin.Z,
                                1
                              }, coordinateSystem.Column(3).ToArray(), tolerance);
    }


    public static void AreEqual(UnitVector3D expected, UnitVector3D actual, double tolerance = 1e-6, string message = "")
    {
      if (string.IsNullOrEmpty(message)) message = string.Format("Expected {0} but was {1}", expected, actual);

      AssertGeometry.assertEqual(expected, actual, tolerance, message);
    }


    public static void AreEqual(Vector3D expected, Vector3D actual, double tolerance = 1e-6, string message = "")
    {
      if (string.IsNullOrEmpty(message)) message = string.Format("Expected {0} but was {1}", expected, actual);
      AssertGeometry.assertEqual(expected, actual, tolerance, message);
    }


    public static void AreEqual(UnitVector3D expected, Vector3D actual, double tolerance = 1e-6, string message = "")
    {
      AssertGeometry.AreEqual(expected.ToVector3D(), actual, tolerance, message);
    }


    public static void AreEqual(Vector3D expected, UnitVector3D actual, double tolerance = 1e-6, string message = "")
    {
      AssertGeometry.AreEqual(expected, actual.ToVector3D(), tolerance, message);
    }


    public static void AreEqual(Point3D expected, Point3D actual, double tolerance = 1e-6, string message = "")
    {
      if (string.IsNullOrEmpty(message)) message = string.Format("Expected {0} but was {1}", expected, actual);
      AssertGeometry.assertEqual(expected, actual, tolerance, message);
    }


    public static void AreEqual(CoordinateSystem expected, CoordinateSystem actual, double tolerance = 1e-6,
                                string message = "")
    {
      if (string.IsNullOrEmpty(message)) message = string.Format("Expected {0} but was {1}", expected, actual);
      expected.Values.Length.ShouldBe(expected.Values.Length, message);
      for (int i = 0; i < expected.Values.Length; i++)
      {
        actual.Values[i].ShouldBe(expected.Values[i], tolerance);
      }
    }


    public static void AreEqual(double[] expected, double[] actual, double tolerance = 1e-6, string message = "")
    {
      if (string.IsNullOrEmpty(message))
        message = string.Format("Expected {0} but was {1}", "{" + string.Join(",", expected) + "}",
                                "{" + string.Join(",", actual) + "}");
      expected.Length.ShouldBe(expected.Length, message);
      for (int i = 0; i < expected.Length; i++)
      {
        actual[i].ShouldBe(expected[i], tolerance);
      }
    }


    public static void AreEqual(Line3D expected, Line3D actual, double tolerance = 1e-6)
    {
      AssertGeometry.AreEqual(expected.StartPoint, actual.StartPoint, tolerance);
      AssertGeometry.AreEqual(expected.EndPoint, actual.EndPoint, tolerance);
    }


    public static void AreEqual(Ray3D expected, Ray3D actual, double tolerance = 1e-6, string message = "")
    {
      AssertGeometry.AreEqual(expected.ThroughPoint, actual.ThroughPoint, tolerance, message);
      AssertGeometry.AreEqual(expected.Direction, actual.Direction, tolerance, message);
    }


    public static void AreEqual(Plane expected, Plane actual, double tolerance = 1e-6, string message = "")
    {
      AssertGeometry.AreEqual(expected.Normal, actual.Normal, tolerance, message);
      AssertGeometry.AreEqual(expected.RootPoint, actual.RootPoint, tolerance, message);
      actual.D.ShouldBe(expected.D, tolerance, message);
    }


    public static void AreEqual(Matrix<double> expected, Matrix<double> actual, double tolerance = 1e-6)
    {
      expected.RowCount.ShouldBe(expected.RowCount);
      expected.ColumnCount.ShouldBe(expected.ColumnCount);
      double[] expectedRowWiseArray = expected.ToRowWiseArray();
      double[] actualRowWiseArray = actual.ToRowWiseArray();
      for (int i = 0; i < expectedRowWiseArray.Length; i++)
      {
         actualRowWiseArray[i].ShouldBe(expectedRowWiseArray[i], tolerance);
      }
    }


    private static void assertEqual(Point3D expected, Point3D actual, double tolerance, string message)
    {
      actual.X.ShouldBe(expected.X, tolerance, message);
      actual.Y.ShouldBe(expected.Y, tolerance, message);
      actual.Z.ShouldBe(expected.Z, tolerance, message);
    }


    private static void assertEqual(Vector3D expected, Vector3D actual, double tolerance, string message)
    {
      actual.X.ShouldBe(expected.X, tolerance, message);
      actual.Y.ShouldBe(expected.Y, tolerance, message);
      actual.Z.ShouldBe(expected.Z, tolerance, message);
    }


    private static void assertEqual(UnitVector3D expected, UnitVector3D actual, double tolerance, string message)
    {
      actual.X.ShouldBe(expected.X, tolerance, message);
      actual.Y.ShouldBe(expected.Y, tolerance, message);
      actual.Z.ShouldBe(expected.Z, tolerance, message);
    }
  }
}
