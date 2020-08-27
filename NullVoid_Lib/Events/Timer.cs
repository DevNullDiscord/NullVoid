using System;
using System.Threading.Tasks;

namespace NullVoid_Lib.Events
{
  public class Timer
  {
    private readonly double _interval;
    private readonly Action _func;
    private bool bIsStopped = true;
    public Timer(Action func, double interval)
    {
      _interval = interval;
      _func = func;
    }
    private async Task Loop()
    {
      Task t = new Task(() =>
      {
        _func.Invoke();
        double _t = Time.UnixEpoch;
        while (Time.UnixEpoch - _t < _interval) { }
        if (!bIsStopped) Loop();
      });
      t.Start();
      await t;
    }
    public void Start()
    {
      bIsStopped = false;
      Loop();
    }
    public void Stop()
    {
      bIsStopped = true;
    }
  }
}