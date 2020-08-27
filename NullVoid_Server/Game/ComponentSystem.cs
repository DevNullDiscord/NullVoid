using System;
using System.Collections.Generic;
using NullVoid_Lib.Events;

namespace NullVoid_Server.Game
{
  public class ComponentSystem
  {
    public readonly Type[] ComponentTypes = new Type[0];
    private readonly World _world;
    private readonly Timer _timer;
    public ComponentSystem(World world, double interval)
    {
      _world = world;
      _timer = new Timer(() =>
      {
        ScheduleRun();
      }, interval);
    }
    public void Start()
    {
      _timer.Start();
    }
    public void Stop()
    {
      _timer.Stop();
    }
    public virtual void Run(List<Entity> entities) { }
    public void ScheduleRun()
    {
      Run(_world.GetMatchingEntities(ComponentTypes));
    }
  }
}