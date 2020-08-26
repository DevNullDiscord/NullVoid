using System;
using NullVoid_Lib.Events;

namespace NullVoid_Server.Game
{
  public class DemoEntity : Entity
  {
    public DemoEntity(EventSystem eventSystem) : base(eventSystem) { }
    public override Action Tick()
    {
      return () =>
      {
        Console.WriteLine("Hello, Entity!");
      };
    }
  }
  public class Entity : IDisposable
  {
    private EventSystem _events;
    public bool bDidTick = false;
    public Entity(EventSystem eventSystem)
    {
      _events = eventSystem;
    }
    public virtual void Dispose()
    {
      //TODO: Dispose I guess
    }
    public virtual Action Tick()
    {
      return () => { };
    }
  }
}