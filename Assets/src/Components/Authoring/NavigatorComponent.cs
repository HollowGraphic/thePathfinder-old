using Drawing;
using Pathfinding;
using Pixeye.Actors;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    [RequireComponent(typeof(IAstarAI))]
    public class NavigatorComponent : AuthoringComponent
    {
        [SerializeField] private Navigator navigator;
        [SerializeField] private Translator translator;
        [SerializeField] private MaxSpeed maxSpeed;
        [SerializeField] private Rotator rotator;
        public float range;
        public override void Set(ref ent entity)
        {
            translator.canMove = true;
            entity.Set(navigator);
            entity.Set(translator);
            entity.Set(maxSpeed);
            if(rotator.canRotate)
                entity.Set(rotator);
        }

        public override void DrawGizmos()
        {
                        Vector3 randomDir = BigBiteStudios.VectorHelpers.GetRandomDirectionCircle(range);
                        Draw.Arrow(Vector3.zero, randomDir);
        }
    }
}