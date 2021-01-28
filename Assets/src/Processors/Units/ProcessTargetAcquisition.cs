using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Units
{
    internal sealed class ProcessTargetAcquisition : Processor, ITick
    {
        [ExcludeBy(GameComponent.Target, GameComponent.MoveToDestination)]
        private readonly Group<VisibleTargets> _needsTarget = default;

        private readonly Group<Target> _targetingGroup = default;

        public void Tick(float delta)
        {
            foreach (var combatant in _needsTarget)
            {
                Debug.Log("Scoring Targets");
                var cVisibleTargets = combatant.VisibleTargetsComponent();
                if (cVisibleTargets.value.length == 0) continue;
                var closest = float.PositiveInfinity;
                ent closestTarget = default;
                foreach (var target in cVisibleTargets.value)
                {
                    var distance = math.distance(target.transform.position, combatant.transform.position);
                    if (distance < closest) closestTarget = target;
                }

                Debug.Log(Msg.BuildWatch("Found Target", closestTarget.exist.ToString()));
                combatant.Get<Target>().Value = closestTarget;
            }

            foreach (var combatant in _targetingGroup)
            {
                var target = combatant.TargetComponent();
                if (!target.Value.exist)
                {
                    //target has been destroyed, remove it
                    combatant.Remove<Target>();
                    continue;//skip to next entity
                }
                    
                float distanceToTarget = math.distance(target.Value.transform.position, combatant.transform.position);
                Draw.Line(combatant.transform.position, target.Value.transform.position, Color.red);
                if (distanceToTarget > combatant.VisionComponent().range)
                {
                    Debug.Log("Target Lost");
                    combatant.Remove<Target>();
                }
            }
        }
    }
}