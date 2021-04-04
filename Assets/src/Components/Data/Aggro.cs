using System;
using System.Runtime.CompilerServices;
//using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Indicates how far entities can be detected.
    /// <remarks>Do not confuse this with vision, entities within radar range are not necessarily visible</remarks>
    /// </summary>
    [Serializable, HideLabel]
    public struct Aggro
    {
        [LabelText("Aggro Range"),
         PropertyTooltip("How close does an entity have to be in order to start trying to attack it?")]
        
        public float value;
    }

    #region HELPERS

    // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Radar = "ThePathfinder.Components.Aggro";
        /// <summary>
        /// Data for how close entities need to be in order to be considered for attack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Aggro AggroComponent(in this ent entity) =>
            ref Storage<Aggro>.components[entity.id];
    }

    // [Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageComponentRadar : Storage<Aggro>
    {
        public override Aggro Create() => new Aggro();

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