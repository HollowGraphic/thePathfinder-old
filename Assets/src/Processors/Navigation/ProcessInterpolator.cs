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
           
        }

        public void Tick(float delta)
        {
           
        }
    }
}