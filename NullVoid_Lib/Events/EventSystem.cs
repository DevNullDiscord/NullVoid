using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace NullVoid_Lib.Events
{
  public class EventSystem
  {
    private ConcurrentQueue<Task<dynamic>> _tasks = new ConcurrentQueue<Task<dynamic>>();
    public readonly CancellationTokenSource Cancellation = new CancellationTokenSource();
    public readonly Thread EventThread;
    private double _lastTick = 0;
    public readonly double Interval;
    public EventSystem(double interval)
    {
      Interval = interval;
      EventThread = new Thread(Loop);
      EventThread.Start();
    }
    public void Join()
    {
      EventThread.Join();
    }
    public void Cancel()
    {
      Cancellation.Cancel();
    }
    private void Loop()
    {
      while (!Cancellation.IsCancellationRequested)
      {
        double elapsed = Time.UnixEpoch - _lastTick;
        if (elapsed >= Interval && _tasks.TryDequeue(out Task<dynamic> task))
        {
          if (task != null)
            task.Start();
          _lastTick = Time.UnixEpoch;
        }
      }
    }
    public async Task Enqueue(Action func)
    {
      await Enqueue(func, Cancellation.Token);
    }
    public async Task Enqueue(Action func, CancellationToken token)
    {
      Task<dynamic> t = new Task<dynamic>((sender) =>
      {
        func.Invoke();
        return null;
      }, token);
      _tasks.Enqueue(t);
      await t;
    }
    public async Task<T> Enqueue<T>(Func<T> func)
    {
      return await Enqueue<T>(func, Cancellation.Token);
    }
    public async Task<T> Enqueue<T>(Func<T> func, CancellationToken token)
    {
      Task<dynamic> t = new Task<dynamic>(() =>
      {
        return func.Invoke();
      }, token);
      _tasks.Enqueue(t);
      object res = await t;
      return (T)res;
    }
  }
}