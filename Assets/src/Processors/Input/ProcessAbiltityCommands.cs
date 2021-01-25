using BigBiteStudios;
using Pathfinding.Ionic.Zip;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using UnityEngine;

namespace ThePathfinder.Processors
{
    sealed class ProcessAbiltityCommands : ProcessorInput, ITick
    {
        private readonly Group<Combatant, Selected, Abilities> _abilityUnits = default;
        private readonly Group<PointerHover> _entityiesUnderMouse = default;
        private ent primedAbility;
        private bool abilityPrimed;

        public ProcessAbiltityCommands()
        {
            primedAbility = Layer.Entity.Create();
        }

        public void Tick(float delta)
        {
            //cast only if we have an abiilty primed
            if (abilityPrimed && player.GetButtonDown(ActionId.Ability_Cast))
            {
                //fill out the required fields before casting

                //set cast location
                if (primedAbility.Has<CastLocation>())
                {
                    this.Log("ability requires location");
                    primedAbility.CastLocationComponent().value = mouse.GetWorldPosition();
                }

                //set target
                if (primedAbility.Has<Target>())
                {
                    //there should only be one
                    foreach (var entity in _entityiesUnderMouse)
                    {
                        primedAbility.TargetComponent().value = entity;
                    }
                }

                this.Log("Casting Ability");
                primedAbility.Get<Cast>();
                abilityPrimed = false;
                EnableConflictingInputs();
            }

            if (player.GetButtonUp(ActionId.Ability_Cast))
            {
                EnableConflictingInputs();
            }


            CheckAbilityButton(ActionId.Ability_0);


            if (player.GetButtonDown(ActionId.Ability_Abort))
            {
                //reenable other input
                EnableConflictingInputs();
            }
        }

        private void CheckAbilityButton(int abilityId)
        {
            if (player.GetButtonDown(abilityId))
            {
                this.Log("Ability Key fired");
                foreach (var unit in _abilityUnits)
                {
                    this.Log("We have units with abilities");
                    var availableAbilities = unit.Get<Abilities>();
                    if (availableAbilities.value.TryGetValue(abilityId, out var ability))
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
                            primedAbility = ability;
                            abilityPrimed = true;
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