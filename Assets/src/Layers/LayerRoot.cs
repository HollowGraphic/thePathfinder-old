using Pixeye.Actors;
using ThePathfinder.Processors;

namespace ThePathfinder
{
  public class LayerRoot : Layer<LayerRoot>
  {
    // Use to add processors and set up a layer.
    protected override void Setup()
    {
      Add<ProcessCameraControls>();
    }

    // Use to clean up custom stuff before the layer gets destroyed.
    protected override void OnLayerDestroy()
    {
    }
  }
}
