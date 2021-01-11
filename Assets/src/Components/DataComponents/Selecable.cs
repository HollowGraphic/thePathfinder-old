using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;
namespace ThePathfinder.Components
{
  [Serializable, HideLabel]
  public struct Selectable
  {
    //Data

  }
#region HELPERS
  public static partial class GameComponent
  {
    public const string Selectable = "ThePathfinder.Components.Selectable";
    internal static ref Selectable SelectableComponent(in this ent entity)
        => ref Storage<Selectable>.components[entity.id];
  }
  internal sealed class StorageSelectable : Storage<Selectable>
  {
    public override Selectable Create() => new Selectable();
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