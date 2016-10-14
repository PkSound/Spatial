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
      this.underlyingPoint = new Point3D(x.As(StandardUnits.Length), y.As(StandardUnits.Length), z.As(StandardUnits.Length));
    }


    public Location3D(double x, double y, double z, LengthUnit unit = StandardUnits.Length)
        : this(Length.From(x, unit), Length.From(y, unit), Length.From(z, unit)) {}


    public static Location3D Origin => new Location3D();


    public bool Equals(Location3D other) { return this.underlyingPoint.Equals(other.Freeze(StandardUnits.Length)); }
    public Length X => Length.From(this.underlyingPoint.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingPoint.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingPoint.Z, StandardUnits.Length);
    public Point3D Freeze(LengthUnit unit) { return new Point3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit)); }
    public static bool operator ==(Location3D left, Location3D right) { return left.Equals(right); }
    public static bool operator !=(Location3D left, Location3D right) { return !(left == right); }
  }
}
