using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct RotationSpeed
    {
        [LabelText("Rotation Speed")] public float value;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string RotationSpeed = "ThePathfinder.Components.RotationSpeed";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref RotationSpeed RotationSpeedComponent(in this ent entity) =>
            ref Storage<RotationSpeed>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageRotationSpeed : Storage<RotationSpeed>
    {
        public override RotationSpeed Create() => new RotationSpeed();

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