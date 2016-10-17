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
  public struct Displacement3D : IEquatable<Displacement3D>, IVector3D<Displacement3D, Length, LengthUnit>
  {
    private readonly Vector3D underlyingVector;


    public Displacement3D(Length x, Length y, Length z)
    {
      this.underlyingVector = new Vector3D(x.As(Length.BaseUnit), y.As(Length.BaseUnit), z.As(Length.BaseUnit));
    }


    public bool Equals(Displacement3D other) { return this.underlyingVector.Equals(other.FreezeTo(Length.BaseUnit)); }
    public Length Magnitude => Length.From(this.underlyingVector.Length, Length.BaseUnit);


    public Vector3D FreezeTo(LengthUnit unit)
    {
      return new Vector3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit));
    }


    public Length X => Length.From(this.underlyingVector.X, Length.BaseUnit);
    public Length Y => Length.From(this.underlyingVector.Y, Length.BaseUnit);
    public Length Z => Length.From(this.underlyingVector.Z, Length.BaseUnit);


    public Angle AngleTo(Displacement3D other)
    {
      var result = this.FreezeTo(Length.BaseUnit).AngleTo(other.FreezeTo(Length.BaseUnit));
      return Angle.FromDegrees(result.Degrees);
    }


    public Angle AngleTo(UnitVector3D other)
    {
      var result = this.FreezeTo(Length.BaseUnit).AngleTo(other);
      return Angle.FromDegrees(result.Degrees);
    }


    public Displacement3D Rotate(UnitVector3D axisOfRotation, Angle angleOfRotation)
    {
      var degrees = angleOfRotation.Degrees;
      var rotatedUnderlyingVector = this.underlyingVector.Rotate(axisOfRotation, degrees, AngleUnit.Degrees);

      return Displacement3D.From(rotatedUnderlyingVector, Length.BaseUnit);
    }


    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals(null, obj)) return false;
      return obj is Displacement3D && this.Equals((Displacement3D) obj);
    }


    public static Displacement3D From(Vector3D vector, LengthUnit unit)
    {
      return Displacement3D.From(vector.X, vector.Y, vector.Z, unit);
    }


    public static Displacement3D From(double x, double y, double z, LengthUnit unit)
    {
      return new Displacement3D(Length.From(x, unit), Length.From(y, unit), Length.From(z, unit));
    }


    public static Displacement3D From(UnitVector3D direction, Length magnitude)
    {
      var x = direction.X*magnitude;
      var y = direction.Y*magnitude;
      var z = direction.Z*magnitude;

      return new Displacement3D(x, y, z);
    }


    public static Displacement3D FromMeters(double x, double y, double z)
    {
      return Displacement3D.From(x, y, z, LengthUnit.Meter);
    }


    public override int GetHashCode() { return this.underlyingVector.GetHashCode(); }


    public static Displacement3D operator +(Displacement3D lhs, Displacement3D rhs)
    {
      var frozenToStandard = lhs.FreezeTo(Length.BaseUnit) + rhs.FreezeTo(Length.BaseUnit);
      return Displacement3D.From(frozenToStandard, Length.BaseUnit);
    }


    public static bool operator ==(Displacement3D lhs, Displacement3D rhs) { return lhs.Equals(rhs); }
    public static bool operator !=(Displacement3D lhs, Displacement3D rhs) { return !(lhs == rhs); }


    public static Displacement3D operator -(Displacement3D lhs, Displacement3D rhs)
    {
      var frozenToStandard = lhs.FreezeTo(Length.BaseUnit) - rhs.FreezeTo(Length.BaseUnit);
      return Displacement3D.From(frozenToStandard, Length.BaseUnit);
    }
  }
}
