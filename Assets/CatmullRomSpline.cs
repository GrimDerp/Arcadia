// https://github.com/SneakyBrian/Catmull-Rom-Sample/blob/master/Catmull%E2%80%93Rom-Sample/CatmullRomSpline.cs

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CatmullRomSpline {
    /// <summary>
    /// Generate spline series of points from array of keyframe points
    /// </summary>
    /// <param name="points">array of key frame points</param>
    /// <param name="numPoints">number of points to generate in spline between each point</param>
    /// <returns>array of points describing spline</returns>
    public static Vector3[] Generate(Vector3[] points, int numPoints)
    {
        if (points.Length < 4)
            throw new ArgumentException("CatmullRomSpline requires at least 4 points", "points");
        
        var splinePoints = new List<Vector3>();

        for (int i = 0; i < points.Length - 3; i++)
        {
            for (int j = 0; j < numPoints; j++)
            {
                splinePoints.Add(PointOnCurve(points[i], points[i + 1], points[i + 2], points[i + 3], (1f / numPoints) * j));
            }
        }

        splinePoints.Add(points[points.Length - 2]);

        return splinePoints.ToArray();
    }

    /// <summary>
    /// Calculates interpolated point between two points using Catmull-Rom Spline
    /// </summary>
    /// <remarks>
    /// Points calculated exist on the spline between points two and three.
    /// </remarks>
    /// <param name="p0">First Point</param>
    /// <param name="p1">Second Point</param>
    /// <param name="p2">Third Point</param>
    /// <param name="p3">Fourth Point</param>
    /// <param name="t">
    /// Normalised distance between second and third point 
    /// where the spline point will be calculated
    /// </param>
    /// <returns>
    /// Calculated Spline Point
    /// </returns>
    public static Vector3 PointOnCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 ret = new Vector3();

        float t2 = t * t;
        float t3 = t2 * t;

        ret.x = 0.5f * ((2.0f * p1.x) +
        (-p0.x + p2.x) * t +
        (2.0f * p0.x - 5.0f * p1.x + 4 * p2.x - p3.x) * t2 +
        (-p0.x + 3.0f * p1.x - 3.0f * p2.x + p3.x) * t3);

        ret.y = 0.5f * ((2.0f * p1.y) +
        (-p0.y + p2.y) * t +
        (2.0f * p0.y - 5.0f * p1.y + 4 * p2.y - p3.y) * t2 +
        (-p0.y + 3.0f * p1.y - 3.0f * p2.y + p3.y) * t3);

        ret.z = 0.5f * ((2.0f * p1.z) +
        (-p0.z + p2.z) * t +
        (2.0f * p0.z - 5.0f * p1.z + 4 * p2.z - p3.z) * t2 +
        (-p0.z + 3.0f * p1.z - 3.0f * p2.z + p3.z) * t3);

        return ret;
    }
}