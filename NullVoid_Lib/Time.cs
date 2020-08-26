using System;

namespace NullVoid_Lib
{
  public static class Time
  {
    public static double UnixEpoch
    {
      get
      {
        return (DateTime.Now - DateTime.UnixEpoch).TotalMilliseconds;
      }
    }
  }
}