using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Defines an entity that can move
    /// <remarks>Translation and Rotation</remarks>
    /// </summary>
    [Serializable, HideLabel]
    public struct Mover
    {
        [HideInInspector]
        public bool canMove;
        public bool canRotate;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class Component
    {
        public const string Mover = "ThePathfinder.Components.Mover";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Mover MoverComponent(in this ent entity) =>
            ref Storage<Mover>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageMover : Storage<Mover>
    {
        public override Mover Create() => new Mover();

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