using System;
using System.Runtime.CompilerServices;
//using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Identifies what entity is hunting this one
    /// </summary>
    [Serializable, HideLabel]
    public struct Predator
    {
        public ent Value;
    }

    #region HELPERS

    // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Predator = "ThePathfinder.Components.Predator";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Predator PredatorComponent(in this ent entity) =>
            ref Storage<Predator>.components[entity.id];
    }

    // [Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageComponentPredator : Storage<Predator>
    {
        public override Predator Create() => new Predator();

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