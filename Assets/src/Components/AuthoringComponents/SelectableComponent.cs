using System;
using UnityEngine;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using Drawing;

namespace ThePathfinder.Components.Authoring
{
    public sealed class SelectableComponent : AuthoringComponent
    {
        [SerializeField] private PointerBounds selectionBounds;
        public Action<bool> OnHoverChanged;
        public Action<bool> OnIsSelectedCandidateChanged;
        public Action<bool> OnSelectedChanged;

        public override void Set(ref ent entity)
        {
            Debug.Log("Settin up entity " + entity);
            entity.Set<Selectable>();
            entity.Set(selectionBounds);
            entity.layer.Observer.Add(entity, s => s.Has<SelectionCandidate>(),
                isCandidate => OnIsSelectedCandidateChanged(isCandidate));
            entity.layer.Observer.Add(entity, s => s.Has<Selected>(), isSelected => OnSelectedChanged(isSelected));
        }

    }
}