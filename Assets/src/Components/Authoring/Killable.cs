using System.Globalization;
using BigBiteStudios.Logging;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public sealed class Killable : AuthoringComponent
    {
        [SerializeField]
        private MaxHealth maxHealth;
        public override void Set(ref ent entity)
        {
            //set entity
            entity.Set(maxHealth);
            entity.Set<Health>() = new Health(maxHealth.value);
            entity.layer.Observer.Add(entity, (e) => e.HealthComponent().value,
                f => { Debug.Log(Msg.BuildWatch("Health", f.ToString(CultureInfo.InvariantCulture))); });
        }
    }
}