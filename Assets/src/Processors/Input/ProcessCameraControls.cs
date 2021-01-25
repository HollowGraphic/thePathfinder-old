using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using Unity.Mathematics;
using UnityEngine;

namespace ThePathfinder.Processors.Input
{
    public sealed class ProcessCameraControls : ProcessorInput, ITickLate
    {
        private readonly Group<GameCamera> _cameras = default;
        private readonly Camera _camera;

        public ProcessCameraControls(Camera camera)
        {
            _camera = camera;
        }
        public void TickLate(float delta)
        {
            foreach (var camera in _cameras)
            {
                //always get pan direction from buttons
                var horizontalPan = Player.GetAxis(ActionId.Camera_PanHorizontal);
                var verticalPan = Player.GetAxis(ActionId.Camera_PanVertical);

                var cCam = camera.GameCameraComponent();
                //INVESTIGATE we need a plane for camera controls. This plane will probably be used in other places, do we make it GLOBAL?

                var rotating = Player.GetButton(ActionId.Camera_Rotate);
                var cameraTransform = _camera.transform;
                if (rotating)
                {
                    //compute screen center
                    var screenCenter = new float2(Screen.width / 2, Screen.height / 2);
                    //a ray from center of screen
                    var ray = _camera.ScreenPointToRay(new float3(screenCenter.x, .01f, screenCenter.y));
                    //this should always hit our plane
                    cCam.Plane.Raycast(ray, out var center);
                    var rotationPoint = ray.GetPoint(center);

                    cameraTransform.RotateAround(rotationPoint, Vector3.up,
                        cCam.rotateSpeed * Mouse.screenPositionDelta.x * delta);
                }

                var zoomDir = Player.GetAxis(ActionId.Camera_Zoom);

                var width = Screen.width - cCam.edgeBuffer;
                var height = Screen.height - cCam.edgeBuffer;

                //get desired mouse pan only when we are not rotatin
                if (!rotating)
                {
                    if (Mouse.screenPosition.x > width)
                        horizontalPan = 1; //screen move right
                    if (Mouse.screenPosition.x < cCam.edgeBuffer)
                        horizontalPan = -1; // screen move left
                    if (Mouse.screenPosition.y > height)
                        verticalPan = 1; //screen move forward
                    if (Mouse.screenPosition.y < cCam.edgeBuffer)
                        verticalPan = -1; // screen move back
                }

                //pan camera horizontaly
                cameraTransform.Translate(new Vector3(horizontalPan, 0, 0) * cCam.panSpeed * delta, Space.Self);

                var localUnitForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
                cameraTransform.Translate(localUnitForward * verticalPan * cCam.panSpeed * delta, Space.World);

                //zoom camera
                cameraTransform.Translate(Vector3.forward * zoomDir * cCam.zoomSpeed * delta, Space.Self);
            }
        }

        protected override int SetCategoryId()
        {
            return Category.Camera;
        }
    }
}