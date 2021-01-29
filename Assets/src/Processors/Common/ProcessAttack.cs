using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessAttack : Processor, ITick
    {
        //TODO know what is the 'active' attack

        private readonly Group<Combatant, Target> _group = default;
        public void Tick(float delta)
        {
            //combatants have a basic attack
            foreach (var attacker in _group)
            {
                //if(combatant.Has<ActiveWeapon>())
                //else
                if(math.distance(attacker.transform.position, attacker.TargetComponent().Value.transform.position) < attacker.CombatantComponent().attackRange)
                    attacker.TargetComponent().Value.Get<HealthModifier>().value = -10;//INVESTIGATE is there a default attack damage value?
                //TODO store statmods on ents structure, then loop through them to apply mods
            }
        }
    }
}