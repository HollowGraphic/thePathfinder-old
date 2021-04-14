using System;
using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using ThePathfinder.Game;
using ThePathfinder.Game.Orders;
using UnityEngine;

namespace ThePathfinder.Processors.Input
{
    [Serializable]
    internal sealed class ProcessMovementOrders : ProcessorInput, ITick
    {
        protected override int CategoryId => Category.Orders;
        private readonly Group<Agent, Navigator, Selected> _movableUnits = default;
        private readonly Group<Agent, Navigator, Selected, Heading> _movingUnits = default;
        private DestinationType _destinationType; 

        public void Tick(float delta)
        {
            bool queue = Player.GetButton(ActionId.Order_Queue);

            if (Player.GetButtonDown(ActionId.Order_Move) || (Player.GetButtonDown(ActionId.Ability_Cast) && _destinationType == DestinationType.AttackMove))
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
                foreach (ent unit in _movingUnits)
                {
                    Ecs.Send(new SignalClearOrderQueue(unit));
                    unit.Remove<Destination>();
                }
            }
        }

        private void AssignDestination(bool shouldQueue, DestinationType destinationType)
        {
            Debug.Log("Sending Move Order");
            QueueProcedure queueProcedure = shouldQueue ? QueueProcedure.QueueBehind : QueueProcedure.None;
            //find if we happen to click on a damageable entity
            var destination = new Destination(Mouse.GetWorldPosition(), destinationType);
            foreach (ent unit in _movableUnits)
            {
                var moveOrder = new MoveOrder(unit, destination);
                Ecs.Send(new SignalAssignOrder(unit, moveOrder,queueProcedure));
            }
        }
    }
}