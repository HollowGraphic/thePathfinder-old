using System;
using System.Runtime.CompilerServices;
//using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using ThePathfinder.Game;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct UnitOrderQueueInfo
    {
        public OrderType nextOrderType;
    }

    

    #region HELPERS

    // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string UnitOrderQueueInfo = "ThePathfinder.Components.UnitOrderQueueInfo";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref UnitOrderQueueInfo UnitOrderQueueInfoComponent(in this ent entity) =>
            ref Storage<UnitOrderQueueInfo>.components[entity.id];
    }

    // [Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageComponentUnitQueueInfo : Storage<UnitOrderQueueInfo>
    {
        public override UnitOrderQueueInfo Create() => new UnitOrderQueueInfo();

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
