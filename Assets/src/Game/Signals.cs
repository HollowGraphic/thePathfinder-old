using Pixeye.Actors;

namespace ThePathfinder.Game
{
    namespace Loading
    {
        public class OverWorldLoaded{}
    }
    namespace Orders
    {
        public enum QueueProcedure
        {
            None,QueueAhead, QueueBehind
        }
        // public class ResumeOrderQueue
        // {
        //     public readonly ent entity;
        //
        //     public ResumeOrderQueue(ent entity)
        //     {
        //         this.entity = entity;
        //     }
        // }
        // public class PauseOrderQueue
        // {
        //     public readonly ent entity;
        //     public PauseOrderQueue(ent entity)
        //     {
        //         this.entity = entity;
        //     }
        // }
        public readonly struct SignalAssignOrder
        {
            public readonly Order order;
            public readonly ent entity;
            public readonly QueueProcedure queueProcedure;
            public SignalAssignOrder(ent entity, Order order, QueueProcedure queueProcedure = QueueProcedure.None)
            {
                this.entity = entity;
                this.order = order;
                this.queueProcedure = queueProcedure;
            }
        }
        public readonly struct SignalClearOrderQueue
        {
            public readonly ent entity;

            public SignalClearOrderQueue(ent entity)
            {
                this.entity = entity;
            }
        }
        /// <summary>
        /// Used to send entity to order queue
        /// </summary>
        public class AssignEntityToOrderQueue
        {
            public ent entity;
        }
    }
}