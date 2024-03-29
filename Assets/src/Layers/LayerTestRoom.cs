﻿using System.Collections.Generic;
using Pixeye.Actors;
using ThePathfinder.Processors.Casting;
using ThePathfinder.Processors.Selection;
using ThePathfinder.Processors.Common;
using ThePathfinder.Processors.Input;
using ThePathfinder.Processors.Navigation;
using ThePathfinder.Processors.StatModding;
using ThePathfinder.Game;
using ThePathfinder.Processors.UnitCommand;

namespace ThePathfinder.Layers
{
    public class LayerTestRoom : Layer<LayerTestRoom>
    {
        // Use to add processors and set up a layer.
        protected override void Setup()
        {
            Comparers.Add(new Float3Comparer());
            //^IMPORTANT all above this line
            
            Add<ProcessDeadEntity>();
            Add<ProcessSelectionBox>();

            Add<ProcessPointerHover>();
            Add<ProcessSelectionConfirm>();
            Add<ProcessMovementOrders>();
            
            Add<ProcessOrderQueue>();
            Add<ProcessPathRequest>();
            Add<ProcessNavigator>();
            Add<ProcessEntityMovement>(); //Investigate this should happen at the end of a frame?

            Add<ProcessCastAbility>();

            Add<ProcessVision>();
            Add<ProcessTargetAcquisition>();
            Add<ProcessNavigatorTarget>();
            ////////////////////////////////////////
            Add<ProcessAttack>();
            Add<ProcessHealthModifier>();
        }

        // Use to clean up custom stuff before the layer gets destroyed.
        protected override void OnLayerDestroy()
        {
        }
    }
}