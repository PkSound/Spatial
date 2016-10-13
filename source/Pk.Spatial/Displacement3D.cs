using System;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using UnitsNet;
using Angle = UnitsNet.Angle;

namespace Pk.Spatial
{
  /// <summary>
  ///   Represents a 3D length vector.
  /// </summary>
  public struct Displacement3D : IEquatable<Displacement3D>, IVector3D<Length>
  {
    private static readonly IHaveXyzEqualityComparer<Length> comparer = new IHaveXyzEqualityComparer<Length>();
    private readonly Vector3D underlyingVector;
    public Displacement3D(double x, double y, double z) { this.underlyingVector = new Vector3D(x, y, z); }
    public Displacement3D(Length x, Length y, Length z) : this(x.Meters, y.Meters, z.Meters) { }


    public Displacement3D(UnitVector3D direction, Length magnitude) : this()
    {
      var length = magnitude.Meters;
      var x = direction.X*length;
      var y = direction.Y*length;
      var z = direction.Z*length;

      this.underlyingVector = new Vector3D(x, y, z);
    }


    public bool Equals(Displacement3D other) { return comparer.Equals(this, other); }
    public Length Magnitude => Length.From(this.underlyingVector.Length, StandardUnits.Length);
    public Length X => Length.From(this.underlyingVector.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingVector.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingVector.Z, StandardUnits.Length);
    public override int GetHashCode() { return comparer.GetHashCode(this); }
    public static bool operator ==(Displacement3D left, Displacement3D right) { return left.Equals(right); }
    public static bool operator !=(Displacement3D left, Displacement3D right) { return !left.Equals(right); }


    public Displacement3D Rotate(UnitVector3D axisOfRotation, Angle angleOfRotation)
    {
      var degrees = angleOfRotation.Degrees;
      var rotatedUnderlyingVector = this.underlyingVector.Rotate(axisOfRotation, degrees, AngleUnit.Degrees);

      var x = rotatedUnderlyingVector.X;
      var y = rotatedUnderlyingVector.Y;
      var z = rotatedUnderlyingVector.Z;

      return new Displacement3D(x, y, z);
    }
  }
}
