using Pixeye.Actors;
using ThePathfinder.Processors.Casting;
using ThePathfinder.Processors.Selection;
using ThePathfinder.Processors.Common;
using ThePathfinder.Processors.Input;
using ThePathfinder.Processors.Navigation;
using ThePathfinder.Processors.StatModding;
using ThePathfinder.Processors.Units;

namespace ThePathfinder.Layers
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
            Add<ProcessHealthModifier>();
            Add<ProcessDeadEntity>();//TODO end of frame?
        }

        // Use to clean up custom stuff before the layer gets destroyed.
        protected override void OnLayerDestroy()
        {
        }
    }
}