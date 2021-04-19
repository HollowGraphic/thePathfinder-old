using System.Collections.Generic;
using System.Numerics;
using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Game.Abilities;
using Vector3 = UnityEngine.Vector3;

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
        public bool IsRunning { get; }

        /// <summary>
        /// Indicates whether order has completed it's task
        /// </summary>
        public bool IsComplete { get; }

        /// <summary>
        /// Activate order
        /// </summary>
        public void Start();

        /// <summary>
        /// Deactivate order
        /// </summary>
        public void Halt();

        //public void Interrupt();

        /// <summary>
        /// Run Order
        /// </summary>
        /// <param name="delta"></param>
        public void Execute(float delta);
    }

    /// <summary>
    /// Base for all orders
    /// </summary>
    public abstract class Order : IOrder
    {
        public OrderType OrderType { get; }

        private readonly ent _entity;

        // private bool _isRunning;
        // private bool _isComplete;
        /// <summary>
        /// Returns whether order has been completed
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (OnCheckIfOrderCompleted(_entity))
                {
                    OnOrderCompleted(_entity);
                    return true;
                }
                return false;
            }
        }

        public bool IsRunning { get; private set; }

        protected Order(ent entity, OrderType orderType)
        {
            _entity = entity;
            OrderType = orderType;
        }

        /// <summary>
        /// Perform order
        /// </summary>
        public void Start()
        {
            OnOrderStart(_entity);
            IsRunning = true;
        }

        /// <summary>
        /// Deactivates order, often when queueing order in ahead of this one
        /// </summary>
        public void Halt()
        {
            OnOrderHalt(_entity);
            IsRunning = false;
        }

        // /// <summary>
        // /// Interrupts the order
        // /// </summary>
        // public void Interrupt()
        // {
        //     OnInterrupt(_entity);
        //
        // }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">The <see cref="ent"/> this <see cref="Order"/> is attached to</param>
        protected virtual void OnInterrupt(ent entity){}

        /// <summary>
        /// Runs <see cref="Order"/>
        /// </summary>
        /// <param name="delta"></param>
        public void Execute(float delta)
        {
            OnOrderExecute(_entity, delta);
        }

        /// <summary>
        /// Gets called when the order is activated
        /// </summary>
        /// <remarks> Useful for long running orders. <see cref="IsRunning"/> has not been updated at this stage,
        /// use it to check it's last frame status.</remarks>
        /// <param name="entity">The <see cref="ent"/> this order is assigned to</param>
        protected abstract void OnOrderStart(ent entity);
        /// <summary>
        /// Gets called when the order should stop.
        /// </summary>
        /// <remarks> Useful for long running orders. <see cref="IsRunning"/> has not been updated at this stage,
        /// use it to check it's last frame status.</remarks>
        /// <param name="entity">The <see cref="ent"/> this order is assigned to</param>
        protected virtual void OnOrderHalt(ent entity){}


        /// <summary>
        /// Gets called every frame
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        protected virtual void OnOrderExecute(ent entity, float delta)
        {
        }

        /// <summary>
        /// Performs complete check
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract bool OnCheckIfOrderCompleted(ent entity);

        /// <summary>
        /// Called when order has fully completed
        /// </summary>
        protected virtual void OnOrderCompleted(ent entity)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AbilityOrder : Order
    {
        private readonly IAbility _ability;

        public AbilityOrder(ent entity, IAbility ability, OrderType orderType) : base(entity, orderType)
        {
            this._ability = ability;
        }

        protected override void OnOrderStart(ent entity)
        {
            _ability.Cast();
        }

        protected override bool OnCheckIfOrderCompleted(ent entity)
        {
            return _ability.IsComplete;
        }
    }
}