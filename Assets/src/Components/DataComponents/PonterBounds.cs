using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Identifies an area that can be detected by a pointer
    /// </summary>
    [Serializable, HideLabel]
    public struct PointerBounds
    {
        public PointerBounds(BoxCollider bounds)
        {
            value = bounds;
        }

        //Data
        [LabelText("Selection Bounds"), Tooltip("Box Collider used to define selection bounds")]
        //public Bounds value;
        public BoxCollider value;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string PointerBounds = "ThePathfinder.Components.PointerBounds";

        internal static ref PointerBounds PointerBoundsComponent(in this ent entity)
            => ref Storage<PointerBounds>.components[entity.id];
    }

    internal sealed class StoragePointerBounds : Storage<PointerBounds>
    {
        public override PointerBounds Create() => new PointerBounds();

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