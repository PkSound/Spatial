using System;
using MathNet.Spatial.Euclidean;
using UnitsNet;
using UnitsNet.Units;
using AngleUnit = MathNet.Spatial.Units.AngleUnit;

namespace Pk.Spatial
{
  /// <summary>
  ///   Represents a 3D length vector.
  /// </summary>
  public struct Displacement3D : IEquatable<Displacement3D>, IVector3D<Length>
  {
    private readonly Vector3D underlyingVector;


    public Displacement3D(double x, double y, double z, LengthUnit units = StandardUnits.Length)
        : this(Length.From(x, units), Length.From(y, units), Length.From(z, units)) {}


    public Displacement3D(Length x, Length y, Length z)
    {
      this.underlyingVector = new Vector3D(x.As(StandardUnits.Length), y.As(StandardUnits.Length),
                                           z.As(StandardUnits.Length));
    }


    public Displacement3D(UnitVector3D direction, Length magnitude) : this()
    {
      var length = magnitude.As(StandardUnits.Length);
      var x = direction.X*length;
      var y = direction.Y*length;
      var z = direction.Z*length;

      this.underlyingVector = new Vector3D(x, y, z);
    }


    public Displacement3D(Vector3D vector, LengthUnit units = StandardUnits.Length)
        : this(Length.From(vector.X, units), Length.From(vector.Y, units), Length.From(vector.Z, units)) {}


    public bool Equals(Displacement3D other)
    {
      return this.underlyingVector.Equals(other.Freeze());
    }


    public Length Magnitude => Length.From(this.underlyingVector.Length, StandardUnits.Length);
    public Vector3D Freeze(LengthUnit unit = StandardUnits.Length) { return new Vector3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit)); }
    public Length X => Length.From(this.underlyingVector.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingVector.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingVector.Z, StandardUnits.Length);
    public override int GetHashCode() { return this.underlyingVector.GetHashCode(); }
    public static bool operator ==(Displacement3D left, Displacement3D right) { return left.Equals(right); }
    public static bool operator !=(Displacement3D left, Displacement3D right) { return !(left == right); }


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
