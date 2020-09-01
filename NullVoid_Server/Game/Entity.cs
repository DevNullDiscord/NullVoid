using System;
using NullVoid_Lib.Events;
using NullVoid_Lib.Concurrency;
using NullVoid_Lib;

namespace NullVoid_Server.Game
{
  public class Entity : IDisposable
  {
    private EventSystem _events;
    public readonly ConcurrentList<Component> Components = new ConcurrentList<Component>();
    public Entity(EventSystem eventSystem)
    {
      _events = eventSystem;
    }
    public bool GetComponent<T>(out T component) where T : Component
    {
      using (var l = Components.Lock())
      {
        if (Enumerable.Find(l.Value, (comp) =>
        {
          return comp.GetType() == typeof(T);
        }, out Component c))
        {
          component = c as T;
          return true;
        }
      }
      component = default(T);
      return false;
    }
    public virtual void Dispose()
    {
      //TODO: Dispose I guess
    }
  }
}