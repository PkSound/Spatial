using System;

namespace Pk.Spatial
{
  /// <summary>
  ///   Represents a 3D length vector.
  /// </summary>
  public struct Displacement3D : IEquatable<Displacement3D>
  {
    public override int GetHashCode() { return base.GetHashCode(); }
    public bool Equals(Displacement3D other) { return true; }
    public static bool operator ==(Displacement3D left, Displacement3D right) { return left.Equals(right); }
    public static bool operator !=(Displacement3D left, Displacement3D right) { return !left.Equals(right); }
  }
}
