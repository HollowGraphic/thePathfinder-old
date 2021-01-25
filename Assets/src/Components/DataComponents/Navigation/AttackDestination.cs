using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct AttackDestination
    {
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    // ReSharper disable once InconsistentNaming
    static partial class GameComponent
    {
        public const string AttackDestination = "ThePathfinder.Components.Authoring.AttackDestination";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref AttackDestination AttackDestinationComponent(in this ent entity) =>
            ref Storage<AttackDestination>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageAttackDestination : Storage<AttackDestination>
    {
        public override AttackDestination Create() => new AttackDestination();

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