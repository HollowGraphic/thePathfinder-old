using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Common
{
    public sealed class ProcessEntityRotation : Processor, ITick
    {
        private readonly Group<Rotation> _group = default;
        public void Tick(float delta)
        {
            foreach (ent entity in _group)
            {
                AxisConstraints constraints = entity.RotatorComponent().constraints;
                Vector3 euler = entity.RotationComponent().value.eulerAngles;
                if (constraints.x)
                    euler.x = 0;
                if (constraints.y)
                    euler.y = 0;
                if (constraints.z)
                    euler.z = 0;
                
                entity.transform.Rotate(euler, Space.Self);
                
            }
        }
    }
}