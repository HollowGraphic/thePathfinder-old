using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Unity.Mathematics;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Destination
    {
        public Destination(float3 location)
        {
            value = location;
        }

        public float3 value;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Destination = "ThePathfinder.Components.Destination";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Destination DestinationComponent(in this ent entity) =>
            ref Storage<Destination>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageDestination : Storage<Destination>
    {
        public override Destination Create() => new Destination();

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