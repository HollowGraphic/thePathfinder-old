using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;
namespace ThePathfinder.Components
{
  [Serializable, HideLabel]
  public struct Selected
  {
    public Selected(ent selector)//INVESTIGATE if this is needed
    {
      commander = selector;
    }
    /// <summary>
    /// entity who selected this entity
    /// </summary>
    public ent commander;
  }
  #region HELPERS
  public static partial class GameComponent
  {
    public const string Selected = "ThePathfinder.Components.Selected";
    internal static ref Selected SelectedComponent(in this ent entity)
        => ref Storage<Selected>.components[entity.id];
  }
  internal sealed class StorageSelected : Storage<Selected>
  {
    public override Selected Create() => new Selected();
    public override void Dispose(indexes disposed)
    {
      foreach (var id in disposed)
      {
        ref var component = ref components[id];
      }
    }
  }
  #endregion
}