using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Indicates that a
    /// </summary>
    [Serializable, HideLabel]
    public struct PointerHover
    {
        public PointerHover(ent selector)
        {
            this.selector = selector;
        }

        /// <summary>
        /// Pointer owner
        /// </summary>
        public readonly ent selector;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string PointerHover = "ThePathfinder.Components.PointerHover";

        internal static ref PointerHover PointerHoverComponent(in this ent entity)
            => ref Storage<PointerHover>.components[entity.id];
    }

    internal sealed class StoragePointerHover : Storage<PointerHover>
    {
        public override PointerHover Create() => new PointerHover();

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