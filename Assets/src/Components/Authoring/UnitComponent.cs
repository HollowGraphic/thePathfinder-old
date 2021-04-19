using System.Collections.Generic;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Game.Abilities;
using ThePathfinder.Input;

namespace ThePathfinder.Components.Authoring
{
    /// <summary>
    /// Units follow orders
    /// </summary>
    public sealed class UnitComponent : AuthoringComponent
    {
        public Agent agent;
        
        public override void Set(ref ent entity)
        {
            entity.Set(agent);
            entity.Set<Commandable>();
            //BUG remove following code
            var abilities = new Dictionary<int, IAbility>(5) {{ActionId.Ability_0, new AbilityTeleport()}};
            entity.Set(new Abilities(abilities));
        }
    }
    
}