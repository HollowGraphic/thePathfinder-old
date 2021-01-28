using Pixeye.Actors;
using UnityEngine;
using UnityEngine.Serialization;

namespace ThePathfinder.Components.Authoring
{
    public class MaxSpeedComponent : AuthoringComponent
    {
        [FormerlySerializedAs("_maxSpeed")] [SerializeField] private MaxSpeed maxSpeed;

        public override void Set(ref ent entity)
        {
            entity.Set(maxSpeed);
        }
    }
}