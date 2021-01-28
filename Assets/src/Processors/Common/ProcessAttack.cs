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
            foreach (var predator in _group)
            {
                //if(combatant.Has<ActiveWeapon>())
                //else
                if(math.distance(predator.transform.position, predator.TargetComponent().Value.transform.position) < predator.CombatantComponent().attackRange)
                    predator.TargetComponent().Value.Get<HealthModifier>().value += -10;//INVESTIGATE is there a default attack damage value?
            }
        }
    }
}