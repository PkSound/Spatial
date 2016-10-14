using System;
using MathNet.Spatial.Euclidean;
using UnitsNet;
using UnitsNet.Units;

namespace Pk.Spatial
{
  /// <summary>
  ///   Describes a location in 3D space.
  ///   X Y and Z each describe the distance from the origin in their respective axis.
  /// </summary>
  public struct Location3D : IPoint3D<Length>, IEquatable<Location3D>
  {
    private readonly Point3D underlyingPoint;


    public Location3D(Length x, Length y, Length z)
    {
      this.underlyingPoint = new Point3D(x.As(StandardUnits.Length), y.As(StandardUnits.Length),
                                         z.As(StandardUnits.Length));
    }


    public Location3D(double x, double y, double z, LengthUnit unit = StandardUnits.Length)
        : this(Length.From(x, unit), Length.From(y, unit), Length.From(z, unit)) {}


    public Location3D(Point3D point, LengthUnit unit)
        : this(Length.From(point.X, unit), Length.From(point.Y, unit), Length.From(point.Z, unit)) {}


    public static Location3D Origin => new Location3D();
    public bool Equals(Location3D other) { return this.underlyingPoint.Equals(other.Freeze(StandardUnits.Length)); }
    public Length X => Length.From(this.underlyingPoint.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingPoint.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingPoint.Z, StandardUnits.Length);
    public Point3D Freeze(LengthUnit unit) { return new Point3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit)); }


    public static Location3D operator +(Location3D l, Displacement3D d)
    {
      var point = l.Freeze(StandardUnits.Length);
      var vector = d.Freeze(StandardUnits.Length);
      var resultPoint = point + vector;
      return new Location3D(resultPoint, StandardUnits.Length);
    }


    public static bool operator ==(Location3D left, Location3D right) { return left.Equals(right); }
    public static bool operator !=(Location3D left, Location3D right) { return !(left == right); }


    public static Location3D operator -(Location3D l, Displacement3D d)
    {
      var point = l.Freeze(StandardUnits.Length);
      var vector = d.Freeze(StandardUnits.Length);
      var resultPoint = point - vector;
      return new Location3D(resultPoint, StandardUnits.Length);
    }


    public static Displacement3D operator -(Location3D lhs, Location3D rhs)
    {
      var p1 = lhs.Freeze(StandardUnits.Length);
      var p2 = rhs.Freeze(StandardUnits.Length);
      var resultVector = p1 - p2;
      return new Displacement3D(resultVector, StandardUnits.Length);
    }
  }
}
