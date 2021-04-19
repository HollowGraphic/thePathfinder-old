using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Game.Abilities
{
    public class AbilityTeleport : IRequireLocation, IManipulateSelf
    {
        private Vector3 _teleportLocation;
        private ent _entity;
        public void Cast()
        {
            //Cast should use ECS to perform
            //That way we can fire and forget
            //otherwise we run the risk of dealing with tasks that should not be canceled
            _entity.transform.position = _teleportLocation;
            IsComplete = true;
        }

        public bool Interrupt => true;
        public bool IsComplete { get; private set; }

        public void SetLocation(Vector3 location)
        {
            _teleportLocation = location;
        }

        public void SetSelf(ent entity)
        {
            _entity = entity;
        }
    } 
    #region REQUIREMENTS

    public interface IAbility
    {
        public bool Interrupt { get; } 
        public bool IsComplete { get;}
        public void Cast();
    }
    
    public interface IRequireLocation : IAbility
    {
        public void SetLocation(Vector3 location);
    }

    public interface IRequireConfirmation : IAbility
    {
        public bool AbilityPrimed { get; set; }
    }
    public interface IRequireTarget : IAbility
    {
        public void SetTarget(ent entity);
    }
    public interface IManipulateSelf
    {
        public void SetSelf(ent entity);
    }
    public interface IProc{}

    public interface IProcOnComplete
    {
        public void OnComplete();
    }
    #endregion
}