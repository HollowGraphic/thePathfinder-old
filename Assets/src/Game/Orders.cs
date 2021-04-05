using Pixeye.Actors;
using ThePathfinder.Components;

namespace ThePathfinder.Game
{
    public enum OrderType
    {
        None,Default, Movement
    }
    /// <summary>
    /// Can be Sent as a signal?
    /// </summary>
    public abstract class Order
    {
        public OrderType orderType;
        private readonly ent _entity;

        protected Order(ent entity)
        {
            this._entity = entity;
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;

            private set
            {
                _isRunning = value;
                OnOrderStatusChanged(_entity);
            }
        }


        /// <summary>
        /// Perform order
        /// </summary>
        public void Activate()
        {
            IsRunning = true;
        }
        /// <summary>
        /// Deactivates order, often when queueing order in ahead of this one
        /// </summary>
        public void Deactivate()
        {
            IsRunning = false;
        }
        /// <summary>
        /// Gets called when the status of the order is changed
        /// </summary>
        /// <param name="entity">The <see cref="ent"/> this order is assigned to</param>
        protected abstract void OnOrderStatusChanged(ent entity);

        /// <summary>
        /// Returns whether order has been completed
        /// </summary>
        /// <param name="delta">Delta frame time</param>
        /// <returns></returns>
        public bool IsComplete(float delta)
        {
            return RunOrder(_entity, delta);
        }
        /// <summary>
        /// Gets called every frame
        /// <remarks>Returns whether the order completed this frame</remarks>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        protected abstract bool RunOrder(ent entity, float delta);

    }
    public sealed class MoveOrder : Order
    {
        private readonly Destination _destination;

        public MoveOrder(ent entity, Destination destination):base(entity) 
        {
            _destination = destination;
            orderType = OrderType.Movement;
        }


        /// <summary>
        /// Gets called when the status of the order is changed
        /// </summary>
        /// <param name="entity">The <see cref="ent"/> this order is assigned to</param>
        protected override void OnOrderStatusChanged(ent entity)
        {
            if (!IsRunning) return;
            switch (_destination.destinationType)
            {
                case DestinationType.ForceMove:
                    if (!entity.Has<Passive>()) entity.Get<Passive>();
                    break;
                case DestinationType.AttackMove:
                    if (entity.Has<Passive>()) entity.Remove<Passive>();
                    break;
                case DestinationType.Target:
                default:
                    break;
            }
            
            entity.Get<Destination>() = _destination;
            entity.Get<PathRequest>();
        }
        
        protected override bool RunOrder(ent entity,float delta)
        {
            bool completed;
            switch (_destination.destinationType)
            {
                case DestinationType.AttackMove:
                case DestinationType.ForceMove:
                default:
                    completed = !entity.Has<Destination>();
                    break;
                case DestinationType.Target:
                    completed = !entity.Has<Target>(); //lost/killed target
                    break;
            }

            if (completed)
            {
                entity.Remove<Heading>();
                if(entity.Has<Passive>()) entity.Remove<Passive>();//HACK this does not belong here, we can't assume that we can just remove the passive flag
                return true;
            }

            float currentTIme = Time.Current;
            if (!(currentTIme >
                  entity.NavigatorComponent().lastRepath + entity.NavigatorComponent().repathRate)) return false;
            
            entity.NavigatorComponent().lastRepath = currentTIme;
            entity.Get<PathRequest>();

            return false;
        }


    }

    // public class AbilityOrder : IOrder
    // {
    //     public Vector3 Location;
    //     public int AbilityId;
    //     public void Perform(ref ent entity)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}