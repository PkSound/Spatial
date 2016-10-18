using MathNet.Spatial.Euclidean;
using Angle = UnitsNet.Angle;

namespace Pk.Spatial
{
  public interface IVector3D<TVector, TUnit, TUnitEnumType>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
    TUnit Magnitude { get; }
    Angle AngleTo(TVector other);
    Angle AngleTo(UnitVector3D other);
    Vector3D FreezeTo(TUnitEnumType unit);
    TVector Rotate(UnitVector3D axisOfRotation, Angle angleOfRotation);
    UnitVector3D Normalize(TUnitEnumType unit);
  }
}
