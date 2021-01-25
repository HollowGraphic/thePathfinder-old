using Rewired;
using UnityEngine;

namespace BigBiteStudios
{
    public static class MouseExtensions
    {
        private static RaycastHit _hit;

        /// <summary>
        /// Gets the world positiion of the mouse.
        /// <remarks>Based on Raycast</remarks>
        /// </summary>
        /// <param name="mouse"></param>
        /// <returns></returns>
        public static Vector3 GetWorldPosition(this Mouse mouse)
        {
            Physics.Raycast(GetRayFromCam(mouse.screenPosition), out _hit);
            return _hit.point;
        }

        public static Collider GetColliderUnderMouse(this Mouse mouse)
        {
            return Physics.Raycast(GetRayFromCam(mouse.screenPosition), out var _hit) ? _hit.collider : null;
        }

        public static Ray GetMouseRay(this Mouse mouse)
        {
            return GetRayFromCam(mouse.screenPosition);
        }

        internal static Ray GetRayFromCam(Vector2 mousePosition)
        {
            return Camera.main.ScreenPointToRay(mousePosition);
        }

        public static Vector3 GetMouseWorldPosOnScreen(this Mouse mouse)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(mouse.screenPosition.x, mouse.screenPosition.y, 1));
        }
    }
}