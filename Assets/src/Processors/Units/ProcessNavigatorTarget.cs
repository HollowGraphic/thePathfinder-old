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
        [ExcludeBy(GameComponent.Passive)]
        private readonly Group<Navigator, Target, Combatant> _targetingUnits = default;

        private readonly Group<Navigator, Target, Arrived> _predatorArrived = default;

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

                //HACK this is wrong, it only works under certain conditions
                unit.DestinationQueueComponent().destinations.Enqueue(unit.DestinationComponent());
                //not within attack range, calculate new destination
                var norm = dir.normalized;
                var offset =
                    norm * (attackRange - .5f); //HACK reduce attack range a fuzz so that we can be WITHIN attack range
                //set destination to target position, and request a path
                unit.Get<Destination>() = new Destination(offset + targetPos, DestinationType.Target);
                unit.Get<PathRequest>();
            }
            //lost/killed target
            foreach (var unit in _targetingUnits.removed)
            {
                if (unit.DestinationQueueComponent().destinations.Count != 0)
                {
                    unit.Get<Destination>() = unit.DestinationQueueComponent().destinations.Dequeue();
                    unit.Get<PathRequest>();
                }
            }

            foreach (var predator in _predatorArrived.added)
            {
                predator.Remove<Heading>();
                predator.Remove<Arrived>();
            }
        }
    }
}