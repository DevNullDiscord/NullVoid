using NullVoid_Lib.Concurrency;
using System.Collections.Generic;
using NullVoid_Lib.Events;
using System;

namespace NullVoid_Server.Game
{
  public class Container : Entity, IDisposable
  {
    private ConcurrentList<Entity> _contained = new ConcurrentList<Entity>();
    public Container(EventSystem eventSystem) : base(eventSystem) { }
    public MutexLock<List<Entity>> GetContents()
    {
      return _contained.Lock();
    }
    public override void Dispose()
    {
      _contained.Dispose();
    }
  }
}