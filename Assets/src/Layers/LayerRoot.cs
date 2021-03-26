using Pixeye.Actors;
using ThePathfinder.Processors.Input;
using UnityEngine;

namespace ThePathfinder.Layers
{
    public class LayerRoot : Layer<LayerRoot>
    {
        // Use to add processors and set up a layer.
        public Camera gameCamera;
        protected override void Setup()
        {
            Add(new ProcessCameraControls(gameCamera));
        }

        // Use to clean up custom stuff before the layer gets destroyed.
        protected override void OnLayerDestroy()
        {
        }
    }
}