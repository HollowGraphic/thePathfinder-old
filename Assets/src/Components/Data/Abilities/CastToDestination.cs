using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Unity.Mathematics;

namespace ThePathfinder.Components
{
    /// <summary>
    /// keep casting until you reach destination
    /// </summary>
    [Serializable, HideLabel]
    public struct CastToDestination
    {
        public float3 value;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string CastToDestination = "ThePathfinder.Components.CastToDestination";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref CastToDestination CastToDestinationComponent(in this ent entity) =>
            ref Storage<CastToDestination>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageCastToDestination : Storage<CastToDestination>
    {
        public override CastToDestination Create() => new CastToDestination();

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