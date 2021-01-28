using System.Collections.Generic;
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
        [SerializeField] private Mover mover;
        [SerializeField] private MaxSpeed maxSpeed;
        [FormerlySerializedAs("_rotationSpeed")] [SerializeField, ShowIf("CanRotate")] private RotationSpeed rotationSpeed;

        public override void Set(ref ent entity)
        {
            mover.canMove = true;
            entity.Set(navigator);
            entity.Set(mover);
            entity.Set(maxSpeed);
            entity.Set(rotationSpeed);
            //entity.Set(new VectorPath());
            entity.Set<DestinationQueue>().destinations = new Queue<Destination>(20);
        }

#if UNITY_EDITOR
        bool CanRotate()
        {
            return mover.canRotate;
        }
#endif
    }
}