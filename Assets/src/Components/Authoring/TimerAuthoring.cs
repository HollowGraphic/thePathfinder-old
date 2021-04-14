using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public sealed class TimerAuthoring : AuthoringComponent
    {
        public Timer timer;
        public override void Set(ref ent entity)
        {
            //set entity
            entity.Set(timer);
        }
    }
}