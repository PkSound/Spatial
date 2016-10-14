using System;
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
    public Location3D(Length x, Length y, Length z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }


    public Location3D(double x, double y, double z, LengthUnit unit = StandardUnits.Length)
        : this(Length.From(x, unit), Length.From(y, unit), Length.From(z, unit)) {}


    public static Location3D Origin => new Location3D();
    public bool Equals(Location3D other) { throw new NotImplementedException(); }
    public Length X { get; }
    public Length Y { get; }
    public Length Z { get; }
    public IPoint3D<double> Freeze(LengthUnit unit) { throw new NotImplementedException(); }
    public static bool operator ==(Location3D left, Location3D right) { return left.Equals(right); }
    public static bool operator !=(Location3D left, Location3D right) { return !(left == right); }
  }
}
