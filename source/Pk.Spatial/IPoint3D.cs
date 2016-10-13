namespace Pk.Spatial
{
  internal interface IPoint3D<TUnit>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
  }
}
