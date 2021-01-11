using System.Collections.Generic;
using Pixeye.Actors;

namespace ThePathfinder.Components
{
  public interface IComponent
  {
    public void Set(ref ent entity);
    public void RegisterObservers(ent entity);
  }
}