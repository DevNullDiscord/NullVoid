using System;

namespace NullVoid_Server.Game
{
  public interface Component : IDisposable
  {
    /// <summary>
    /// Serialize this component.
    /// </summary>
    /// <returns>The valid JSON string representing this component.</returns>
    abstract string Serialize();
    /// <summary>
    /// Deserialize a JSON string and populate the component's data.
    /// </summary>
    /// <param name="data">The valid JSON string.</param>
    abstract void Deserialize(string data);
  }
}