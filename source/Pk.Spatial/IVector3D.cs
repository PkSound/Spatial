namespace Pk.Spatial
{
  public interface IVector3D<TUnit>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
    TUnit Magnitude { get; }
  }
}