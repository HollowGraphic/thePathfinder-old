using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Vision
    {
        public bool cone;

        [ShowIf("cone"), LabelText("Width"), Range(0, 360)]
        public float angle;

        public float range;
        public float height;
        public LayerMask mask;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Vision = "ThePathfinder.Components.Vision";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Vision VisionComponent(in this ent entity) =>
            ref Storage<Vision>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageVision : Storage<Vision>
    {
        public override Vision Create() => new Vision();

        // Use for cleaning components that were removed at the current frame.
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