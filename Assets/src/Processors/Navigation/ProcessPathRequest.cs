using System.Collections.Generic;
using BigBiteStudios.Logging;
using Drawing;
using Pathfinding;
using Pixeye.Actors;
using ThePathfinder.Components;
using Unity.Mathematics;
using UnityEngine;


namespace ThePathfinder.Processors.AI
{
    sealed class ProcessPathRequest : Processor
    {
        private readonly Group<PathRequest, Navigator> _pathRequesters = default;
        private readonly Group<VectorPath> _vectorPaths = default;

        public override void HandleEcsEvents()
        {
            foreach (var requester in _pathRequesters.added)
            {
                // fire a ray first to see if we can just move in straight line
                var direction = ((Vector3) requester.DestinationComponent().value - requester.transform.position);
                if (Physics.Raycast((requester.transform.position + (Vector3.up *.01f)),
                    direction.normalized, direction.magnitude ))
                {
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
                                //set the vector path
                                requester.Get<VectorPath>().value = new List<Vector3>(p.vectorPath);
                                requester.Remove<PathRequest>();
                            }

                            p.Release(this);
                        });
                    AstarPath.StartPath(path);
                }
                else //no obstacles in the way, ignore path request, and if there happens to be a vector path, remove it 
                {
                    Debug.Log("Skipping path request");
                    requester.Remove<PathRequest>();
                    if (requester.Has<VectorPath>()) requester.Remove<VectorPath>();
                }
            }

        }
    }
}