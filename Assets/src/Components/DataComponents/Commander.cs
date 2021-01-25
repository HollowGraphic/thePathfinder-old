using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    /// <summary>
    /// Commander component, contains id
    /// </summary>
    [Serializable, HideLabel]
    public struct Commander
    {
        /// <summary>
        /// Create commander with id
        /// </summary>
        /// <param name="id"></param>
        public Commander(int id)
        {
            this.ID = id;
        }

        /// <summary>
        /// Rewired.Player ID
        /// </summary>
        public readonly int ID;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string Commander = "ThePathfinder.Components.Commander";

        internal static ref Commander CommanderComponent(in this ent entity)
            => ref Storage<Commander>.components[entity.id];
    }

    internal sealed class StorageCommander : Storage<Commander>
    {
        public override Commander Create() => new Commander();

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