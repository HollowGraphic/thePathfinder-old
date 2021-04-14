using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Defines an entity that can move
    /// <remarks>Translation and Rotation</remarks>
    /// </summary>
    [Serializable, HideLabel]
    public struct Translator
    {
        [HideInInspector] public bool canMove;
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Translator = "ThePathfinder.Components.Translator";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Translator TranslatorComponent(in this ent entity) =>
            ref Storage<Translator>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageMover : Storage<Translator>
    {
        public override Translator Create() => new Translator();

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