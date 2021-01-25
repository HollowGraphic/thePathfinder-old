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
        [FormerlySerializedAs("_navigator")] [SerializeField] private Navigator navigator;
        [FormerlySerializedAs("_mover")] [SerializeField] private Mover mover;
        [FormerlySerializedAs("_maxSpeed")] [SerializeField] private MaxSpeed maxSpeed;
        [FormerlySerializedAs("_rotationSpeed")] [SerializeField, ShowIf("CanRotate")] private RotationSpeed rotationSpeed;

        public override void Set(ref ent entity)
        {
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