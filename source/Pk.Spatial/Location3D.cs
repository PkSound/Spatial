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


    public Location3D(Point3D point, LengthUnit unit = StandardUnits.Length)
        : this(Length.From(point.X, unit), Length.From(point.Y, unit), Length.From(point.Z, unit)) {}


    public static Location3D Origin => new Location3D();
    public bool Equals(Location3D other) { return this.underlyingPoint.Equals(other.Freeze()); }
    public Length X => Length.From(this.underlyingPoint.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingPoint.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingPoint.Z, StandardUnits.Length);


    public Point3D Freeze(LengthUnit unit = StandardUnits.Length)
    {
      return new Point3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit));
    }


    public Displacement3D DisplacementFromOrigin() { return this - Location3D.Origin; }


    public static Location3D operator +(Location3D l, Displacement3D d)
    {
      return new Location3D(l.Freeze() + d.Freeze());
    }


    public static bool operator ==(Location3D left, Location3D right) { return left.Equals(right); }
    public static bool operator !=(Location3D left, Location3D right) { return !(left == right); }


    public static Location3D operator -(Location3D l, Displacement3D d)
    {
      return new Location3D(l.Freeze() - d.Freeze());
    }


    public static Displacement3D operator -(Location3D lhs, Location3D rhs)
    {
      return new Displacement3D(lhs.Freeze() - rhs.Freeze());
    }
  }
}
