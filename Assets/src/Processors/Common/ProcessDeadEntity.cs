using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessDeadEntity : Processor
    {
        private readonly Group<Dead> _group = default;

        public override void HandleEcsEvents()
        {
            foreach (ent entity in _group.added)      
            {
                Debug.Log("Releasing Entity : "+ entity);
                entity.Release();
            }
        }
    }
}