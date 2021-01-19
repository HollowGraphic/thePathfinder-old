using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;


namespace ThePathfinder.Processors
{
    sealed class ProcessEntityMovement : Processor, ITick
    {
        private readonly Group<Heading, Mover, MaxSpeed> _group = default;

        public void Tick(float delta)
        {
            foreach (var entity in _group)
            {
                var heading = entity.HeadingComponent();
                var cMove = entity.MoverComponent();
                var cRotSpeed = entity.RotationSpeedComponent();
                if (cMove.canRotate)
                {
                    var targetRotation = quaternion.LookRotation(heading.value, math.up());
                    var rotation = math.slerp(entity.transform.rotation, targetRotation, delta * cRotSpeed.value);

                    entity.transform.rotation = rotation;
                }

                if (cMove.canMove)
                {
                    //debug heading
                    Draw.Arrow(entity.transform.localPosition, entity.transform.position + (Vector3) heading.value);
                    var speedFactor = entity.Has<SpeedMod>() ? entity.SpeedModComponent().value : 1;
                    entity.transform.position += (Vector3)heading.value * entity.MaxSpeedComponent().value *
                                                 speedFactor * delta;
                     
                }
            }
        }
    }
}