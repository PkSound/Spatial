namespace Pk.Spatial
{
  internal interface IHaveXyz<TUnit>
  {
    TUnit X { get; }
    TUnit Y { get; }
    TUnit Z { get; }
  }
}
