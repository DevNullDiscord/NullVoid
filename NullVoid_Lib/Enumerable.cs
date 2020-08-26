using System;
using System.Collections.Generic;

namespace NullVoid_Lib
{
  public static class Enumerable
  {
    /// <summary>
    /// Ensure that all items from `a` also exist in `b`.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool Both<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> predicate)
    {
      foreach (T v1 in a)
      {
        bool res = false;
        foreach (T v2 in b)
        {
          res = predicate.Invoke(v1, v2);
          if (res) break;
        }
        if (!res) return false;
      }
      return true;
    }
  }
}
