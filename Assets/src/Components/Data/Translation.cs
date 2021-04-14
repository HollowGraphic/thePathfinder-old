using System;
using System.Runtime.CompilerServices;
//using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Translation
    {
        public Vector3 value;

        public Translation(Vector3 translation)
        {
            value = translation;
        }
    }

    #region HELPERS

    // [Unity.IL2CPP.CompilerServices.Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Translation = "ThePathfinder.Components.Translation";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Translation TranslationComponent(in this ent entity) =>
            ref Storage<Translation>.components[entity.id];
    }

    // [Il2CppSetOption(Option.NullChecks, false)]
    // [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    // [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageComponentTranslation : Storage<Translation>
    {
        public override Translation Create() => new Translation();

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