using System;
using UnityEngine;
using Unity.Mathematics;

namespace BigBiteStudios
{
    public static class Geometry
    {
        public static Vector3 Plane3Intersect(Plane p1, Plane p2, Plane p3)
        {
            //get the intersection point of 3 planes
            return ((-p1.distance * Vector3.Cross(p2.normal, p3.normal)) +
                    (-p2.distance * Vector3.Cross(p3.normal, p1.normal)) +
                    (-p3.distance * Vector3.Cross(p1.normal, p2.normal))) /
                   (Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal)));
        }

        /// <summary>
        /// Get the normal to a triangle from the three corner points, a, b and c.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            // Find vectors corresponding to two of the sides of the triangle.
            Vector3 side1 = b - a;
            Vector3 side2 = c - a;

            // Cross the vectors to get a perpendicular vector, then normalize it.
            return Vector3.Cross(side1, side2).normalized;
        }

        public static float3 GetNormal(float3 a, float3 b, float3 c)
        {
            // Find vectors corresponding to two of the sides of the triangle.
            float3 side1 = b - a;
            float3 side2 = c - a;

            // Cross the vectors to get a perpendicular vector, then normalize it.
            return math.normalize(math.cross(side1, side2));
        }

        /// <summary>
        /// Get the normal to a triangle from the three corner points, a, b and c.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Vector3 GetNormal(Vector3[] points)
        {
            // Cross the vectors to get a perpendicular vector, then normalize it.
            return GetNormal(points[0], points[1], points[2]);
        }

        /// <summary>
        /// Get the normal to a triangle from the three corner points, a, b and c.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Vector3 GetNormal(float3[] points)
        {
            // Cross the vectors to get a perpendicular vector, then normalize it.
            return GetNormal(points[0], points[1], points[2]);
        }

        /// <summary>
        /// Get the center of group of points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static float3 GetCentroidOfPoints(float3[] points)
        {
            float3 centroid = float3.zero;

            for (int i = 0; i < 3; i++)
            {
                centroid += points[i];
            }

            return centroid / points.Length;
        }
    }

    public static class Direction
    {
        public static float3 GetDirection(Axis direction) => direction switch
        {
            Axis.Right => Vector3.right,
            Axis.Left => Vector3.left,
            Axis.Forward => Vector3.forward,
            Axis.Back => Vector3.back,
            _ => Vector3.zero
        };
    }

    public enum Axis
    {
        Left,
        Up,
        Right,
        Down,
        Forward,
        Back
    }
}