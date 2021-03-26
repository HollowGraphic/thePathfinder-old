using Pixeye.Actors;
using ThePathfinder.Components;

namespace ThePathfinder.Game
{
    public enum OrderType
    {
        Default, Movement
    }
    /// <summary>
    /// Can be Sent as a signal?
    /// </summary>
    public abstract class Order
    {
        public OrderType orderType;
        public bool Running { get;protected set; }

 

        /// <summary>
        /// Perform order
        /// </summary>
        public abstract void Activate(ref ent entity);

        public abstract bool IsComplete(ref ent entity, float delta);
    }
    public sealed class MoveOrder : Order
    {
        private readonly Destination _destination;

        public MoveOrder(Destination destination) 
        {
            _destination = destination;
            orderType = OrderType.Movement;
        }

        /// <summary>
        /// Happens one time
        /// </summary>
        /// <param name="entity"></param>
        public override void Activate(ref ent entity)
        {
            Running = true;
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

        public override bool IsComplete(ref ent entity, float delta)
        {
            bool completed;
            switch (_destination.destinationType)
            {
                case DestinationType.AttackMove:
                case DestinationType.ForceMove:
                default:
                    completed = entity.Has<Arrived>();
                    break;
                case DestinationType.Target:
                    completed = !entity.Has<Target>(); //lost/killed target
                    break;
            }

            if (completed)
            {
                entity.Remove<Destination>();
                entity.Remove<Heading>();
                entity.Remove<Arrived>();
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