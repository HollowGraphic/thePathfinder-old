using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Common
{
    //TODO this should happen at the end of a frame?
    internal sealed class ProcessMovers : Processor, ITick
    {
        private readonly Group<Heading,MaxSpeed> _group = default;

        public void Tick(float delta)
        {
            foreach (ent entity in _group)
            {
                Heading heading = entity.HeadingComponent();
                Translator cMove = entity.TranslatorComponent();
                Rotator cRotator = entity.RotatorComponent();
                if (cRotator.canRotate)
                {
                    
                    //entity.transform.rotation = rotation;
                }

                if (cMove.canMove)
                {
                    //debug heading
                    Vector3 currentPos;
                    Draw.Arrow(entity.transform.localPosition, (currentPos = entity.transform.position) + (Vector3) heading.value);
                    float speedFactor = entity.Has<SpeedMod>() ? entity.SpeedModComponent().value : 1;
                    Vector3 position = (Vector3) heading.value * (entity.MaxSpeedComponent().value * speedFactor * delta) + currentPos;
                    entity.Get<Translation>() = new Translation(position);
                    //entity.transform.position += position;
                }
            }
        }
    }
}