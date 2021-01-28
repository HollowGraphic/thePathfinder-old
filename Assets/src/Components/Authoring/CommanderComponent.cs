using Pixeye.Actors;

namespace ThePathfinder.Components.Authoring
{
    public sealed class CommanderComponent : AuthoringComponent
    {
        public override void Set(ref ent entity)
        {
            entity.Set<Commander>();
        }
    }
}