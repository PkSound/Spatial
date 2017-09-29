using MathNet.Spatial.Euclidean;
using UnitsNet;

namespace Pk.Spatial
{
  public interface IVector3D<TVector, TUnit, TUnitEnumType>
  {
    TUnit Magnitude { get; }
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
    Angle AngleTo(TVector other);
    Angle AngleTo(UnitVector3D other);
    Vector3D FreezeTo(TUnitEnumType unit);
    TVector Negate();
    UnitVector3D Normalize(TUnitEnumType unit);
    TVector Rotate(UnitVector3D axisOfRotation, Angle angleOfRotation);
  }
}
