using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct Agent
    {
        [LabelText("Size")]
        public float radius;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string Unit = "ThePathfinder.Components.Unit";

        internal static ref Agent UnitComponent(in this ent entity)
            => ref Storage<Agent>.components[entity.id];
    }

    internal sealed class StorageUnit : Storage<Agent>
    {
        public override Agent Create() => new Agent();

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