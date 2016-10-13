namespace Pk.Spatial
{
  internal interface IVector3D<TUnit> : IPoint3D<TUnit>
  {
    TUnit Magnitude { get; }
  }
}