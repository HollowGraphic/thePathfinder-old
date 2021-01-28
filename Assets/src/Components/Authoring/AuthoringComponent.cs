using Drawing;
using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    [AddComponentMenu("ThePathfinder")]
    public abstract class AuthoringComponent : MonoBehaviour, IComponent, IDrawGizmos
    {
        public abstract void Set(ref ent entity);

        public virtual void RegisterObservers(ent entity)
        {
        }

//#if UNITY_EDITOR
#pragma warning disable
        public AuthoringComponent()
        {
            DrawingManager.Register(this);
        }

        public virtual void DrawGizmos()
        {
        }

//#endif
    }
}