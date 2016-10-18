using System;
using MathNet.Spatial.Euclidean;
using UnitsNet;
using UnitsNet.Units;
using AngleUnit = MathNet.Spatial.Units.AngleUnit;

namespace Pk.Spatial
{
  public struct ForceVector3D : IVector3D<ForceVector3D, Force, ForceUnit>, IEquatable<ForceVector3D>
  {
    private readonly Vector3D underlyingVector;


    public ForceVector3D(Force x, Force y, Force z)
    {
      this.underlyingVector = new Vector3D(x.As(Force.BaseUnit), y.As(Force.BaseUnit), z.As(Force.BaseUnit));
    }


    public bool Equals(ForceVector3D other) { return this.underlyingVector.Equals(other.FreezeTo(Force.BaseUnit)); }


    public Angle AngleTo(ForceVector3D other)
    {
      var result = this.FreezeTo(Force.BaseUnit).AngleTo(other.FreezeTo(Force.BaseUnit));
      return Angle.FromDegrees(result.Degrees);
    }


    public Angle AngleTo(UnitVector3D other)
    {
      var result = this.FreezeTo(Force.BaseUnit).AngleTo(other);
      return Angle.FromDegrees(result.Degrees);
    }


    public Vector3D FreezeTo(ForceUnit unit) { return new Vector3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit)); }


    public ForceVector3D Rotate(UnitVector3D axisOfRotation, Angle angleOfRotation)
    {
      var degrees = angleOfRotation.Degrees;
      var rotatedUnderlyingVector = this.underlyingVector.Rotate(axisOfRotation, degrees, AngleUnit.Degrees);

      return ForceVector3D.From(rotatedUnderlyingVector, Force.BaseUnit);
    }


    public UnitVector3D Normalize(ForceUnit unit) { throw new NotImplementedException(); }
    public Force X => Force.From(this.underlyingVector.X, Force.BaseUnit);
    public Force Y => Force.From(this.underlyingVector.Y, Force.BaseUnit);
    public Force Z => Force.From(this.underlyingVector.Z, Force.BaseUnit);
    public Force Magnitude => Force.From(this.underlyingVector.Length, Force.BaseUnit);


    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals(null, obj)) return false;
      return obj is ForceVector3D && this.Equals((ForceVector3D) obj);
    }


    public static ForceVector3D From(double x, double y, double z, ForceUnit unit)
    {
      return new ForceVector3D(Force.From(x, unit), Force.From(y, unit), Force.From(z, unit));
    }


    public static ForceVector3D From(UnitVector3D direction, Force magnitude)
    {
      var x = direction.X*magnitude;
      var y = direction.Y*magnitude;
      var z = direction.Z*magnitude;

      return new ForceVector3D(x, y, z);
    }


    public static ForceVector3D From(Vector3D vector, ForceUnit unit)
    {
      return ForceVector3D.From(vector.X, vector.Y, vector.Z, unit);
    }


    public static ForceVector3D FromNewtons(double x, double y, double z)
    {
      return ForceVector3D.From(x, y, z, ForceUnit.Newton);
    }


    public override int GetHashCode() { return this.underlyingVector.GetHashCode(); }
    public UnitVector3D NormalizeToNewtons() { return this.Normalize(ForceUnit.Newton); }


    public static ForceVector3D operator +(ForceVector3D lhs, ForceVector3D rhs)
    {
      var frozenToStandard = lhs.FreezeTo(Force.BaseUnit) + rhs.FreezeTo(Force.BaseUnit);
      return ForceVector3D.From(frozenToStandard, Force.BaseUnit);
    }


    public static bool operator ==(ForceVector3D lhs, ForceVector3D rhs) { return lhs.Equals(rhs); }
    public static bool operator !=(ForceVector3D lhs, ForceVector3D rhs) { return !(lhs == rhs); }


    public static ForceVector3D operator -(ForceVector3D lhs, ForceVector3D rhs)
    {
      var frozenToStandard = lhs.FreezeTo(Force.BaseUnit) - rhs.FreezeTo(Force.BaseUnit);
      return ForceVector3D.From(frozenToStandard, Force.BaseUnit);
    }
  }
}
