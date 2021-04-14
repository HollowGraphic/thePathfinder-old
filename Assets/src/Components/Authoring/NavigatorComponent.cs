using System;
using Pathfinding;
using Pixeye.Actors;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization; //using ThePathfinder.Processors.Navigation;

namespace ThePathfinder.Components.Authoring
{
    [RequireComponent(typeof(IAstarAI))]
    public class NavigatorComponent : AuthoringComponent
    {
        [SerializeField] private Navigator navigator;
        [SerializeField] private Translator translator;
        [SerializeField] private MaxSpeed maxSpeed;
        [SerializeField] private Rotator rotator;

        public override void Set(ref ent entity)
        {
            translator.canMove = true;
            entity.Set(navigator);
            entity.Set(translator);
            entity.Set(maxSpeed);
            if(rotator.canRotate)
                entity.Set(rotator);
            //entity.Set(new VectorPath());
            
        }
    }
}