using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct MoveToDestination
    {
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string MoveToDestination = "ThePathfinder.Components.MoveToDestination";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref MoveToDestination MoveToDestinationComponent(in this ent entity) =>
            ref Storage<MoveToDestination>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageMoveToDestination : Storage<MoveToDestination>
    {
        public override MoveToDestination Create() => new MoveToDestination();

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