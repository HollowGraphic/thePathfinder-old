using System;
using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;
using Component = ThePathfinder.Components.Component;


namespace ThePathfinder.Processors
{
    sealed class ProcessTargetAcquisition : Processor, ITick
    {
        [ExcludeBy(Component.Target, Component.MoveToDestination)]
        private readonly Group<VisibleTargets> _needsTarget = default;
        private readonly Group<Target> _targetingGroup = default;

        public void Tick(float delta)
        {
            foreach (var combatant in _needsTarget)
            {
                Debug.Log("Scoring Targets");
                var cVisibleTargets = combatant.VisibleTargetsComponent();
                if (cVisibleTargets.value.length == 0) continue;
                float closest = Single.PositiveInfinity;
                ent closestTarget = default;
                foreach (var target in cVisibleTargets.value)
                {
                    var distance = math.distance(target.transform.position, combatant.transform.position);
                    if (distance < closest)
                    {
                        closestTarget = target;
                    }
                }
                Debug.Log(Msg.BuildWatch("Found Target", closestTarget.exist.ToString()));
                combatant.Get<Target>().value = closestTarget;
            }

            foreach (var combatant in _targetingGroup)
            {
                Target target = combatant.TargetComponent();
                var distanceToTarget = math.distance(target.value.transform.position, combatant.transform.position);
                Draw.Line(combatant.transform.position, target.value.transform.position, Color.red);
                if (distanceToTarget > combatant.VisionComponent().range)
                {
                    Debug.Log("Target Lost");
                    combatant.Remove<Target>();
                }
            }
        }
    }
}