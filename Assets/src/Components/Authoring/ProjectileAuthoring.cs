using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public sealed class ProjectileAuthoring : AuthoringComponent
    {
        public MaxSpeed maxSpeed;
        public override void Set(ref ent entity)
        {
            //set entity
            
            entity.Set(new Heading(transform.forward));
            entity.Set(new Translator(){canMove = true});
            entity.Set(maxSpeed);
        }
    }
}