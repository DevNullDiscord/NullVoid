using System;
using System.Threading;

namespace NullVoid_Lib.Concurrency
{
  public class MutexLock<T> : IDisposable
  {
    private Mutex _mut;
    public readonly T Value;
    public MutexLock(ref Mutex mutex, ref T value)
    {
      _mut = mutex;
      Value = value;
    }
    public void Dispose()
    {
      _mut.ReleaseMutex();
    }
  }
  public class AutoMutex<T> : IDisposable
  {
    private Mutex _mut = new Mutex();
    private T _value;
    public AutoMutex(T value)
    {
      _value = value;
    }
    public MutexLock<T> Lock()
    {
      _mut.WaitOne();
      return new MutexLock<T>(ref _mut, ref _value);
    }
    public void Dispose()
    {
      _mut.Dispose();
    }
  }
  public class MutexLock : IDisposable
  {
    private Mutex _mut;
    public MutexLock(ref Mutex mut)
    {
      _mut = mut;
    }
    public void Dispose()
    {
      _mut.ReleaseMutex();
    }
  }
  public class AutoMutex : IDisposable
  {
    private Mutex _mut = new Mutex();
    public void Dispose()
    {
      _mut.Dispose();
    }
    public MutexLock Lock()
    {
      _mut.WaitOne();
      return new MutexLock(ref _mut);
    }
  }
}