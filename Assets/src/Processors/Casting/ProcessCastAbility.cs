using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Casting
{
    internal sealed class ProcessCastAbility : Processor, ITick
    {
        private readonly Group<Cast, CastLocation> _castLocations = default;
        private readonly Group<Cast, InstaCast> _instaCasts = default;
        private readonly Group<Cast, Target> _targetCasters = default;

        public void Tick(float delta)
        {
            
        }

        public override void HandleEcsEvents()
        {
            foreach (var caster in _castLocations.added) Debug.Log("Casting at " + caster.CastLocationComponent().value);

            foreach (var caster in _instaCasts.added) Debug.Log("Insta Casting");

            foreach (var caster in _targetCasters.added) Debug.Log("Target Casting");
        }
    }
}