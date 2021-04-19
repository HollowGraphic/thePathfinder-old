using System.Collections.Generic;
using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Game;
using ThePathfinder.Game.Abilities;
using ThePathfinder.Game.Orders;
using ThePathfinder.Input;
using UnityEngine;

namespace ThePathfinder.Processors.Input
{
    internal sealed class ProcessAbilityOrders : ProcessInput, ITick
    {
        protected override int CategoryId => Category.Abiltity_Map;
        private readonly Group<Combatant, Selected, Abilities> _abilityUnits = default;
        private readonly Group<PointerHover> _entitiesUnderMouse = default;
        private readonly Dictionary<ent, Dictionary<int, IAbility>> _entityAbilities = default;

        /// <summary>
        /// Need to know if an ability is waiting for confirmation
        /// </summary>
        private bool _requireConfirmation;

        public ProcessAbilityOrders()
        {
        }

        public void Tick(float delta)
        {
            if (_abilityUnits.length > 1) return;
            ent entity = _abilityUnits[0];
            bool queueAbility = Player.GetButton(ActionId.Order_Queue);
            //cast only if we have an ability primed
            // if (_abilityPrimed && Player.GetButtonDown(ActionId.Ability_Cast))
            // {
            //     //fill out the required fields before casting
            //
            //     //set cast location
            //     if (_primedAbility.Has<CastLocation>())
            //     {
            //         this.Log("ability requires location");
            //         _primedAbility.CastLocationComponent().value = Mouse.GetWorldPosition();
            //     }
            //
            //     //set target
            //     if (_primedAbility.Has<Target>())
            //         //there should only be one
            //         foreach (var entity in _entitiesUnderMouse)
            //             _primedAbility.TargetComponent().Value = entity;
            //
            //     this.Log("Casting Ability");
            //     _primedAbility.Get<Cast>();
            //     _abilityPrimed = false;
            //     EnableConflictingInputs();
            // }

            if (Player.GetButtonUp(ActionId.Ability_Cast)) EnableConflictingInputs();


            CheckAbilityButton(entity, ActionId.Ability_0, queueAbility);

            
            if (Player.GetButtonDown(ActionId.Ability_Abort) && _requireConfirmation)
            {
                //reenable other input
                EnableConflictingInputs();
                //current ability cancel
                //TODO cancel ability when player selects different unit
                CancelPrimedAbility();
                
            }
        }

        private void CancelPrimedAbility()
        {
            throw new System.NotImplementedException();
        }

        private void CheckAbilityButton(ent entity, int abilityId, bool queueAbility)
        {
            if (Player.GetButtonDown(abilityId))
            {
                Debug.Log("Ability Key fired");
                QueueProcedure queueProcedure = queueAbility ? QueueProcedure.QueueBehind : QueueProcedure.None;
                foreach (ent unit in _abilityUnits)
                {
                    Debug.Log("We have units with abilities");

                    if (entity.AbilitiesComponent().value.TryGetValue(abilityId, out IAbility ability))
                    {
                        if (ability is IRequireConfirmation {AbilityPrimed: false} requireConfirmation)
                        {
                            requireConfirmation.AbilityPrimed = _requireConfirmation = true;
                            return;
                        }

                        if (ability is IManipulateSelf manipulateSelf)
                        {
                            manipulateSelf.SetSelf(entity);
                        }
                        if(ability is IRequireLocation requireLocation)
                        {
                            requireLocation.SetLocation(Mouse.GetWorldPosition());
                        }
                        
                        Ecs.Send(new SignalAssignOrder(unit, new AbilityOrder(unit, ability, OrderType.Default),
                            ability.Interrupt,queueProcedure));
                    }
                    else
                    {
                        //in the future, we will just return
                        this.Log("Entity has no such ability", LogType.Warning);
                    }
                }
            }
        }

        //TODO find way to satisfy ability requirements
    }
}