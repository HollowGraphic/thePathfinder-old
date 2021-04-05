using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;

namespace ThePathfinder.Processors.StatModding
{
    public sealed class ProcessHealthModifier : Processor, ITick
    {
        private readonly Group<Health, HealthModifier> _group = default;
        public void Tick(float delta)
        {
            foreach (var entity in _group)
            {
                float currentHealth = entity.HealthComponent().value;
                //don't go below zero
                float mod = math.max(currentHealth + entity.HealthModifierComponent().value, entity.MaxHealthComponent().value);
                //don't go above max health
                float health = math.min(mod, 0);
                if(health == 0) entity.Get<Dead>();//kills entity
                else
                {
                    
                    entity.HealthComponent().value = health;
                }

                //always remove the health mod
                //entity.Remove<HealthModifier>();
                //TODO store statmods on ents structure, then loop through them to apply mods
            }
        }
    }
}