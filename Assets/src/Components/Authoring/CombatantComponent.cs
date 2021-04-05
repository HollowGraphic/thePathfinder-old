using Drawing;
using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public class CombatantComponent : AuthoringComponent
    {
        public Combatant combatant;
        public Aggro aggro;
        public override void Set(ref ent entity)
        {
            entity.Set(combatant);
            entity.Set(new VisibleTargets(20));
            entity.Set(aggro);
        }

        public override void DrawGizmos()
        {
            Vector3 position = transform.position;
            Draw.WireSphere(position, combatant.attackRange, Color.yellow);
            Draw.WireSphere(position, aggro.value, Color.red);
        }
    }
}