using System;
using System.Collections.Generic;

namespace NullVoid_Lib.Concurrency
{
  public class ConcurrentList<T> : AutoMutex<List<T>>
  {
    public ConcurrentList() : base(new List<T>()) { }
    public ConcurrentList(IEnumerable<T> init) : base(new List<T>(init)) { }
  }
}