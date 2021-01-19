using System;
using System.Collections.Generic;
using System.Diagnostics;
using Pathfinding;
using Pixeye.Actors;
using Sirenix.OdinInspector;
//using ThePathfinder.Processors.Navigation;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ThePathfinder.Components.Authoring
{
    [RequireComponent(typeof(IAstarAI))]
    public class NavigatorComponent : AuthoringComponent
    {
        [SerializeField] private Navigator _navigator;
        [SerializeField] private Mover _mover;
        [SerializeField] private MaxSpeed _maxSpeed;
        [SerializeField, ShowIf("CanRotate")] private RotationSpeed _rotationSpeed;

        public override void Set(ref ent entity)
        {
            entity.Set(_navigator);
            entity.Set(_mover);
            entity.Set(_maxSpeed);
            entity.Set(_rotationSpeed);
            entity.Set<Interpolator>();
            entity.Set<DestinationQueue>().destinations = new Queue<Destination>(20);
        }

#if UNITY_EDITOR
        bool CanRotate()
        {
            return _mover.canRotate;
        }
#endif
    }
}