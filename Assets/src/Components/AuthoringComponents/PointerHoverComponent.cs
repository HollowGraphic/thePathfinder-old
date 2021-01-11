using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public sealed class PointerHoverComponent : AuthoringComponent
    {
        public override void Set(ref ent entity)
        {
            entity.Set<PointerHover>();
        }
    }
}