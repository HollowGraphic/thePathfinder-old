#define DebugBox
//#undef DebugBox
using BigBiteStudios;
using Pixeye.Actors;
using ThePathfinder.Components;
using ThePathfinder.Input;
using ThePathfinder.Processors.Input;
using Unity.Mathematics;
using UnityEngine;
#if UNITY_EDITOR
using Drawing;
#endif
namespace ThePathfinder.Processors.Selection
{
    public sealed class
        ProcessSelectionBox : ProcessorInput,
            ITick //, IReceive<StartBoxSelectionCommand>, IReceive<UpdateSelectionBoxCommand>, IReceive<CancelSelectingBoxCommand>
    {
        protected override int CategoryId => Category.Selection;

        //settings
        private const float NearDistance = 1f;
        private const float FarDistance = 1000; //how far can we box select?

        private const float BoxThreshold = 3;

        //selection box cache
        private readonly float3[] _boxPoints = new float3[8]; // selection box corners
        private readonly Plane[] _planes = new Plane[6]; // selection box planes

        private Vector2 _initialMousePos;

        //ecs
        private readonly Group<Selectable, PointerBounds> _selectables = default;
        private readonly Group<Commander> _commanders = default;

        protected override void OnDispose()
        {
        }

        public void Tick(float delta)
        {
            foreach (var commander in _commanders)
            {
                if (Player.GetButtonDown(ActionId.Selection_Confirm)) _initialMousePos = Mouse.screenPosition;
                if (Player.GetButton(ActionId.Selection_Confirm))
                {
                    if (!ConstructBoxFromMouse(_initialMousePos, Mouse.screenPosition)) return;
#if UNITY_EDITOR
                    DrawCustomBox(_boxPoints);
#endif
                    UpdateEntitiesInBounds(commander);
                }
            }
        }

        private void UpdateEntitiesInBounds(ent commander)
        {
            //check if any units are inside the box
            foreach (var selectable in _selectables)
                if (GeometryUtility.TestPlanesAABB(_planes, selectable.PointerBoundsComponent().value.bounds))
                {
                    if (!selectable.Has<SelectionCandidate>())
                        selectable.Get<SelectionCandidate>() = new SelectionCandidate(commander);
                }
                else if (selectable.Has<SelectionCandidate>())
                {
                    selectable.Remove<SelectionCandidate>();
                }
        }

        /// <summary>
        ///     Construct a selection box
        /// </summary>
        /// <param name="initialPos"></param>
        /// <param name="currentPos"></param>
        /// <returns>If it successfuly constructed a box</returns>
        private bool ConstructBoxFromMouse(float2 initialPos, float2 currentPos)
        {
            var minPos = math.min(initialPos, currentPos);
            var maxPos = math.max(initialPos, currentPos);
            var minX = maxPos.x - minPos.x;
            var minY = maxPos.y - minPos.y;

            var valid = minX > BoxThreshold && minY > BoxThreshold;
            if (!valid) return valid;

            _boxPoints[2] = new float3(maxPos.x, minPos.y, NearDistance);
            _boxPoints[0] = new float3(minPos.x, maxPos.y, NearDistance); //near plane
            _boxPoints[1] = new float3(maxPos.x, maxPos.y, NearDistance);
            _boxPoints[3] = new float3(minPos.x, minPos.y, NearDistance);

            //"extrude" our plane away from the camera by 'FarDistance', ending up with a complete box
            _boxPoints[4] = new float3(_boxPoints[0].x, _boxPoints[0].y, FarDistance); //far plane
            _boxPoints[5] = new float3(_boxPoints[1].x, _boxPoints[1].y, FarDistance);
            _boxPoints[6] = new float3(_boxPoints[2].x, _boxPoints[2].y, FarDistance);
            _boxPoints[7] = new float3(_boxPoints[3].x, _boxPoints[3].y, FarDistance);
            //return boxPoints;
            TransformBoxFromScreenToWorld(_boxPoints);

            return valid;
        }

        private void ConstrucPlanesFromPoints(float3[] points)
        {
            //todo: reverse winding
            var frontace = new[] {points[0], points[1], points[2]};
            var topFace = new[] {points[4], points[1], points[0]};
            var rightFace = new[] {points[6], points[2], points[5]};
            var leftFace = new[] {points[0], points[7], points[4]};
            var backFace = new[] {points[5], points[4], points[7]};
            var bottomFace = new[] {points[2], points[6], points[7]};
#if UNITY_EDITOR
            DrawPlane(Geometry.GetCentroidOfPoints(frontace), -Geometry.GetNormal(frontace));
            DrawPlane(Geometry.GetCentroidOfPoints(topFace), -Geometry.GetNormal(topFace));
            DrawPlane(Geometry.GetCentroidOfPoints(rightFace), -Geometry.GetNormal(rightFace));
            DrawPlane(Geometry.GetCentroidOfPoints(bottomFace), -Geometry.GetNormal(bottomFace));
            DrawPlane(Geometry.GetCentroidOfPoints(backFace), -Geometry.GetNormal(backFace));
            DrawPlane(Geometry.GetCentroidOfPoints(leftFace), -Geometry.GetNormal(leftFace));
#endif
            _planes[0].SetNormalAndPosition(-Geometry.GetNormal(topFace),
                Geometry.GetCentroidOfPoints(topFace));
            _planes[1].SetNormalAndPosition(-Geometry.GetNormal(rightFace),
                Geometry.GetCentroidOfPoints(rightFace));
            _planes[2].SetNormalAndPosition(-Geometry.GetNormal(bottomFace),
                Geometry.GetCentroidOfPoints(bottomFace));
            _planes[3].SetNormalAndPosition(-Geometry.GetNormal(backFace),
                Geometry.GetCentroidOfPoints(backFace));
            _planes[4].SetNormalAndPosition(-Geometry.GetNormal(leftFace),
                Geometry.GetCentroidOfPoints(leftFace));
            _planes[5].SetNormalAndPosition(-Geometry.GetNormal(frontace),
                Geometry.GetCentroidOfPoints(frontace));
        }

        private void TransformBoxFromScreenToWorld(float3[] points)
        {
            for (var i = 0; i < points.Length; i++) points[i] = Camera.main.ScreenToWorldPoint(_boxPoints[i]);

            ConstrucPlanesFromPoints(_boxPoints);
        }
#if UNITY_EDITOR
#pragma warning disable
        void DrawPlane(float3 position, float3 normal)
        {
            float3 v3;
            float size = Mathf.Max(position.x, position.y, position.z) / 2;
            if (!math.normalize(normal).Equals(new float3(0, 0, 1)))
                v3 = Vector3.Cross(normal, Vector3.forward).normalized * size;
            else
                v3 = Vector3.Cross(normal, Vector3.up).normalized * size;
            ;

            var corner0 = position + v3;
            var corner2 = position - v3;
            var q = Quaternion.AngleAxis(90.0f, normal);
            v3 = q * v3;
            var corner1 = position + v3;
            var corner3 = position - v3;
            // //X through center of plane
            Draw.Line(corner0, corner2, Color.green);
            Draw.Line(corner1, corner3, Color.green);
            //outside edges
            Draw.Line(corner0, corner1, Color.cyan);
            Draw.Line(corner1, corner2, Color.cyan);
            Draw.Line(corner2, corner3, Color.cyan);
            Draw.Line(corner3, corner0, Color.cyan);
            //Draw.Ray(position, normal*size, Color.red);
            Draw.Ray(position, position + (normal * size), Color.magenta);
        }

        private void DrawCustomBox(float3[] boxPoints)
        {
            // //draw initial side
            Draw.Line(boxPoints[4], boxPoints[7]);
            Draw.Line(boxPoints[0], boxPoints[3]);
            //draw plane boxPoints the other corner plane
            Draw.Line(boxPoints[0], boxPoints[4], Color.green);
            Draw.Line(boxPoints[1], boxPoints[5], Color.green);
            Draw.Line(boxPoints[2], boxPoints[6], Color.green);
            Draw.Line(boxPoints[3], boxPoints[7], Color.green);
            // Draw.Cross(Contexts.sharedInstance.game.gameCamera.Value.ScreenToWorldPoint(new Vector3(_initialPos.x, _initialPos.y, FarDistance)), 100,
            //     Color.yellow);
            //draw rest of plane sides
            for (int i = 0; i < 7; i++)
            {
                if (i < 3) //near plane
                {
                    Draw.CrossXZ(_boxPoints[i], 5, Color.red);
                    Draw.Line(_boxPoints[i], _boxPoints[i + 1]);
                }
                else if (i > 3)
                {
                    Draw.CrossXZ(_boxPoints[i], 10, Color.blue);
                    Draw.Line(_boxPoints[i], _boxPoints[i + 1]);
                }
            }
        }
#endif
    }
}