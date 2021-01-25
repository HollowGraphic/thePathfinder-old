using Pixeye.Actors;
using UnityEngine;
using ThePathfinder.Components;
using ThePathfinder.Input;
using Unity.Mathematics;

namespace ThePathfinder.Processors
{
    public sealed class ProcessCameraControls : ProcessorInput, ITickLate
    {
        private readonly Group<GameCamera> _cameras = default;

        public void TickLate(float delta)
        {
            foreach (var camera in _cameras)
            {
                //always get pan direction from buttons
                float horizontalPan = player.GetAxis(ActionId.Camera_PanHorizontal);
                float verticalPan = player.GetAxis(ActionId.Camera_PanVertical);

                var cCam = camera.GameCameraComponent();
                var main = Camera.main;

                //INVESTIGATE we need a plane for camera controls. This plane will probably be used in other places, do we make it GLOBAL?

                bool rotating = player.GetButton(ActionId.Camera_Rotate);
                if (rotating)
                {
                    //compute screen center
                    var screenCenter = new float2(Screen.width / 2, Screen.height / 2);
                    //a ray from center of screen
                    Ray ray = main.ScreenPointToRay(new float3(screenCenter.x, .01f, screenCenter.y));
                    //this should always hit our plane
                    cCam.plane.Raycast(ray, out var center);
                    var rotationPoint = ray.GetPoint(center);

                    main.transform.RotateAround(rotationPoint, Vector3.up,
                        cCam.rotateSpeed * mouse.screenPositionDelta.x * delta);
                }

                var zoomDir = player.GetAxis(ActionId.Camera_Zoom);

                float width = Screen.width - cCam.edgeBuffer;
                float height = Screen.height - cCam.edgeBuffer;

                //get desired mouse pan only when we are not rotatin
                if (!rotating)
                {
                    if (mouse.screenPosition.x > width)
                        horizontalPan = 1; //screen move right
                    if (mouse.screenPosition.x < cCam.edgeBuffer)
                        horizontalPan = -1; // screen move left
                    if (mouse.screenPosition.y > height)
                        verticalPan = 1; //screen move forward
                    if (mouse.screenPosition.y < cCam.edgeBuffer)
                        verticalPan = -1; // screen move back
                }

                //pan camera horizontaly
                main.transform.Translate(new Vector3(horizontalPan, 0, 0) * cCam.panSpeed * delta, Space.Self);

                var localUnitForward = new Vector3(main.transform.forward.x, 0, main.transform.forward.z).normalized;
                main.transform.Translate(localUnitForward * verticalPan * cCam.panSpeed * delta, Space.World);

                //zoom camera
                main.transform.Translate(Vector3.forward * zoomDir * cCam.zoomSpeed * delta, Space.Self);
            }
        }

        protected override int SetCategoryId()
        {
            return Category.Camera;
        }
    }
}