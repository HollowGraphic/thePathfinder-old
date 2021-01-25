using Pixeye.Actors;
using ThePathfinder.Components.Authoring;

#if UNITY_EDITOR
using Drawing;
using Sirenix.OdinInspector;
#endif

namespace ThePathfinder
{
    public class GameActor : Actor
#if UNITY_EDITOR
        , IDrawGizmos
#endif
    {
        protected override void Setup()
        {
            foreach (var component in GetComponents<IComponent>())
            {
                component.Set(ref entity);
                component.RegisterObservers(entity);
            }
        }

#if UNITY_EDITOR
#pragma warning disable
        public GameActor()
        {
            DrawingManager.Register(this);
        }

        private void OnDrawGizmos()
        {
        }
        public virtual void DrawGizmos()
        {
        }

#endif
    }
}