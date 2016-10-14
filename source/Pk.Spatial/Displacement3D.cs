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


    public Displacement3D(double x, double y, double z, LengthUnit unit)
        : this(Length.From(x, unit), Length.From(y, unit), Length.From(z, unit)) {}


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


    public Displacement3D(Vector3D vector, LengthUnit unit)
        : this(Length.From(vector.X, unit), Length.From(vector.Y, unit), Length.From(vector.Z, unit)) {}


    public bool Equals(Displacement3D other) { return this.underlyingVector.Equals(other.FreezeTo(StandardUnits.Length)); }
    public Length Magnitude => Length.From(this.underlyingVector.Length, StandardUnits.Length);


    public Vector3D FreezeTo(LengthUnit unit)
    {
      return new Vector3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit));
    }


    public Length X => Length.From(this.underlyingVector.X, StandardUnits.Length);
    public Length Y => Length.From(this.underlyingVector.Y, StandardUnits.Length);
    public Length Z => Length.From(this.underlyingVector.Z, StandardUnits.Length);


    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals(null, obj)) return false;
      return obj is Displacement3D && this.Equals((Displacement3D) obj);
    }


    public override int GetHashCode() { return this.underlyingVector.GetHashCode(); }


    public static Displacement3D operator +(Displacement3D lhs, Displacement3D rhs)
    {
      var frozenToStandard = lhs.FreezeTo(StandardUnits.Length) + rhs.FreezeTo(StandardUnits.Length);
      return new Displacement3D(frozenToStandard, StandardUnits.Length);
    }


    public static bool operator ==(Displacement3D lhs, Displacement3D rhs) { return lhs.Equals(rhs); }
    public static bool operator !=(Displacement3D lhs, Displacement3D rhs) { return !(lhs == rhs); }


    public static Displacement3D operator -(Displacement3D lhs, Displacement3D rhs)
    {
      var frozenToStandard = lhs.FreezeTo(StandardUnits.Length) - rhs.FreezeTo(StandardUnits.Length);
      return new Displacement3D(frozenToStandard, StandardUnits.Length);
    }


    public Displacement3D Rotate(UnitVector3D axisOfRotation, Angle angleOfRotation)
    {
      var degrees = angleOfRotation.Degrees;
      var rotatedUnderlyingVector = this.underlyingVector.Rotate(axisOfRotation, degrees, AngleUnit.Degrees);

      var x = rotatedUnderlyingVector.X;
      var y = rotatedUnderlyingVector.Y;
      var z = rotatedUnderlyingVector.Z;

      return new Displacement3D(x, y, z, StandardUnits.Length);
    }


    public Angle AngleTo(Displacement3D other)
    {
      var result = this.FreezeTo(StandardUnits.Length).AngleTo(other.FreezeTo(StandardUnits.Length));
      return Angle.FromDegrees(result.Degrees);
    }


    public Angle AngleTo(UnitVector3D other)
    {
      var result = this.FreezeTo(StandardUnits.Length).AngleTo(other);
      return Angle.FromDegrees(result.Degrees);
    }
  }
}
