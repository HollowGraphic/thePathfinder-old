using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ThePathfinder
{
  public class Float3Comparer : IEqualityComparer<float3>
  {
    public bool Equals(float3 x, float3 y)
    {

      return math.Equals(x, y);
    }

    public int GetHashCode(float3 obj)
    {
      int hCode = (int) obj.x ^ (int) obj.y ^ (int) obj.z;
      return hCode.GetHashCode();
    }
  }
}