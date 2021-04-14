using System;
using System.Runtime.CompilerServices;
//using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Rotation
    {
        public Quaternion value;

        public Rotation(quaternion rotation)
        {
            value = rotation;
        }
    }

    #region HELPERS

    // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Rotation = "ThePathfinder.Components.Rotation";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Rotation RotationComponent(in this ent entity) =>
            ref Storage<Rotation>.components[entity.id];
    }

    // [Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageComponentRotation : Storage<Rotation>
    {
        public override Rotation Create() => new Rotation();

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