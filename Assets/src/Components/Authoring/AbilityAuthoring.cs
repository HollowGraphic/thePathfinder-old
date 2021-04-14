using System.Collections.Generic;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    [LabelText("Ability")]
   public sealed class AbilityAuthoring : AuthoringComponent
   {
       public Ability ability;
        public override void Set(ref ent entity)
        {
            //set entity
            entity.Set<Ability>();
        }
    }
}

public interface IAbility
{
    public void Perform();
}

public class FireBall : IAbility
{
    public string RefToAbilityPrefab;
    public void Perform()
    {
        
    }
}