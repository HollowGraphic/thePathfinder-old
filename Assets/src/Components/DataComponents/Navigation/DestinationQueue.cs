using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Unity.Mathematics;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct DestinationQueue
    {
        public Queue<Destination> destinations;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class Component
    {
        public const string DestinationQueue = "ThePathfinder.Components.DestinationQueue";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref DestinationQueue DestinationQueueComponent(in this ent entity) =>
            ref Storage<DestinationQueue>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageDestinationQueue : Storage<DestinationQueue>
    {
        public override DestinationQueue Create() => new DestinationQueue();

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