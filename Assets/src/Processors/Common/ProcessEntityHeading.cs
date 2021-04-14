using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessEntityHeading : Processor, ITick
    {
        private readonly Group<Heading> _group = default;

        public void Tick(float delta)
        {
            foreach (ent entity in _group)
            {
                if (entity.Has<Rotator>() && entity.RotatorComponent().canRotate)
                {
                    quaternion targetRotation = quaternion.LookRotation(entity.HeadingComponent().value, math.up());
                    quaternion rotation = math.slerp(entity.transform.rotation, targetRotation,
                        delta * entity.RotatorComponent().speed);
                    entity.Get<Rotation>() = new Rotation(rotation);
                }

                if (entity.Has<Translator>() && entity.TranslatorComponent().canMove)
                {
                    Heading heading = entity.HeadingComponent();
                    Vector3 translation = entity.Get<Translation>().value;
                    Debug.Log(Msg.BuildWatch("Previous Translation", translation.ToString()));
                    Draw.Arrow(entity.transform.localPosition, translation  + (Vector3) heading.value);
                    float speedFactor = entity.Has<SpeedMod>() ? entity.SpeedModComponent().value : 1;
                    
                    Vector3 newTranslation = (Vector3) heading.value * (entity.MaxSpeedComponent().value * speedFactor * delta) + translation;
                    entity.Get<Translation>() = new Translation(newTranslation); 
                    Debug.Log(Msg.BuildWatch("Next Position", newTranslation.ToString()));
                }
            }
        }
    }
}