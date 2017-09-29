using MathNet.Spatial.Euclidean;
using UnitsNet.Units;

namespace Pk.Spatial
{
  public interface IPoint3D<TUnit>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
    Point3D FreezeTo(LengthUnit unit);
  }
}
