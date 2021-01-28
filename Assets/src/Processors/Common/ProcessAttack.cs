using Pixeye.Actors;
using ThePathfinder.Components;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessAttack : Processor, ITick
    {
        //TODO know what is the 'active' attack

        private readonly Group<Combatant, Target> _group = default;
        public void Tick(float delta)
        {
            //combatants have a basic attack
            foreach (var combatant in _group)
            {
                //if(combatant.Has<ActiveWeapon>())
                //INVESTIGATE is there a default attack damage value?
                combatant.TargetComponent().Value.Get<HealthModifier>().value += -10;
            }
        }
    }
}