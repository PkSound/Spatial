﻿using System;
using System.Collections.Generic;

namespace Pk.Spatial
{
  internal sealed class IHaveXyzEqualityComparer<TUnit> : IEqualityComparer<IHaveXyz<TUnit>> where TUnit : IEquatable<TUnit>
  {
    public bool Equals(IHaveXyz<TUnit> x, IHaveXyz<TUnit> y)
    {
      if (x == null || y == null) return false;
      if (x.X.Equals(y.X) && x.Y.Equals(y.Y))
        return x.Z.Equals(y.Z);
      return false;
    }

    public int GetHashCode(IHaveXyz<TUnit> obj)
    {
      var num1 = obj.X;
      var num2 = num1.GetHashCode() * 397;
      num1 = obj.Y;
      var hashCode1 = num1.GetHashCode();
      var num3 = (num2 ^ hashCode1) * 397;
      num1 = obj.Z;
      var hashCode2 = num1.GetHashCode();
      return num3 ^ hashCode2;
    }
  }
}