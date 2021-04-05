using System;
using System.Runtime.CompilerServices;
//using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct HealthModifier
    {
        public float value;

        public HealthModifier(float mod)
        {
            value = mod;
        }
    }

    #region HELPERS

    // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string HealthModifier = "ThePathfinder.Components.HealthModifier";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref HealthModifier HealthModifierComponent(in this ent entity) =>
            ref Storage<HealthModifier>.components[entity.id];
    }

    // [Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageComponentHealthModifier : Storage<HealthModifier>
    {
        public override HealthModifier Create() => new HealthModifier();

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