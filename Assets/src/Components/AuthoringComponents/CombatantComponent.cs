using System;
using Drawing;
using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public class CombatantComponent : AuthoringComponent
    {
        public Combatant combatant;

        public override void Set(ref ent entity)
        {
            entity.Set(combatant);
            entity.Set(new VisibleTargets(20));
        }

        public override void DrawGizmos()
        {
            Draw.WireSphere(transform.position, combatant.attackRange, Color.yellow);
        }
    }
}