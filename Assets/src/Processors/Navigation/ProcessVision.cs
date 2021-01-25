using BigBiteStudios.Logging;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using Drawing;
using UnityEngine;

namespace ThePathfinder.Processors.AI
{
    /// <summary>
    /// Updates entities with vision. Updates their list of entities within vision range
    /// </summary>
    sealed class ProcessVision : Processor, ITick
    {
        /// <summary>
        /// entities that can 'see'
        /// </summary>
        private readonly Group<Vision, VisibleTargets> _seers = default;

        private readonly Collider[] targetsInRange = new Collider[GameSettings.VISIBLE_TARGET_CAPACITY];

        public void Tick(float delta)
        {
            foreach (var seer in _seers)
            {
                var cVision = seer.VisionComponent();
                var cTargets = seer.VisibleTargetsComponent();

                Vector3 eyePosition = (float3) seer.transform.position + (math.up() * cVision.height);

                //find colliders within range
                int targetCount =
                    Physics.OverlapSphereNonAlloc(eyePosition, cVision.range, targetsInRange, cVision.mask);
                if (targetCount == 0 && cTargets.value.length != 0)
                {
                    for (int i = 0; i < cTargets.value.length; i++)
                    {
                        seer.VisibleTargetsComponent().value.RemoveAt(i);
                    }
                }

                Debug.Log(Msg.BuildWatch("Found Potential Targets", targetCount.ToString()));
                //INVESTIGATE: sort targets by distance?????
                for (int i = 0; i < targetCount; i++)
                {
                    var target = targetsInRange[i];
                    var dir = target.bounds.center - eyePosition;
                    if (cVision.cone)
                    {
                        var angle = Vector3.Angle(seer.transform.forward, dir.normalized);
                        if (angle > cVision.angle / 2)
                        {
                            Debug.Log("Skipping");
                            continue; //skip if target is not inside of cone
                        }
                    }

                    Ray ray = new Ray(eyePosition, dir);
                    //check if we can see target
                    //INVESTIGATE: is it better to have the enmies check this themselves? Could be more performant
                    //INVESTIGATE: should we try a shape cast instead?
                    if (Physics.Raycast(ray, out var hit))
                    {
                        //we can see it
                        if (hit.collider == target)
                        {
                            Debug.Log("Can see target");
                            Draw.Ray(ray, dir.magnitude / 2, Color.yellow);
                            if (target.TryGetComponent<Actor>(out var actor))
                            {
                                var entity = actor.entity;
                                if (!cTargets.value.Has(entity))
                                {
                                    Debug.Log("Adding Target");
                                    cTargets.value.Add(entity);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Hit nothing");
                    }
                }
            }
        }
    }
}