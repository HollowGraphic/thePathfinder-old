using Pixeye.Actors;
using ThePathfinder.Components;


namespace ThePathfinder.Processors.AI
{
    sealed class ProcessMoveToDestination : Processor
    {
        private readonly Group<MoveToDestination> _group = default;
        public override void HandleEcsEvents()
        {
            foreach (var entity in _group.added)
            {
                entity.Get<PathRequest>();
            }
        }
    }
}