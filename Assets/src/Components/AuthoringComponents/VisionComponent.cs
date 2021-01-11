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
                var right = Quaternion.AngleAxis(vision.angle * .5f, Vector3.up) * transform.forward;
                float angleRad = vision.angle / 2 * (math.PI / 180);
                var vecAngleRight = new Vector3(math.sin(angleRad), 0, math.cos(angleRad)) * vision.range + eyePos;
                var vecAngleLeft = new Vector3(math.sin(-angleRad), 0, math.cos(-angleRad)) * vision.range + eyePos;

                //var left = Quaternion.AngleAxis(-vision.angle, Vector3.up) * transform.forward;
                using (Draw.InLocalSpace(transform))
                {
                    Draw.Line(eyePos, vecAngleRight + transform.position);
                    Draw.Line(eyePos, vecAngleLeft + transform.position);
                }

                //arc doesnt' render
                // Draw.Arc(eyePos , vecAngleRight, vecAngleRight);
                // Draw.Arc(Vector3.zero, Vector3.right, Vector3.left );
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