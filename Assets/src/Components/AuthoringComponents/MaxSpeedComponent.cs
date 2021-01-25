using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public class MaxSpeedComponent : AuthoringComponent
    {
        [SerializeField] private MaxSpeed _maxSpeed;

        public override void Set(ref ent entity)
        {
            entity.Set(_maxSpeed);
        }
    }
}