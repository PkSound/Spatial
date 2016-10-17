using System;
using MathNet.Spatial.Euclidean;
using UnitsNet;
using UnitsNet.Units;

namespace Pk.Spatial
{
  public struct ForceVector3D : IVector3D<Force>, ICanFreeze<ForceUnit>
  {
    private readonly Vector3D underlyingVector;

    public ForceVector3D(Force x, Force y, Force z)
    {
      this.underlyingVector = new Vector3D(x.As(StandardUnits.Force), y.As(StandardUnits.Force), z.As(StandardUnits.Force));
    }


    public Vector3D FreezeTo(ForceUnit unit)
    {
      return new Vector3D(this.X.As(unit), this.Y.As(unit), this.Z.As(unit));
    }
    public Force X => Force.From(this.underlyingVector.X, StandardUnits.Force);
    public Force Y => Force.From(this.underlyingVector.Y, StandardUnits.Force);
    public Force Z => Force.From(this.underlyingVector.Z, StandardUnits.Force);
    public Force Magnitude => Force.From(this.underlyingVector.Length, StandardUnits.Force);

    public static ForceVector3D From(double x, double y, double z, ForceUnit unit)
    {
    return   new ForceVector3D(Force.From(x,unit), Force.From(y, unit), Force.From(z, unit));
    }


    public static ForceVector3D From(UnitVector3D direction, Force magnitude)
    {
      var x = direction.X*magnitude;
      var y = direction.Y*magnitude;
      var z = direction.Z*magnitude;

      return new ForceVector3D(x,y,z);
    }


    public static ForceVector3D From(Vector3D vector, ForceUnit unit)
    {
      return ForceVector3D.From(vector.X, vector.Y, vector.Z, unit);
    }


    public static ForceVector3D FromNewtons(double x, double y, double z)
    {
      return ForceVector3D.From(x, y, z, ForceUnit.Newton);
    }
  }
}
