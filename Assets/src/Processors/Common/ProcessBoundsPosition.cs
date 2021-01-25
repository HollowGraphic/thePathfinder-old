using ThePathfinder.Components;
using Pixeye.Actors;

namespace ThePathfinder.Processors
{
    public sealed class ProcessBoundsPosition : Processor, ITick
    {
        private readonly Group<PointerBounds> _bounds = default;

        public void Tick(float delta)
        {
            foreach (var bound in _bounds)
            {
                bound.Get<PointerBounds>().value.center = bound.transform.position;
            }
        }
    }
}