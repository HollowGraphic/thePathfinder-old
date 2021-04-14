using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Game
{
    internal sealed class ProcessTargetAcquisition : Processor, ITick
    {
        [ExcludeBy(GameComponent.Target, GameComponent.Passive)]
        private readonly Group<VisibleTargets> _entitiesWithoutTargets = default;
        [ExcludeBy(GameComponent.Passive)]
        private readonly Group<Target> _entitiesWithTargets = default;
        
        public void Tick(float delta)
        {
            foreach (ent entity in _entitiesWithTargets)
            {
                Target target = entity.TargetComponent();
                if (!target.Value.exist || target.Value.Has<Dead>())
                {
                    Debug.Log("Target is dead, removing");
                    //target has been destroyed, remove it
                    entity.Remove<Target>();
                    continue; //skip to next entity
                }

                float distanceToTarget = Vector3.Distance(target.Value.transform.position, entity.transform.position);
                Draw.Line(entity.transform.position, target.Value.transform.position, Color.red);
                float aggroRange = entity.AggroComponent().value;
                if (distanceToTarget < aggroRange) continue;//all good, within attack range
                Debug.Log("Entity "+entity+" lost target");
                Debug.Log("Entity Range: "+aggroRange+" Distance to target: " + distanceToTarget);
                entity.Remove<Target>();
            }

            foreach (ent entity in _entitiesWithoutTargets)
            {
                                
                VisibleTargets cVisibleTargets = entity.VisibleTargetsComponent();
                Debug.Log(string.Concat("Scoring ",cVisibleTargets.value.length," Targets"));
                if (cVisibleTargets.value.length == 0) continue;
                float closestDistance = float.PositiveInfinity;
                ent closestTarget = default;
                foreach (ent target in cVisibleTargets.value)
                {
                    float distanceToTarget = math.distance(target.transform.position, entity.transform.position);
                    if (distanceToTarget > closestDistance) continue;
                    Debug.Log("Distance to target smaller than closest distance");
                    if(distanceToTarget > entity.AggroComponent().value) continue;
                    Debug.Log("Distance to target smaller than attack range, setting potential target");
                    closestTarget = target;
                    closestDistance = distanceToTarget;
                }

                if (!closestTarget.exist) continue;

                Debug.Log(string.Concat("Chose Target", closestTarget.exist.ToString()));
                entity.Get<Target>().Value = closestTarget;
            }
        }

        public override void HandleEcsEvents()
        {
            foreach (ent entity in _entitiesWithTargets.removed)
            {
                if(entity.Has<Target>()) entity.Remove<Target>();
            }
        }
    }
}