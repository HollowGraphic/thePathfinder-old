using Pixeye.Actors;
using Sirenix.OdinInspector;

namespace ThePathfinder.Components.Authoring
{
    public sealed class DamageAuthoring : AuthoringComponent
    {
        [MinMaxSlider(0, float.PositiveInfinity )]
        public float damage;

        public override void Set(ref ent entity)
        {
            //set entity
            entity.Set(new HealthModifier(-damage));
        }
    }
}