using Pixeye.Actors;
using ThePathfinder.Game;

namespace ThePathfinder.Signals
{
    namespace Loading
    {
        public class OverWorldLoaded{}
    }
    namespace Units
    {
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
        public class AssignOrder
        {
            public readonly Order order;
            public readonly bool queueOrder;
            public readonly ent entity;

            public AssignOrder(ent entity, Order order, bool queueOrder = false)
            {
                this.entity = entity;
                this.order = order;
                this.queueOrder = queueOrder;
            }
        }
        public class ClearOrderQueue
        {
            public readonly ent entity;

            public ClearOrderQueue(ent entity)
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