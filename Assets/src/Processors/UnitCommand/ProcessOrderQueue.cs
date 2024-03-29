using System;
using System.Collections.Generic;
using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Game;
using ThePathfinder.Game.Orders;
using UnityEngine;

namespace ThePathfinder.Processors.UnitCommand
{
    public sealed class ProcessOrderQueue : Processor<SignalAssignOrder, SignalClearOrderQueue>, ITick
    {
        private readonly Dictionary<ent, LinkedList<Order>> _entityOrderQueues =
            new Dictionary<ent, LinkedList<Order>>(10);

        private readonly Group<Commandable> _commandableGroup = default;
        private const int QUEUE_CAPACITY = 20;

        public ProcessOrderQueue()
        {
            //Layer.AddSignal(this);

            // orders = Layer.Get<Dictionary<ent, OrderQueue>>();
            // foreach (var orderQueues in orders)
            // {
            //     Observer.Add(orderQueues.Value, queue => queue.Count, i =>
            //     {
            //         //what do we do when the queue count changes?
            //         //how do we know when we want to go to another order?
            //         //how do we know if the last order has been canceled/superseded?
            //         //how do we know if it's been performed?
            //         
            //     } );
            // }
        }

        public override void HandleEcsEvents()
        {
            //when a new commandable entity is added to the game, they get their own entry
            foreach (ent commandableEntity in _commandableGroup.added)
            {
                Debug.Log("Added Commandable to Dictionary");
                _entityOrderQueues.Add(commandableEntity, new LinkedList<Order>());
            }

            //they get removed when they are destroyed
            foreach (ent commandableEntity in _commandableGroup.removed)
            {
                _entityOrderQueues.Remove(commandableEntity);
            }
        }

        public void Tick(float delta)
        {
            foreach (KeyValuePair<ent, LinkedList<Order>> pair in _entityOrderQueues)
            {
                LinkedList<Order> orders = pair.Value;
                ent entity = pair.Key;
                if (orders.Count == 0) continue;
                Order currentOrder = orders.First.Value;
                Draw.Label2D(entity.transform.position, string.Concat("Order Count", orders.Count.ToString()));
                if (!currentOrder.IsRunning)
                {
                    currentOrder.Activate();
                    //if there is a next order, cache order type, otherwise set to none
                    entity.UnitOrderQueueInfoComponent().nextOrderType =
                        orders.First.Next?.Value.orderType ?? OrderType.None; 
                }

                if (!currentOrder.IsComplete(delta)) continue;
                orders.RemoveFirst(); //remove old one
            }
        }

        public override void ReceiveEcs(ref SignalAssignOrder signal, ref bool stopSignal)
        {
            Debug.Log("Received Order to Assign");
            Order order = signal.order;
            if (_entityOrderQueues.TryGetValue(signal.entity, out LinkedList<Order> orders))
            {
                LinkedListNode<Order> first = orders.First;
                if (orders.Count == 0)
                {
                    orders.AddFirst(order);
                    return;
                }
                switch (signal.queueProcedure)
                {
                    case QueueProcedure.None:
                        orders.Clear();
                        orders.AddFirst(order);
                        break;
                    case QueueProcedure.QueueBehind:
                        orders.AddAfter(orders.Last, order);
                        break;
                    case QueueProcedure.QueueAhead:
                        first.Value.Deactivate();
                        orders.AddBefore(first, order);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Debug.LogError("Entity " + signal.entity + " not found in order queue");
            }
        }

        public override void ReceiveEcs(ref SignalClearOrderQueue signal, ref bool stopSignal)
        {
            _entityOrderQueues[signal.entity].Clear();
        }
    }
}