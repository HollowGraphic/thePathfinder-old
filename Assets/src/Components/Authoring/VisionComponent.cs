using Drawing;
using Pixeye.Actors;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Components.Authoring
{
    public class VisionComponent : AuthoringComponent
    {
        public Vision vision;

        public override void Set(ref ent entity)
        {
            entity.Set(vision);
        }

        public override void DrawGizmos()
        {
            var eyePos = transform.position + (Vector3.up * vision.height);
            if (vision.cone)
            {
                float angleRad = vision.angle / 2 * (math.PI / 180);
                var vecAngleRight = new Vector3(math.sin(angleRad), 0, math.cos(angleRad)) * vision.range + eyePos;
                var vecAngleLeft = new Vector3(math.sin(-angleRad), 0, math.cos(-angleRad)) * vision.range + eyePos;
                
                using (Draw.InLocalSpace(transform))
                {
                    var position = transform.position;
                    Draw.Line(eyePos, vecAngleRight + position);
                    Draw.Line(eyePos, vecAngleLeft + position);
                }
                //TODO render arc
            }
            else
            {
                //draw forward sight line
                Draw.Ray(eyePos, transform.forward * vision.range);
                //draw circular range
                Draw.CircleXZ(eyePos, vision.range);
            }
        }
    }
}