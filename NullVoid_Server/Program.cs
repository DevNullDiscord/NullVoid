using System;
using System.Collections.Generic;
using NullVoid_Lib.Events;

namespace NullVoid_Server
{
  class TestComponent : Game.Component
  {
    public long tickCounter = 0;
  }
  class TestSystem : Game.ComponentSystem
  {
    public new readonly Type[] ComponentTypes = new Type[] { typeof(TestComponent) };
    public TestSystem(Game.World world, double interval) : base(world, interval) { }
    public override void Run(List<Game.Entity> entities)
    {
      foreach (Game.Entity e in entities)
      {
        if (e.GetComponent<TestComponent>(out TestComponent c))
        {
          Console.WriteLine($"{e}: {c.tickCounter}");
          c.tickCounter++;
        }
      }
    }
  }
  class Program
  {
    static EventSystem EventLoop = new EventSystem(1000 / 60); // 60 hz
    static Game.World World = new Game.World(EventLoop);
    static void Main(string[] args)
    {
      // Empty entity
      World.AddEntity();
      // Test entity
      World.AddEntity(new TestComponent());
      // This system should only see the test entity.
      World.RegisterSystem<TestSystem>(5000);
      EventLoop.Join();
    }
  }
}
