using MathNet.Spatial.Euclidean;
using UnitsNet;

namespace Pk.Spatial
{
  /// <summary>
  ///   Describes a location in 3D space.
  ///   X Y and Z each describe the distance from the origin in their respective axis.
  /// </summary>
  public struct Location3D
  {
    public Location3D(Length x, Length y, Length z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }


    public static Location3D Origin { get { return new Location3D(); } }
    public Length X { get; private set; }
    public Length Y { get; private set; }
    public Length Z { get; private set; }


    public static Location3D From(UnitVector3D direction, Length magnitude)
    {
      return new Location3D(direction.X*magnitude, direction.Y*magnitude, direction.Z*magnitude);
    }
  }
}
