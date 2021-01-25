using Pixeye.Actors;

namespace ThePathfinder.Components.Authoring
{
    public interface IComponent
    {
        public void Set(ref ent entity);
        public void RegisterObservers(ent entity);
    }
}