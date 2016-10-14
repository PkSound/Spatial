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
    private static Point3D underlyingAddition;
    private readonly Point3D underlyingPoint;


    public Location3D(Length x, Length y, Length z)
    {
      this.underlyingPoint = new Point3D(x.As(StandardUnits.Length), y.As(StandardUnits.Length),
                                         z.As(StandardUnits.Length));
    }


    public static Location3D Origin => new Location3D();
    public bool Equals(Location3D other) { return this.underlyingPoint.Equals(other.FreezeTo(StandardUnits.Length)); }
    public Length X => Length.From(this.underlyingPoint.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingPoint.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingPoint.Z, StandardUnits.Length);
    public Point3D FreezeTo(LengthUnit unit) { return new Point3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit)); }
    public Displacement3D DisplacementFromOrigin() { return this - Location3D.Origin; }
    public Displacement3D DisplacementTo(Location3D other) { return other - this; }


    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals(null, obj)) return false;
      return obj is Location3D && this.Equals((Location3D) obj);
    }


    public static Location3D From(Point3D point, LengthUnit unit)
    {
      return Location3D.From(point.X, point.Y, point.Z, unit);
    }


    public static Location3D From(double x, double y, double z, LengthUnit unit)
    {
      return new Location3D(Length.From(x, unit), Length.From(y, unit), Length.From(z, unit));
    }


    public static Location3D FromMeters(Point3D point) { return Location3D.FromMeters(point.X, point.Y, point.Z); }


    public static Location3D FromMeters(double x, double y, double z)
    {
      return Location3D.From(x, y, z, LengthUnit.Meter);
    }


    public override int GetHashCode() { return this.underlyingPoint.GetHashCode(); }


    public static Location3D operator +(Location3D l, Displacement3D d)
    {
      underlyingAddition = l.FreezeTo(StandardUnits.Length) + d.FreezeTo(StandardUnits.Length);
      return Location3D.From(underlyingAddition, StandardUnits.Length);
    }


    public static bool operator ==(Location3D left, Location3D right) { return left.Equals(right); }
    public static bool operator !=(Location3D left, Location3D right) { return !(left == right); }


    public static Location3D operator -(Location3D l, Displacement3D d)
    {
      var underlyingPoint = l.FreezeTo(StandardUnits.Length) - d.FreezeTo(StandardUnits.Length);
      return Location3D.From(underlyingPoint, StandardUnits.Length);
    }


    public static Displacement3D operator -(Location3D lhs, Location3D rhs)
    {
      var underlyingVector = lhs.FreezeTo(StandardUnits.Length) - rhs.FreezeTo(StandardUnits.Length);
      return Displacement3D.From(underlyingVector, StandardUnits.Length);
    }
  }
}
