using System.Collections.Generic;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Signals.Units;
using ThePathfinder.Game;
using UnityEngine;

namespace ThePathfinder.Game
{
    public sealed class ProcessOrderQueue : Processor, ITick, IReceive<AssignOrder>, IReceive<ClearOrderQueue>
    // , IReceive<PauseOrderQueue>, IReceive<ResumeOrderQueue>
    {
        private readonly Dictionary<ent, Queue<Order>> _entityOrderQueues = new Dictionary<ent, Queue<Order>>(10);
        private readonly Group<Commandable> _commandableGroup = default;
        private bool _processOrderQueue = true;
        private const int QUEUE_CAPACITY = 20;
        public ProcessOrderQueue()
        {
            Layer.AddSignal(this);
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
                _entityOrderQueues.Add(commandableEntity, new Queue<Order>(QUEUE_CAPACITY));
            }
            //they get removed when they are destroyed
            foreach (ent commandableEntity in _commandableGroup.removed)
            {
                _entityOrderQueues.Remove(commandableEntity);
            }
        }

        public void Tick(float delta)
        {
            if(!_processOrderQueue) return;
            foreach (KeyValuePair<ent, Queue<Order>> pair in _entityOrderQueues)
            {
                
                Queue<Order> orders = pair.Value;
                ent entity = pair.Key;
                if (orders.Count == 0) continue;
                Order currentOrder = orders.Peek();
                if (!currentOrder.Running)
                {
                    currentOrder.Activate(ref entity);
                    continue;//skip to next q
                }
                if (currentOrder.IsComplete(ref entity, delta))
                {
                    orders.Dequeue();//remove old one
                    
                    //if(orders.Count == 0) continue;
                    //entity.UnitOrderQueueInfoComponent().nextOrderType = orders.Peek().orderType;//Peek next order
                    //orders.Enqueue(currentOrder);//requeue current order
                }
            }
        }
        
        public void HandleSignal(in AssignOrder arg)
        {
            Order order = arg.order;
            Queue<Order> orderQueue = _entityOrderQueues[arg.entity];
            if (!arg.queueOrder && orderQueue.Count != 0)
            {
                orderQueue.Clear();
            }
            orderQueue.Enqueue(order);
        }

        public void HandleSignal(in ClearOrderQueue arg)
        {
            _entityOrderQueues[arg.entity].Clear();
        }

        // public void HandleSignal(in PauseOrderQueue arg)
        // {
        //     _processOrderQueue = false;
        // }
        //
        // public void HandleSignal(in ResumeOrderQueue arg)
        // {
        //     _processOrderQueue = true;
        // }
    }


}