using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Components.Authoring;
using UnityEngine;
using Component = ThePathfinder.Components.Component;


namespace ThePathfinder.Processors
{
    sealed class ProcessNavigatorTarget : Processor, ITick
    {
        [ExcludeBy(Component.MoveToDestination)]
        private readonly Group<Navigator, Target, Combatant> _targetingUnits = default;

        public override void HandleEcsEvents()
        {
            foreach (var unit in _targetingUnits.added)
            {
                var target = unit.TargetComponent();
                if ( unit.Has<AttackDestination>() && unit.Has<Destination>())
                {
                    //pop current destination into queue so we can go back to it after we deal with target
                    unit.DestinationQueueComponent().destinations.Enqueue(unit.DestinationComponent());
                }
Debug.Log(Msg.BuildWatch("Unit Pos", unit.transform.position.ToString()));
                Debug.Log(Msg.BuildWatch("Target Pos", target.value.transform.position.ToString()));
Debug.Log(Msg.BuildWatch("Unit Alive", unit.exist.ToString()));
                var dir = ( unit.transform.position - target.value.transform.position );
                var norm = dir.normalized;
                var offset = norm * 3.5f;
                //set destination to target position, and request a path
                
                unit.Get<Destination>() = new Destination(offset + target.value.transform.position  );
                unit.Get<PathRequest>();
            }
        }

        public void Tick(float delta)
        {

        }
    }
}