using System;
using Drawing;
using Pathfinding;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;


namespace ThePathfinder.Processors.AI
{
    sealed class ProcessInterpolator : Processor, ITick
    {
        private readonly Group<VectorPath, Interpolator> _paths;
        public override void HandleEcsEvents()
        {
            //find best point
            foreach (var pather in _paths.added)
            {
                var path = pather.VectorPathComponent().value;
                float bestDist = float.PositiveInfinity;
                float bestFactor = 0f;
                int bestIndex = 0;
                
                //check the first 3 points to see which one we want to go to?
                
                for (int i = 0; i < path.Count-1; i++) 
                {
                    float factor = VectorMath.ClosestPointOnLineFactor(path[i], path[i+1], pather.transform.position);
                    Vector3 closest = Vector3.Lerp(path[i], path[i+1], factor);
                    float dist = (pather.transform.position - closest).sqrMagnitude;

                    if (dist < bestDist) {
                        bestDist = dist;
                        bestFactor = factor;
                        bestIndex = i;
                    }
                }

                //var v = path[bestIndex + 1] - path[bestIndex].normalized * math.clamp(bestFactor, 0, 1);
                
                //this we save
                //var bestPoint = path[bestIndex] + v;
                //Draw.Cross(bestPoint, Color.green); //this is the best point
                //path[bestIndex] = bestPoint;
                pather.InterpolatorComponent().currentIndex = bestIndex;
            }
        }

        public void Tick(float delta)
        {
            foreach (var pather in _paths)
            {
                var path = pather.VectorPathComponent().value;
                var interpolator = pather.InterpolatorComponent();
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Draw.CircleXZ(path[i], .5f, Color.blue);
                }

                if (math.distance(path[interpolator.currentIndex], pather.transform.position) < 0.1f)
                {
                    if(interpolator.currentIndex +1 == path.Count) break;// on last waypoint
                    interpolator.currentIndex++;
                }

                //todo : move this to different processor
                pather.Get<Heading>().value =( (Vector3)path[interpolator.currentIndex] - pather.transform.position).normalized;
                pather.InterpolatorComponent() = interpolator;
            }
        }
    }
}