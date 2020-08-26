using NullVoid_Lib.Events;

namespace NullVoid_Server
{
  class Program
  {
    static EventSystem EventLoop = new EventSystem(1000 / 60); // 60 hz
    static Game.World World = new Game.World(EventLoop);
    static void Main(string[] args)
    {
      using (var l = World.Entites.Lock())
      {
        l.Value.Add(new Game.DemoEntity(EventLoop));
      }
      EventLoop.Join();
    }
  }
}
