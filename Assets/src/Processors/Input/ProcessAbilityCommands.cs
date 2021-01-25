using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using UnityEngine;

namespace ThePathfinder.Processors.Input
{
    internal sealed class ProcessAbilityCommands : ProcessorInput, ITick
    {
        private readonly Group<Combatant, Selected, Abilities> _abilityUnits = default;
        private readonly Group<PointerHover> _entityiesUnderMouse = default;
        private bool _abilityPrimed;
        private ent _primedAbility;

        public ProcessAbilityCommands()
        {
            _primedAbility = Layer.Entity.Create();
        }

        public void Tick(float delta)
        {
            //cast only if we have an abiilty primed
            if (_abilityPrimed && Player.GetButtonDown(ActionId.Ability_Cast))
            {
                //fill out the required fields before casting

                //set cast location
                if (_primedAbility.Has<CastLocation>())
                {
                    this.Log("ability requires location");
                    _primedAbility.CastLocationComponent().value = Mouse.GetWorldPosition();
                }

                //set target
                if (_primedAbility.Has<Target>())
                    //there should only be one
                    foreach (var entity in _entityiesUnderMouse)
                        _primedAbility.TargetComponent().Value = entity;

                this.Log("Casting Ability");
                _primedAbility.Get<Cast>();
                _abilityPrimed = false;
                EnableConflictingInputs();
            }

            if (Player.GetButtonUp(ActionId.Ability_Cast)) EnableConflictingInputs();


            CheckAbilityButton(ActionId.Ability_0);


            if (Player.GetButtonDown(ActionId.Ability_Abort))
                //reenable other input
                EnableConflictingInputs();
        }

        private void CheckAbilityButton(int abilityId)
        {
            if (Player.GetButtonDown(abilityId))
            {
                this.Log("Ability Key fired");
                foreach (var unit in _abilityUnits)
                {
                    this.Log("We have units with abilities");
                    var availableAbilities = unit.Get<Abilities>();
                    if (availableAbilities.Value.TryGetValue(abilityId, out var ability))
                    {
                        if (ability.Has<InstaCast>()) //can we cast it right away?
                        {
                            this.Log("Can insta cast");
                            //just cast it
                            ability.Get<Cast>();
                        }
                        else
                        {
                            this.Log("Priming Ability");
                            //prime ability. cache it and wait for player to cast
                            _primedAbility = ability;
                            _abilityPrimed = true;
                            DisableConflictingInputs();
                        }
                    }
                    else
                    {
                        //in the future, we will just return
                        this.Log("Entity has no such ability", LogType.Warning);
                    }
                }
            }
        }

        protected override int SetCategoryId()
        {
            return Category.Abiltity_Map;
        }
    }
}