using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Navigation
{
    /// <summary>
    ///     Updates entities with vision. Updates their list of entities within vision range
    /// </summary>
    internal sealed class ProcessVision : Processor, ITick
    {
        /// <summary>
        ///     entities that can 'see'
        /// </summary>
        private readonly Group<Vision, VisibleTargets> _seers = default;

        private readonly Collider[] _targetsInRange = new Collider[GameSettings.VisibleTargetCapacity];

        public void Tick(float delta)
        {
            foreach (ent seer in _seers)
            {
                Vision cVision = seer.VisionComponent();
                VisibleTargets cTargets = seer.VisibleTargetsComponent();

                Vector3 eyePosition = (float3) seer.transform.position + math.up() * cVision.height;

                //find colliders within range
                int targetCount =
                    Physics.OverlapSphereNonAlloc(eyePosition, cVision.range, _targetsInRange, cVision.mask);
                if (targetCount == 0 && cTargets.value.length != 0)
                    for (int i = 0; i < cTargets.value.length; i++)
                        seer.VisibleTargetsComponent().value.RemoveAt(i);

                Debug.Log(Msg.BuildWatch("Found Potential Targets", targetCount.ToString()));
                //INVESTIGATE: sort targets by distance?????
                for (int i = 0; i < targetCount; i++)
                {
                    Collider targetCollider = _targetsInRange[i];
                    Vector3 dir = targetCollider.bounds.center - eyePosition;
                    if (cVision.cone)
                    {
                        float angle = Vector3.Angle(seer.transform.forward, dir.normalized);
                        if (angle > cVision.angle / 2)
                        {
                            Debug.Log("Skipping");
                            continue; //skip if target is not inside of cone
                        }
                    }

                    Ray ray = new Ray(eyePosition, dir);
                    //check if we can see target
                    //INVESTIGATE: is it better to have the enemies check this themselves? Could be more performant
                    //INVESTIGATE: should we try a shape cast instead?
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        //we can see it
                        if (hit.collider != targetCollider) continue;
                        Debug.Log("Can see target");
                        Draw.Ray(ray, dir.magnitude / 2, Color.yellow);
                        
                        if (!targetCollider.TryGetComponent(out Actor actor)) continue;//INVESTIGATE expensive?
                        ent targetEntity = actor.entity;
                        bool hasTarget = cTargets.value.Has(targetEntity);
                        bool isDead = targetEntity.Has<Dead>();
                        
                        //handle dead entities 
                        if (isDead || !targetEntity.exist)
                        {
                            //from target list, if they happen to be in list
                            if(hasTarget) cTargets.value.Remove(targetEntity);
                            continue;
                        }
                        if(hasTarget) continue;//already has target, continue
                        Debug.Log("Adding "+ targetEntity +" As Potential Target");
                        cTargets.value.Add(targetEntity);
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