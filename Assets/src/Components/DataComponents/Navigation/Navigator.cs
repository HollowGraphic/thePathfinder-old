using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Can navigate paths
    /// </summary>
    [Serializable, HideLabel]
    public struct Navigator
    {
        [Tooltip("Distance that's used to determine when the entity should start slowing down")]
        public float breakingDistance;

        [HideInInspector] public bool navigationComplete;
        [HideInInspector] public float lastRepath;
        public float repathRate;
        public int currentPathNode;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class Component
    {
        public const string Navigator = "Game.Source.Navigator";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Navigator NavigatorComponent(in this ent entity) =>
            ref Storage<Navigator>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageNavigator : Storage<Navigator>
    {
        public override Navigator Create() => new Navigator();

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