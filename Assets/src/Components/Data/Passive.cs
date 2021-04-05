using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Passive//TODO generalize this (something like 'Passive'|| 'Aggressive'
    {
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Passive = "ThePathfinder.Components.Passive";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Passive PassiveComponent(in this ent entity) =>
            ref Storage<Passive>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StoragePassive : Storage<Passive>
    {
        public override Passive Create() => new Passive();

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