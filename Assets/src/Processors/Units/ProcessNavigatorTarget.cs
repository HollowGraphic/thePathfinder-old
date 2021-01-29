using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
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
                var targetPos = target.Value.transform.position;
                var dir = unit.transform.position - targetPos;
                if (dir.magnitude < unit.CombatantComponent().attackRange) continue;//we are within attack range
            
                if (unit.Has<Destination>() && 
                    unit.DestinationComponent().destinationType != DestinationType.Target) //handle previous destination type
                {
                    unit.DestinationQueueComponent().destinations.Enqueue(unit.DestinationComponent());
                }
                //not within attack range, calculate new destination
                var norm = dir.normalized;
                var offset = norm * 3.5f;//INVESTIGATE why the hardcoded value?
                //set destination to target position, and request a path
                unit.Get<Destination>() = new Destination(offset + targetPos, DestinationType.Target);
                unit.Get<PathRequest>();
            }
        }
    }
}