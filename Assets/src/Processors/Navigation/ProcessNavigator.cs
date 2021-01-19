using System.Linq;
using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using Pathfinding;
using Pathfinding.Util;
using ThePathfinder.Components;
using ThePathfinder.Components.Authoring;
using Unity.Mathematics;
using UnityEngine;
using Component = ThePathfinder.Components.Component;


namespace ThePathfinder.Processors.AI
{
    sealed class ProcessNavigator : Processor, ITick
    {
        //[ExcludeBy(Component.PathRequest)]
        private readonly Group<Navigator, Destination> _navigatorsWithDestination = default;

        private bool approachingFinalWaypoint;

        /// <summary>
        /// How far to look for waypoints?
        /// </summary>
        private const float nextWaypointDistance = .5f;

        public override void HandleEcsEvents()
        {
            //get the next destination in the queue
            foreach (var entity in _navigatorsWithDestination.removed)
            {
                if (entity.Has<DestinationQueue>() && entity.DestinationQueueComponent().destinations.Count != 0)
                {
                    entity.Get<Destination>() = entity.DestinationQueueComponent().destinations.Dequeue();
                    entity.Get<PathRequest>();
                }
                else //no where else to go
                {
                    //remove heading so that they stop moving
                    if (entity.Has<Heading>()) entity.Remove<Heading>();
                    //remove move type
                    if (entity.Has<MoveToDestination>()) entity.Remove<MoveToDestination>();
                    if (entity.Has<AttackDestination>()) entity.Remove<AttackDestination>();
                    //remove path
                    entity.Remove<VectorPath>();
                }
            }

            // //on destination added
            // foreach (var navigator in _navigatorsWithDestination.added)
            // {
            //     // Reset the waypoint counter so that we start to move towards the first point in the path
            //     navigator.NavigatorComponent().currentWaypoint = 0;
            // }
        }

        public void Tick(float delta)
        {
            foreach (var unit in _navigatorsWithDestination)
            {

                // if (Time.Current >  cNavigator.lastRepath + cNavigator.repathRate)
                // {
                //     cNavigator.lastRepath = Time.Current;
                //     //path request
                //     navigator.Get<PathRequest>();
                //     continue;
                // }
                approachingFinalWaypoint = false;
                float distanceToWaypoint;
                //Heading heading;
                var destination = unit.DestinationComponent();

                //handle vector path
                // if (false)//(unit.Has<VectorPath>())
                // {
                //     VectorPath cVecPath = unit.VectorPathComponent();

                //     Debug.Log(Msg.BuildWatch("Current Way Point",
                //         unit.NavigatorComponent().currentWaypoint.ToString()));
                //     // Check in a loop if we are close enough to the current waypoint to switch to the next one.
                //     // We do this in a loop because many waypoints might be close to each other and we may reach
                //     // several of them in the same frame.

                //     while (true)
                //     {
                //         // The distance to the next waypoint in the path
                //         distanceToWaypoint = math.distancesq(unit.transform.position,
                //             cVecPath.value[unit.NavigatorComponent().currentWaypoint]);
                //         //navigating vector path
                //         if (distanceToWaypoint < nextWaypointDistance) //approaching next waypoint
                //         {
                //             Debug.Log(Msg.BuildWatch(unit.transform.name, "Close To NextWayPoint"));

                //             // Check if there is another waypoint or if we have reached the end of the vector path
                //             if (unit.NavigatorComponent().currentWaypoint + 1 < cVecPath.value.Count)
                //             {
                //                 Debug.Log(Msg.BuildWatch(unit.transform.name, "Incrementing"));

                //                 unit.NavigatorComponent().currentWaypoint++;
                //             }
                //             //must be on last waypoint
                //             else
                //             {
                //                 // Set a status variable to indicate that the agent has reached the end of the path.
                //                 // You can use this to trigger some special code if your game requires that.
                //                 Debug.Log(Msg.BuildWatch(unit.transform.name, "Reached end of Path"));
                //                 approachingFinalWaypoint = true;
                //                 break;
                //             }
                //         }
                //         else //not close enough yet, bump out 
                //         {
                //             break;
                //         }
                //     }

                //     // Direction to the next waypoint
                //     // Normalize it so that it has a length of 1 world unit
                //     //heading = new Heading((cVecPath.value[unit.NavigatorComponent().currentWaypoint] -
                //       //                     unit.transform.position).normalized);
                // }
                // else //handle straight line
                // {
                //     Debug.Log(Msg.BuildWatch(unit.transform.name, "Checkin distance to destination"));

                //     distanceToWaypoint = math.distance(unit.transform.position,
                //         destination.value);
                //     if (distanceToWaypoint < nextWaypointDistance) approachingFinalWaypoint = true;
                //     //heading = new Heading(math.normalize(destination.value - (float3) unit.transform.position));
                // }

                var distance = math.distance(unit.transform.position, destination.value);
                
                // Slow down smoothly upon approaching the end of the path and there are no more destinations queued
                // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
                unit.Get<SpeedMod>().value =distance < unit.NavigatorComponent().breakingDistance &&
                    unit.DestinationQueueComponent().destinations.Count == 0
                        ? math.sqrt(distance / unit.NavigatorComponent().breakingDistance)
                        : 1f;

                //Debug.Log(Msg.BuildWatch("Distance To Waypoint", distanceToWaypoint.ToString()));
                //we have reached our destination
                if (distance < .1f) //(approachingFinalWaypoint && distanceToWaypoint < .1f)
                {
                    Debug.Log(Msg.BuildWatch(unit.transform.name, "Removing Destination"));

                    unit.Remove<Destination>();
                    break;
                }

                //unit.Get<Heading>() = heading;
                Draw.Cross(destination.value);
            }
        }
    }
}