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
        foreach (Entity ent in l.Value)
        {
          if (ent.bDidTick) continue;
          ent.bDidTick = true;
          _events.Enqueue(() =>
          {
            ent.Tick().Invoke();
            ent.bDidTick = false;
          });
        }
      }
    }
  }
}