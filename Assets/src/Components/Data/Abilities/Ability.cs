using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Ability
    {
        /// <summary>
        /// The Id used to refer to an ability
        /// </summary>
        [HideInInspector]
        public int id;
        [LabelText("Cast Range"), Tooltip("A value of zero is considered infinite range")]
        public float range;
        /// <summary>
        /// Is ability ready to fire 
        /// </summary>
        [HideInInspector]
        public bool ready;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Ability = "ThePathfinder.Components.Ability";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Ability AbilityComponent(in this ent entity) =>
            ref Storage<Ability>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageAbility : Storage<Ability>
    {
        public override Ability Create() => new Ability();

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