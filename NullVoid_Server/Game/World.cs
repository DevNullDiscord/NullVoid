using System;
using System.Collections.Generic;
using NullVoid_Lib.Events;
using NullVoid_Lib.Concurrency;
using NullVoid_Lib;
using System.Threading;
using System.Reflection;

namespace NullVoid_Server.Game
{
  public class World
  {
    public readonly ConcurrentList<Entity> Entites = new ConcurrentList<Entity>();
    public readonly ConcurrentList<ComponentSystem> Systems = new ConcurrentList<ComponentSystem>();
    public readonly Thread WorldThread;
    private readonly EventSystem _events;
    public World(EventSystem eventSystem)
    {
      _events = eventSystem;
      WorldThread = new Thread(TickHandler);
      WorldThread.Start();
    }
    private void TickHandler()
    {
      while (!_events.Cancellation.IsCancellationRequested)
      {
        using var l = Entites.Lock();
        // Do stuff here I guess.
      }
    }
    public Entity AddEntity(params Component[] components)
    {
      Entity ent = new Entity(_events);
      using (var l = ent.Components.Lock())
      {
        l.Value.AddRange(components);
      }
      using var l2 = Entites.Lock();
      l2.Value.Add(ent);
      return ent;
    }
    public T RegisterSystem<T>(double interval) where T : ComponentSystem
    {
      ConstructorInfo[] constructors = typeof(T).GetConstructors();
      if (constructors.Length == 0) throw new Exception("No valid constructor found.");
      ConstructorInfo constructor = constructors[0];
      using var l = Systems.Lock();
      T system = (T)constructor.Invoke(new object[] { this, interval });
      l.Value.Add(system);
      system.Start();
      return system;
    }
    public List<Entity> GetMatchingEntities(params Type[] types)
    {
      List<Entity> entities = new List<Entity>();
      List<Type> t = new List<Type>(types);
      using (var l = Entites.Lock())
      {
        foreach (Entity ent in l.Value)
        {
          List<Type> t2;
          using (var l2 = ent.Components.Lock())
          {
            t2 = Enumerable.Map<Type, Component>(l2.Value, (comp) =>
            {
              return comp.GetType();
            });
          }
          if (Enumerable.Both(t, t2, (Type a, Type b) =>
          {
            return a == b;
          })) entities.Add(ent);
        }
      }
      return entities;
    }
  }
}