using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public sealed class CameraComponent : AuthoringComponent
    {
        [SerializeField] private GameCamera settings;

        public override void Set(ref ent entity)
        {
            entity.Set(settings);
        }
    }
}