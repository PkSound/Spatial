using MathNet.Spatial.Euclidean;
using UnitsNet.Units;

namespace Pk.Spatial
{
  public interface IVector3D<TUnit>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
    TUnit Magnitude { get; }
    Vector3D Freeze(LengthUnit unit = StandardUnits.Length);
  }
}