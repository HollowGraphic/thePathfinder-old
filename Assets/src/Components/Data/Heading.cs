using System;
using System.Runtime.CompilerServices;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Unity.Mathematics;

namespace ThePathfinder.Components
{
    /// <summary>
    /// normalize
    /// </summary>
    [Serializable, HideLabel]
    public struct Heading
    {
        public float3 value;

        public Heading(float3 direction)
        {
            value = direction;
        }
    }

    #region HELPERS

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    static partial class GameComponent
    {
        public const string Heading = "ThePathfinder.Components.Heading";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref Heading HeadingComponent(in this ent entity) =>
            ref Storage<Heading>.components[entity.id];
    }

    //[Il2CppSetOption(Option.NullChecks, false)]
    //[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    //[Il2CppSetOption(Option.DivideByZeroChecks, false)]
    sealed class StorageHeading : Storage<Heading>
    {
        public override Heading Create() => new Heading();

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