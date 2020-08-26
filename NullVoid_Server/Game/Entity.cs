using System;
using NullVoid_Lib.Events;
using NullVoid_Lib.Concurrency;

namespace NullVoid_Server.Game
{
  public class Entity : IDisposable
  {
    private EventSystem _events;
    private ConcurrentList<Component> _components = new ConcurrentList<Component>();
    public Entity(EventSystem eventSystem)
    {
      _events = eventSystem;
    }
    public virtual void Dispose()
    {
      //TODO: Dispose I guess
    }
  }
}