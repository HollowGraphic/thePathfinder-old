using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public class MaxSpeed
    {
        [LabelText("Max Speed")] public float value;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class Component
    {
        public const string MaxSpeed = "ThePathfinder.Components.MaxSpeed";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref MaxSpeed MaxSpeedComponent(in this ent entity) =>
            ref Storage<MaxSpeed>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageMaxSpeed : Storage<MaxSpeed>
    {
        public override MaxSpeed Create() => new MaxSpeed();

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