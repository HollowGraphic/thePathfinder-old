using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Units
{
    internal sealed class ProcessNavigatorTarget : Processor, ITick
    {
        [ExcludeBy(GameComponent.MoveToDestination)]
        private readonly Group<Navigator, Target, Combatant> _targetingUnits = default;

        public void Tick(float delta)
        {
        }

        public override void HandleEcsEvents()
        {
            foreach (var unit in _targetingUnits.added)
            {
                var target = unit.TargetComponent();
                if (unit.Has<AttackDestination>() && unit.Has<Destination>())
                    //pop current destination into queue so we can go back to it after we deal with target
                    unit.DestinationQueueComponent().destinations.Enqueue(unit.DestinationComponent());

                var unitPos = unit.transform.position;
                Debug.Log(Msg.BuildWatch("Unit Pos", unitPos.ToString()));
                var targetPos = target.Value.transform.position;
                Debug.Log(Msg.BuildWatch("Target Pos", targetPos.ToString()));
                Debug.Log(Msg.BuildWatch("Unit Alive", unit.exist.ToString()));
                
                var dir = unitPos - targetPos;
                var norm = dir.normalized;
                var offset = norm * 3.5f;
                //set destination to target position, and request a path

                unit.Get<Destination>() = new Destination(offset + targetPos);
                unit.Get<PathRequest>();
            }
        }
    }
}