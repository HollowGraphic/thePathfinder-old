using BigBiteStudios.Logging;
using Drawing;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Game;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Navigation
{
    /// <summary>
    /// Process navigators with navigation path
    /// </summary>
    public sealed class ProcessNavigator : Processor, ITick
    {
        /// <summary>
        ///How far to look for waypoints?
        /// </summary>
        private const float NEXT_WAYPOINT_DISTANCE = .5f;
        private readonly Group<Navigator, Destination, VectorPath> _navigators = default;
        
        public void Tick(float delta)
        {
            foreach (ent unit in _navigators)
            {
                Debug.Log(Msg.BuildWatch("# of Nodes", unit.VectorPathComponent().value.Count.ToString()));
                foreach (Vector3 node in unit.VectorPathComponent().value) Draw.CircleXZ(node, .25f, Color.blue);

                // if (Time.Current >  cNavigator.lastRepath + cNavigator.repathRate)
                // {
                //     cNavigator.lastRepath = Time.Current;
                //     //path request
                //     navigator.Get<PathRequest>();
                //     continue;
                // }
                float distanceToWaypoint;
                Heading heading;
                Destination destination = unit.DestinationComponent();
                bool approachingFinalWaypoint = false;

                VectorPath cVecPath = unit.VectorPathComponent();

                Navigator cNavigator = unit.NavigatorComponent();
                Debug.Log(Msg.BuildWatch("Current Path Node",
                    cNavigator.currentPathNode.ToString()));
                // Check in a loop if we are close enough to the current waypoint to switch to the next one.
                // We do this in a loop because many waypoints might be close to each other and we may reach
                // several of them in the same frame.
                while (true)
                {
                    // The distance to the next waypoint in the path
                    distanceToWaypoint = math.distancesq(unit.transform.position,
                        cVecPath.value[cNavigator.currentPathNode]);
                    //navigating vector path
                    if (distanceToWaypoint < NEXT_WAYPOINT_DISTANCE) //approaching next waypoint
                    {
                        Debug.Log(Msg.BuildWatch(unit.transform.name, "Close To NextWayPoint"));

                        // Check if there is another waypoint or if we have reached the end of the vector path
                        if (cNavigator.currentPathNode + 1 < cVecPath.value.Count)
                        {
                            Debug.Log(Msg.BuildWatch(unit.transform.name, "Incrementing"));

                            cNavigator.currentPathNode++;
                        }
                        //must be on last waypoint
                        else
                        {
                            // Set a status variable to indicate that the agent has reached the end of the path.
                            // You can use this to trigger some special code if your game requires that.
                            Debug.Log(Msg.BuildWatch(unit.transform.name, "Reached end of Path"));
                            approachingFinalWaypoint = true;
                            break;
                        }
                    }
                    else //not close enough yet, bump out 
                    {
                        break;
                    }
                }

                // Direction to the next waypoint
                // Normalize it so that it has a length of 1 world unit
                heading = new Heading(math.normalize(cVecPath.value[cNavigator.currentPathNode] -
                                                     unit.transform.position));

                float distance = math.distance(unit.transform.position, destination.location);

                // Slow down smoothly upon approaching the end of the path and there are no more destinations queued
                // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
                unit.Get<SpeedMod>().value = distance < cNavigator.breakingDistance &&
                                             unit.UnitOrderQueueInfoComponent().nextOrderType == OrderType.Default
                    ? math.sqrt(distance / cNavigator.breakingDistance)
                    : 1f;

                //Debug.Log(Msg.BuildWatch("Distance To Waypoint", distanceToWaypoint.ToString()));
                //we have reached our destination
                if (approachingFinalWaypoint && distanceToWaypoint < .1f)
                {
                    Debug.Log(Msg.BuildWatch(unit.transform.name, "Arrived"));
                    unit.Remove<Destination>();
                    break;
                }

                unit.NavigatorComponent() = cNavigator;
                unit.Get<Heading>() = heading;
                Draw.Cross(destination.location);
            }
        }
    }
}