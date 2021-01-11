using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct VisibleTargets
    {
        public ents value;

        public VisibleTargets(int capacity)
        {
            value = new ents(capacity);
        }
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class Component
    {
        public const string VisibleTargets = "ThePathfinder.Components.VisibleTargets";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref VisibleTargets VisibleTargetsComponent(in this ent entity) =>
            ref Storage<VisibleTargets>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageVisibleTargets : Storage<VisibleTargets>
    {
        public override VisibleTargets Create() => new VisibleTargets();

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