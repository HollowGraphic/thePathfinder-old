using Pixeye.Actors;
using ThePathfinder.Components;

namespace ThePathfinder.Game
{
    public enum OrderType
    {
        None,
        Default,
        Movement
    }

    public interface IOrder
    {
        /// <summary>
        /// What type of order it is
        /// </summary>
        public OrderType OrderType { get; }
        /// <summary>
        /// Indicates whether order has been activated
        /// </summary>
        public bool IsActivated { get; }
        /// <summary>
        /// Indicates whether order has completed it's task
        /// </summary>
        public bool IsComplete { get; }
        /// <summary>
        /// Activate order
        /// </summary>
        public void Activate();
        /// <summary>
        /// Activate order
        /// </summary>
        public void Deactivate();
        /// <summary>
        /// Run Order
        /// </summary>
        /// <param name="delta"></param>
        public void Run(float delta);
    }

    /// <summary>
    /// Base for all orders
    /// </summary>
    public abstract class Order : IOrder
    {
        public OrderType OrderType { get; }
        private readonly ent _entity;
        private bool _isRunning;

        protected Order(ent entity, OrderType orderType)
        {
            _entity = entity;
            OrderType = orderType;
        }


        public bool IsActivated
        {
            get => _isRunning;

            private set
            {
                _isRunning = value;
                OnActivationStatusChanged(_entity, value);
            }
        }


        /// <summary>
        /// Perform order
        /// </summary>
        public void Activate()
        {
            IsActivated = true;
        }

        /// <summary>
        /// Deactivates order, often when queueing order in ahead of this one
        /// </summary>
        public void Deactivate()
        {
            IsActivated = false;
        }
        /// <summary>
        /// Runs <see cref="Order"/>
        /// </summary>
        /// <param name="delta"></param>
        public void Run(float delta)
        {
            OnRun(_entity, delta);
        }

        /// <summary>
        /// Gets called when the activated status of the order is changed
        /// </summary>
        /// <remarks>Useful for long running orders</remarks>
        /// <param name="entity">The <see cref="ent"/> this order is assigned to</param>
        /// <param name="isActivated">Activation status</param>
        protected abstract void OnActivationStatusChanged(ent entity, bool isActivated);

        /// <summary>
        /// Returns whether order has been completed
        /// </summary>
        public bool IsComplete
        {
            get
            {
                bool completed = OnCheckIfComplete(_entity);
                if(completed) OnCompleted(_entity);
                return completed;
            }
        }

        /// <summary>
        /// Gets called every frame
        /// <remarks>Returns whether the order completed this frame</remarks>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        protected virtual void OnRun(ent entity, float delta){}

        /// <summary>
        /// Performs complete check
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract bool OnCheckIfComplete(ent entity);

        /// <summary>
        /// Called when order has completed
        /// </summary>
        protected virtual void OnCompleted(ent entity)
        {
            
        }
        
    }

    public sealed class MoveOrder : Order
    {
        private readonly Destination _destination;

        public MoveOrder(ent entity, Destination destination) : base(entity,
            OrderType.Movement)
        {
            _destination = destination;
        }


        protected override void OnActivationStatusChanged(ent entity, bool isActivated)
        {
            if (!isActivated) return;
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


        protected  override void OnRun(ent entity, float delta)
        {

            float currentTIme = Time.Current;
            if (!(currentTIme >
                  entity.NavigatorComponent().lastRepath + entity.NavigatorComponent().repathRate)) return;

            entity.NavigatorComponent().lastRepath = currentTIme;
            entity.Get<PathRequest>();
        }

        protected override bool OnCheckIfComplete(ent entity)
        {
            switch (_destination.destinationType)
            {
                case DestinationType.AttackMove:
                case DestinationType.ForceMove:
                default:
                    return !entity.Has<Destination>();
                case DestinationType.Target:
                    return !entity.Has<Target>(); //lost/killed target
            }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    public class AbilityOrder : Order
    {
        private ent _ability;
        public AbilityOrder(ent entity,ent ability, OrderType orderType) : base(entity, orderType)
        {
            _ability = ability;
        }

        protected override void OnActivationStatusChanged(ent entity, bool isActivated)
        {
            _ability.Get<Cast>();
        }

        protected override bool OnCheckIfComplete(ent entity)
        {
            return true;
        }
    }
}