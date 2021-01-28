using Pixeye.Actors;
using ThePathfinder.Components;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessDeadEntity : Processor, ITickLate
    {
        private readonly Group<Dead> _group = default;

        public void TickLate(float delta)
        {
            foreach (var entity in _group)      
            {
                entity.Release();
            }
        }
    }
}