using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

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
                //TODO if(combatant.Has<ActiveWeapon>())
                //else
                var attackerPos = attacker.transform.position;

                var targetPos = attacker.TargetComponent().Value.transform.position;
                Debug.Log(Msg.BuildWatch("Target", targetPos.ToString()));
                Debug.Log(Msg.BuildWatch("Attacker", attackerPos.ToString()));
                if(math.distance(attackerPos, targetPos) <= attacker.CombatantComponent().attackRange)
                {
                    Debug.Log("Adding Damage");
                    attacker.TargetComponent().Value.Get<HealthModifier>().value = -10;//INVESTIGATE is there a default attack damage value?
}
                //TODO store statmods on ents structure, then loop through them to apply mods
            }
        }
    }
}