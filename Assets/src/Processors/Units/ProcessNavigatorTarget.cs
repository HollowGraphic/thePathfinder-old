using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Units
{
    /// <summary>
    /// Handle new target on navigator
    /// </summary>
    internal sealed class ProcessNavigatorTarget : Processor
    {
        [ExcludeBy(GameComponent.MoveToDestination)]
        private readonly Group<Navigator, Target, Combatant> _targetingUnits = default;

        public override void HandleEcsEvents()
        {
            foreach (var unit in _targetingUnits.added)
            {
                var target = unit.TargetComponent();
                Vector3 targetPos;
                //HACK snap position to the targets feet so that we get a better position for naving, might have to come up with a 'Feet' component
                bool hitGround = Physics.Raycast(target.Value.transform.position, Vector3.down, out var hit);
                if (hitGround)
                    targetPos = hit.point;
                else
                    targetPos = target.Value.transform.position;
                var dir = unit.transform.position - targetPos;
                float attackRange = unit.CombatantComponent().attackRange;
                if (dir.sqrMagnitude < attackRange) continue; //we are within attack range

                if (unit.Has<Destination>() &&
                    unit.DestinationComponent().destinationType != DestinationType.Target
                ) //handle previous destination type
                {
                    unit.DestinationQueueComponent().destinations.Enqueue(unit.DestinationComponent());
                }

                //not within attack range, calculate new destination
                var norm = dir.normalized;
                var offset =
                    norm * (attackRange - .5f); //HACK reduce attack range a fuzz so that we can be WITHIN attack range
                //set destination to target position, and request a path
                unit.Get<Destination>() = new Destination(offset + targetPos, DestinationType.Target);
                unit.Get<PathRequest>();
            }
        }
    }
}