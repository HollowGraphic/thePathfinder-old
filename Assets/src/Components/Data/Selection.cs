using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Selection
    {
        public Selection(int capacity)
        {
            value = new ents(capacity);
        }

        //INVESTIGATE do we want to collect entities by type?
        public ents value;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string Selection = "ThePathfinder.Components.Selection";

        internal static ref Selection SelectionComponent(in this ent entity)
            => ref Storage<Selection>.components[entity.id];
    }

    internal sealed class StorageSelection : Storage<Selection>
    {
        public override Selection Create() => new Selection();

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