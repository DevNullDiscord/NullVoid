using System;
using Newtonsoft.Json;

struct ProcessorData
{
  public float Speed;
  public int Cores;
  public bool bHyperThreading;
}

namespace NullVoid_Server.Game.Components
{
  public class Processor : Component
  {
    public float Speed = 1600.0f;
    public int Cores = 1;
    public bool bHyperThreading = false;
    public int LogicalCores
    {
      get
      {
        return bHyperThreading ? Cores * 2 : Cores;
      }
    }
    public Processor() { }
    public void Dispose()
    {

    }
    public string Serialize()
    {
      ProcessorData data = new ProcessorData();
      data.Speed = Speed;
      data.bHyperThreading = bHyperThreading;
      data.Cores = Cores;
      return JsonConvert.SerializeObject(data);
    }
    public void Deserialize(string data)
    {
      ProcessorData d = JsonConvert.DeserializeObject<ProcessorData>(data);
    }
  }
}