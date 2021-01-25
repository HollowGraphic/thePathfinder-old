using System.ComponentModel;
using System.Diagnostics;
using Pixeye.Actors;
using ThePathfinder.Processors;
using ThePathfinder.Processors.AI;
using ThePathfinder.src.Processors.Abilities;
using UnityEngine;

namespace ThePathfinder
{
    public class LayerTestRoom : Layer<LayerTestRoom>
    {
        // Use to add processors and set up a layer.
        protected override void Setup()
        {
            Comparers.Add(new Float3Comparer());
            //^IMPORTANT all above this line
            Add<ProcessSelectionBox>();

            Add<ProcessPointerHover>();
            Add<ProcessSelectionConfirm>();
            Add<ProcessUnitSelected>();
            Add<ProcessAbiltityCommands>();
            Add<ProcessMovementOrders>();

            Add<ProcessDestinations>();
            Add<ProcessPathRequest>();
            Add<ProcessNavigator>();
            Add<ProcessEntityMovement>(); //TODO this should happen at the end of a frame?

            Add<ProcessCastAbility>();

            Add<ProcessVision>();
            Add<ProcessTargetAcquisition>();
            Add<ProcessNavigatorTarget>();
            ////////////////////////////////////////
            Add<ProcessInterpolator>();
        }

        // Use to clean up custom stuff before the layer gets destroyed.
        protected override void OnLayerDestroy()
        {
        }
    }
}