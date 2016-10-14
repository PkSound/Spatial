using UnitsNet.Units;

namespace Pk.Spatial
{
  public interface IPoint3D<TUnit>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
    IPoint3D<double> Freeze(LengthUnit unit);
  }
}
