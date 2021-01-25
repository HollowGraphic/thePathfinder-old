using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Navigation
{
    internal sealed class ProcessDestinations : Processor, ITick
    {
        private readonly Group<Combatant> _group = default;


        public void Tick(float dt)
        {
            foreach (var entity in _group)
            {
                Debug.Log(Msg.BuildWatch("Move To Destination", entity.Has<MoveToDestination>().ToString()));
                Debug.Log(Msg.BuildWatch("Attack Destination", entity.Has<AttackDestination>().ToString()));
            }
        }
    }
}