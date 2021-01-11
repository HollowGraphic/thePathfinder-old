using System;
using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Components.Authoring;
using ThePathfinder.Input;
using UnityEngine;

namespace ThePathfinder.Processors
{
    [Serializable]
    sealed class ProcessMovementOrders : ProcessorInput, ITick
    {
        private readonly Group<Unit, Navigator, Selected> _moveableUnits = default;
        private readonly Group<Unit, Navigator, Selected, Heading> _movingUnits = default;
        private bool attackMove = false;

        public void Tick(float delta)
        {
            var queue = player.GetButton(ActionId.Order_Waypoint);
            if (player.GetButtonDown(ActionId.Order_Move) && !attackMove)
            {
                AssignDestination(queue, true);
            }

            if (player.GetButtonDown(ActionId.Ability_Cast) && attackMove)
            {
                AssignDestination(queue, false);
            }

            //clean up attack move state
            if (player.GetButtonUp(ActionId.Ability_Cast) && attackMove && !queue)
            {
                EnableConflictingInputs();
                attackMove = false;
            }

            if (player.GetButtonDown(ActionId.Ability_Attack))
            {
                attackMove = true;
                DisableConflictingInputs();
            }

            if (player.GetButtonDown(ActionId.Ability_Abort))
            {
                attackMove = false;
                EnableConflictingInputs();
            }

            if (player.GetButtonDown(ActionId.Order_Stop))
            {
                Debug.Log("Order Stop");
                foreach (var unit in _movingUnits)
                {
                    unit.Remove<Destination>();
                    unit.DestinationQueueComponent().destinations.Clear();
                }
            }
        }

        private void AssignDestination(bool shouldQueue, bool forceMove)
        {
            Destination destination = new Destination(mouse.GetWorldPosition());
            foreach (var unit in _moveableUnits)
            {
                var queue = unit.DestinationQueueComponent();
                if (forceMove)
                {
                    if (unit.Has<AttackDestination>())
                    {
                        //We are changin destination types and there are queued destinations, clear them
                        if(queue.destinations.Count > 0) queue.destinations.Clear();
                        unit.Remove<AttackDestination>();
                    }
                    unit.Get<MoveToDestination>();
                }
                else
                {
                    if (unit.Has<MoveToDestination>() && queue.destinations.Count > 0)
                    {
                        //if we are changing destination types and there are queued destinations, clear them
                        if(queue.destinations.Count != 0) queue.destinations.Clear();
                        unit.Remove<MoveToDestination>();
                    }
                    unit.Get<AttackDestination>();
                }

                //only queue if unit already has destination
                if (shouldQueue && unit.Has<Destination>())
                {
                    this.Log("Queue Destination");
                    queue.destinations.Enqueue(new Destination(destination.value));
                    continue; // bump out to next unit
                }

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