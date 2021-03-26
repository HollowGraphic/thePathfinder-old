using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Signals.Units;
using UnityEngine;

namespace ThePathfinder.Game
{
    /// <summary>
    /// Handle new target on navigator
    /// </summary>
    internal sealed class ProcessNavigatorTarget : Processor
    {
        [ExcludeBy(GameComponent.Passive)]
        private readonly Group<Navigator, Target, Combatant> _targetingUnits = default;
        public override void HandleEcsEvents()
        {
            foreach (ent unit in _targetingUnits.added)
            {
                Target target = unit.TargetComponent();
                //HACK snap position to the targets feet so that we get a better position for naving, might have to come up with a 'Feet' component
                Vector3 position = target.Value.transform.position;
                bool hitGround = Physics.Raycast(position, Vector3.down, out RaycastHit hit);
                Vector3 targetPos = hitGround ? hit.point : position;
                Vector3 dir = unit.transform.position - targetPos;
                float attackRange = unit.CombatantComponent().attackRange;
                if (dir.sqrMagnitude < attackRange) continue; //we are within attack range

                //not within attack range, calculate new destination
                Vector3 norm = dir.normalized;
                Vector3 offset =
                    norm * (attackRange - .5f); //HACK reduce attack range a fuzz so that we can be WITHIN attack range
                //queue order 
                Layer.Send(new AssignOrder(unit,new MoveOrder(new Destination(offset + targetPos, DestinationType.Target)), false));
               
            }
        }
    }
}