using System;
using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components
{
    [Serializable, HideLabel]
    public struct SelectionCandidate
    {
        public SelectionCandidate(int player)
        {
            this.player = player;
        }

        //Data
        public int player;
    }

    #region HELPERS

    public static partial class GameComponent
    {
        public const string SelectionCandidate = "ThePathfinder.Components.SelectionCandidate";

        internal static ref SelectionCandidate SelectionCandidateComponent(in this ent entity)
            => ref Storage<SelectionCandidate>.components[entity.id];
    }

    internal sealed class StorageSelectionCandidate : Storage<SelectionCandidate>
    {
        public override SelectionCandidate Create() => new SelectionCandidate();

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