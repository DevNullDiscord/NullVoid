using System;
using System.Collections.Generic;
using NullVoid_Lib.Events;
using NullVoid_Lib.Concurrency;
using System.Threading;

namespace NullVoid_Server.Game
{
  public class World
  {
    public readonly ConcurrentList<Entity> Entites = new ConcurrentList<Entity>();
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
    public List<Entity> GetMatchingEntities(params Type[] types)
    {
      List<Entity> entities = new List<Entity>();
      using (var l = Entites.Lock())
      {
        foreach (Entity ent in l.Value)
        {

        }
      }
      return entities;
    }
  }
}