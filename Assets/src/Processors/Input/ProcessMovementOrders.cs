using System;
using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using UnityEngine;

namespace ThePathfinder.Processors.Input
{
    [Serializable]
    internal sealed class ProcessMovementOrders : ProcessorInput, ITick
    {
        private readonly Group<Unit, Navigator, Selected> _movableUnits = default;
        private readonly Group<Unit, Navigator, Selected, Heading> _movingUnits = default;
        private DestinationType _destinationType;
        public void Tick(float delta)
        {
            bool queue = Player.GetButton(ActionId.Order_Waypoint);
            
            if (Player.GetButtonDown(ActionId.Order_Move) || Player.GetButtonDown(ActionId.Ability_Cast)) AssignDestination(queue, _destinationType);

            //clean up attack move state
            if (Player.GetButtonUp(ActionId.Ability_Cast) && _destinationType == DestinationType.AttackMove && !queue)
            {
                EnableConflictingInputs();
                _destinationType = DestinationType.Default;
            }

            if (Player.GetButtonDown(ActionId.Ability_Attack))
            {
                _destinationType = DestinationType.AttackMove;
                DisableConflictingInputs();
            }

            if (Player.GetButtonDown(ActionId.Ability_Abort))
            {
                _destinationType = DestinationType.Default;
                EnableConflictingInputs();
            }

            if (Player.GetButtonDown(ActionId.Order_Stop))
            {
                _destinationType = DestinationType.Default;
                Debug.Log("Order Stop");
                foreach (var unit in _movingUnits)
                {
                    unit.DestinationQueueComponent().destinations.Clear();
                    unit.Remove<Destination>();
                }
            }
        }

        private void AssignDestination(bool shouldQueue, DestinationType destinationType)
        {
            foreach (var unit in _movableUnits)
            {
                var queue = unit.DestinationQueueComponent();
                {//Handle move types
                    switch (destinationType)
                    {
                        case DestinationType.Default:
                            if (unit.Has<AttackDestination>())
                            {
                                //We are changing destination types and there are queued destinations, clear them
                                if (queue.destinations.Count > 0) queue.destinations.Clear();
                                unit.Remove<AttackDestination>();
                            }
                            unit.Get<MoveToDestination>();
                            break;
                        case DestinationType.AttackMove:
                            if (unit.Has<MoveToDestination>())
                            {
                                //if we are changing destination types and there are queued destinations, clear them
                                if (queue.destinations.Count != 0) queue.destinations.Clear();
                                unit.Remove<MoveToDestination>();
                                Debug.Log("Removing ForceMove");
                            }
                            Debug.Log("Adding AttackMove");
                            unit.Get<AttackDestination>();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(destinationType), destinationType, null);
                    }
                }
                var destination = new Destination(Mouse.GetWorldPosition(), destinationType);
                //only queue if unit already has destination
                if (shouldQueue && unit.Has<Destination>())
                {
                    this.Log("Queue Destination");
                    queue.destinations.Enqueue(destination);
                    continue; // bump out to next unit
                }
                if(unit.Has<Arrived>()) unit.Remove<Arrived>();//TODO In don't like this here
                //add a destination and request a path
                unit.Get<Destination>() = destination;
                unit.Get<PathRequest>();
            }
        }

        protected override int SetCategoryId()
        {
            return Category.Orders;
        }
    }
}