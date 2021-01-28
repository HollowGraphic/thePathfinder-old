using Pixeye.Actors;
using ThePathfinder.Components;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessDeadEntity : Processor, ITick
    {
        private readonly Group<Dead> _group = default;
        public override void HandleEcsEvents()
        {
            foreach (var entity in _group.added)      
            {
                entity.Release();
            }
        }

        public void Tick(float delta)
        {
        }
    }
}