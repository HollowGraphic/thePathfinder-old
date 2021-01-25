using System.Collections.Generic;
using BigBiteStudios.Logging;
using Pathfinding;
using Pixeye.Actors;
using ThePathfinder.Components;
using UnityEngine;

namespace ThePathfinder.Processors.Navigation
{
    internal sealed class ProcessPathRequest : Processor
    {
        private readonly Group<PathRequest, Navigator> _pathRequesters = default;

        public override void HandleEcsEvents()
        {
            foreach (var requester in _pathRequesters.added)
            {
                // fire a ray first to see if we can just move in straight line
                var direction = (Vector3) requester.DestinationComponent().value - requester.transform.position;
                if (Physics.Raycast(requester.transform.position + Vector3.up * .01f,
                    direction.normalized, direction.magnitude))
                {
                    Debug.Log("Calculate path");
                    // Start a new path to the targetPosition, call the the OnPathComplete function
                    // when the path has been calculated (which may take a few frames depending on the complexity)
                    var path = ABPath.Construct(requester.transform.position, requester.DestinationComponent().value,
                        p =>
                        {
                            Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

                            // Path pooling. To avoid unnecessary allocations paths are reference counted.
                            // Calling Claim will increase the reference count by 1 and Release will reduce
                            // it by one, when it reaches zero the path will be pooled and then it may be used
                            // by other scripts. The ABPath.Construct and Seeker.StartPath methods will
                            // take a path from the pool if possible. See also the documentation page about path pooling.
                            p.Claim(this);
                            if (!p.error)
                            {
                                Debug.Log(Msg.BuildWatch(" Path N Count ", p.vectorPath.Count.ToString()));

                                //adjust starting path node
                                {
                                    var bestDist = float.PositiveInfinity;
                                    var bestFactor = 0f;
                                    var bestIndex = 0;

                                    for (var i = 0; i < p.vectorPath.Count - 1; i++)
                                    {
                                        var factor =
                                            VectorMath.ClosestPointOnLineFactor(p.vectorPath[i], p.vectorPath[i + 1],
                                                requester.transform.position);
                                        var closest = Vector3.Lerp(p.vectorPath[i], p.vectorPath[i + 1], factor);

                                        var dist = (requester.transform.position - closest).sqrMagnitude;

                                        if (dist < bestDist)
                                        {
                                            bestDist = dist;
                                            bestFactor = factor;
                                            bestIndex = i;
                                        }
                                    }

                                    Debug.Log(Msg.BuildWatch("Best Index", bestIndex.ToString()));
                                    p.vectorPath[bestIndex] +=
                                        (p.vectorPath[bestIndex + 1] - p.vectorPath[bestIndex]).normalized * bestDist;
                                }
                                //set the vector path
                                requester.Get<VectorPath>() = new VectorPath(p.vectorPath);
                            }

                            p.Release(this);
                        });
                    AstarPath.StartPath(path);
                }
                else //no obstacles in the way, manually set path 
                {
                    Debug.Log("Straight line path");
                    requester.Get<VectorPath>().value = new List<Vector3>
                        {requester.transform.position, requester.DestinationComponent().value};
                }

                // Reset navigator
                requester.NavigatorComponent().currentPathNode = 0;
                requester.Remove<PathRequest>();
            }
        }
    }
}