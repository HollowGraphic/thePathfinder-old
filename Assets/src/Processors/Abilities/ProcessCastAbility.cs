using Pixeye.Actors;
using ThePathfinder.Components;


namespace ThePathfinder.src.Processors.Abilities
{
    sealed class ProcessCastAbility : Processor, ITick
    {
        private readonly Group<Cast, CastLocation> _castLocations = default;
        private readonly Group<Cast, InstaCast> _instaCasts = default;
        private readonly Group<Cast, Target> _targetCasters = default;

        public override void HandleEcsEvents()
        {
            foreach (var caster in _castLocations.added)
            {
                this.Log("Casting at " + caster.CastLocationComponent().value.ToString());
            }

            foreach (var caster in _instaCasts.added)
            {
                this.Log("Insta Casting");
            }

            foreach (var caster in _targetCasters.added)
            {
                this.Log("Target Casting");
            }
        }

        public void Tick(float delta)
        {
        }
    }
}