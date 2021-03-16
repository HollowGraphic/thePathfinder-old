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
        private readonly Group<Agent, Navigator, Selected> _movableUnits = default;
        private readonly Group<Agent, Navigator, Selected, Heading> _movingUnits = default;
        private DestinationType _destinationType;

        public void Tick(float delta)
        {
            bool queue = Player.GetButton(ActionId.Order_Waypoint);

            if (Player.GetButtonDown(ActionId.Order_Move) || Player.GetButtonDown(ActionId.Ability_Cast))
                AssignDestination(queue, _destinationType);
            //TODO refactor this into a switch statement
            //clean up attack move state
            if (Player.GetButtonUp(ActionId.Ability_Cast) && _destinationType == DestinationType.AttackMove && !queue)
            {
                EnableConflictingInputs();
                _destinationType = DestinationType.ForceMove;
            }

            if (Player.GetButtonDown(ActionId.Ability_Attack))
            {
                _destinationType = DestinationType.AttackMove;
                DisableConflictingInputs();
            }

            if (Player.GetButtonDown(ActionId.Ability_Abort))
            {
                _destinationType = DestinationType.ForceMove;
                EnableConflictingInputs();
            }

            if (Player.GetButtonDown(ActionId.Order_Stop))
            {
                _destinationType = DestinationType.ForceMove;
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
            var destination = new Destination(Mouse.GetWorldPosition(), destinationType);
            foreach (var unit in _movableUnits)
            {
                var queue = unit.DestinationQueueComponent();
                {
                    //Handle move types
                    switch (destinationType)
                    {
                        case DestinationType.ForceMove:
                            unit.Get<Passive>();
                            break;
                        case DestinationType.AttackMove:
                            if (unit.Has<Passive>())
                            {
                                unit.Remove<Passive>();
                                Debug.Log("Removing Passive");
                            }

                            //queue destination if unit already has a target, otherwise we will override their current destination (which happens to be the target position)
                            if (unit.Has<Target>())
                            {
                                if (!shouldQueue)
                                {
                                    queue.destinations.Clear();
                                }

                                queue.destinations.Enqueue(destination);
                                continue; //bump out
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(destinationType), destinationType, null);
                    }
                }

                //only queue if unit already has destination
                if (shouldQueue && unit.Has<Destination>())
                {
                    Debug.Log("Queue Destination");
                    queue.destinations.Enqueue(destination);
                    continue; // bump out to next unit
                }

                //if we are here, then we are not queueing

                queue.destinations.Clear();

                if (unit.Has<Arrived>()) unit.Remove<Arrived>(); //TODO In don't like this here
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