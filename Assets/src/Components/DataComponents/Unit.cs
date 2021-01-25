using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Unit
    {
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string Unit = "ThePathfinder.Components.Unit";

        internal static ref Unit UnitComponent(in this ent entity)
            => ref Storage<Unit>.components[entity.id];
    }

    internal sealed class StorageUnit : Storage<Unit>
    {
        public override Unit Create() => new Unit();

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