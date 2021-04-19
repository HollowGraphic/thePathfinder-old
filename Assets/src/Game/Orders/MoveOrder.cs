using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;
using Time = Pixeye.Actors.Time;

namespace ThePathfinder.Game
{
    public sealed class MoveOrder : Order
    {
        private readonly Destination _destination;

        public MoveOrder(ent entity, Destination destination) : base(entity,
            OrderType.Movement)
        {
            _destination = destination;
        }

        protected override void OnOrderStart(ent entity)
        {
            switch (_destination.destinationType)
            {
                case DestinationType.ForceMove:
                    entity.Get<Passive>();
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

        protected override void OnOrderExecute(ent entity, float delta)
        {
            float currentTIme = Time.Current;
            if (!(currentTIme >
                  entity.NavigatorComponent().lastRepath + entity.NavigatorComponent().repathRate)) return;

            entity.NavigatorComponent().lastRepath = currentTIme;
            entity.Get<PathRequest>();
        }

        protected override void OnOrderHalt(ent entity)
        {
            Debug.Log("Current Order was Deactivated");
            entity.Remove<Destination>();
        }

        protected override void OnOrderCompleted(ent entity)
        {
            if(entity.Has<Passive>())
                entity.Remove<Passive>();
        }

        protected override bool OnCheckIfOrderCompleted(ent entity)
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
}