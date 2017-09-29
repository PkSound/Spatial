using System;
using System.Globalization;

namespace MathNet.Spatial.Euclidean
{
  [Serializable]
  public struct Ray3D : IEquatable<Ray3D>, IFormattable
  {
    public readonly UnitVector3D Direction;
    public readonly Point3D ThroughPoint;


    public Ray3D(Point3D throughPoint, UnitVector3D direction)
    {
      this.ThroughPoint = throughPoint;
      this.Direction = direction;
    }


    public Ray3D(Point3D throughPoint, Vector3D direction) : this(throughPoint, direction.Normalize()) { }

    //        public bool IsCollinear(Ray3D otherRay, double tolerance = float.Epsilon)
    //        {
    //            return this.Direction.IsParallelTo(otherRay.Direction, tolerance);
    //        }

    public bool Equals(Ray3D other)
    {
      return this.Direction.Equals(other.Direction) && this.ThroughPoint.Equals(other.ThroughPoint);
    }


    public string ToString(string format, IFormatProvider formatProvider)
    {
      return string.Format("ThroughPoint: {0}, Direction: {1}", this.ThroughPoint.ToString(format, formatProvider),
                           this.Direction.ToString(format, formatProvider));
    }


    //        public bool Equals(Ray3D other, double tolerance)
    //        {
    //            return this.Direction.Equals(other.Direction, tolerance) &&
    //                   this.ThroughPoint.Equals(other.ThroughPoint, tolerance);
    //        }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals(null, obj))
      {
        return false;
      }

      return obj is Ray3D && this.Equals((Ray3D) obj);
    }


    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = this.ThroughPoint.GetHashCode();
        hashCode = (hashCode*397) ^ this.Direction.GetHashCode();
        return hashCode;
      }
    }


    /// <summary>
    ///   The intersection of the two planes
    /// </summary>
    /// <param name="plane1"></param>
    /// <param name="plane2"></param>
    /// <returns></returns>
    public static Ray3D IntersectionOf(Plane plane1, Plane plane2)
    {
      return plane1.IntersectionWith(plane2);
    }


    public Point3D? IntersectionWith(Plane plane) { return plane.IntersectionWith(this); }


    /// <summary>
    ///   Returns the shortes line from a point to the ray
    /// </summary>
    /// <param name="point3D"></param>
    /// <returns></returns>
    public Line3D LineTo(Point3D point3D)
    {
      Vector3D v = this.ThroughPoint.VectorTo(point3D);
      Vector3D alongVector = v.ProjectOn(this.Direction);
      return new Line3D(this.ThroughPoint + alongVector, point3D);
    }


    public static bool operator ==(Ray3D left, Ray3D right) { return left.Equals(right); }

    public static bool operator !=(Ray3D left, Ray3D right) { return !left.Equals(right); }


    /// <summary>
    ///   Parses string representation of throughpoint and direction
    ///   This is mainly meant for tests
    /// </summary>
    /// <param name="point"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Ray3D Parse(string point, string direction)
    {
      return new Ray3D(Point3D.Parse(point), UnitVector3D.Parse(direction));
    }


    /// <summary>
    ///   Parses a string in the format: 'p:{1, 2, 3} v:{0, 0, 1}' to a Ray3D
    ///   This is mainly meant for tests
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Ray3D Parse(string s)
    {
      return Parser.ParseRay3D(s);
    }


    /// <summary>
    ///   Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return this.ToString(null, CultureInfo.InvariantCulture);
    }
  }
}
