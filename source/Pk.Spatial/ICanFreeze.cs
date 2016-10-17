using MathNet.Spatial.Euclidean;

namespace Pk.Spatial
{
  public interface ICanFreeze<TUnit>
  {
    Vector3D FreezeTo(TUnit unit);
  }
}