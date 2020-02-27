using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public static class PointInPolygon
{
    /// <summary>
    /// Optimized Winding Number test from http://geomalgorithms.com/a03-_inclusion.html
    /// </summary>
    public static bool WindingNumber(Vector2 point, IList<Vector2> polygon)
    {
        int vCount = polygon.Count;
        if (vCount < 2) return point == polygon[0];

        int wn = 0;
        for (int i = 1, j = vCount - 1; i < vCount; j = i++)
        {
            var a = polygon[j];
            var b = polygon[i];

            if (b.y <= point.y)
            {
                // start y <= P.y
                if (polygon[j].y > point.y)      // an upward crossing
                    if (IsLeft(b, a, point) > 0) // P left of  edge
                        ++wn;                    // have  a valid up intersect
            }
            else
            {
                // start y > P.y (no test needed)
                if (a.y <= point.y)              // a downward crossing
                    if (IsLeft(b, a, point) < 0) // P right of  edge
                        --wn;                    // have  a valid down intersect
            }
        }

        return wn != 0;
    }

    /// <summary>
    /// tests if point is Left|On|Right of an infinite line ab
    /// From http://geomalgorithms.com/a03-_inclusion.html
    /// </summary>
    /// <returns>
    /// &gt; 0 for point left of the line through a and b<br/>
    /// = 0 for point on the line<br/>
    /// &lt; 0 for point right of the line<br/>
    /// </returns>
    static int IsLeft(Vector2 a, Vector2 b, Vector2 point)
    {
        return (int) ((b.x - a.x) * (point.y - a.y)
                    - (point.x - a.x) * (b.y - a.y));
    }

    public static bool CrossingNumber(Vector2 point, IList<Vector2> polygon)
    {
        int vCount = polygon.Count;
        if (vCount < 2) return point == polygon[0];

        bool inside = false;
        // Assigning both indexes at the same time. Unnecessarily clever.
        for (int i = 1, j = vCount - 1; i < vCount; j = i++)
        {
            var a = polygon[j];
            var b = polygon[i];

            // Micro optimization: might avoid 1 branch. But mostly look neat.
            // XOR operator used to toggle the boolean
            // Starting from false, odd number of toggles will return true
            inside ^= IntersectLeft(point, a, b);
        }

        return inside;
    }

    static bool IntersectLeft(Vector2 point, Vector2 a, Vector2 b)
    {
        return IsBetween(point.y, a.y, b.y) &&
               LineIntersectionX(point.y, a, b) < point.x;
    }

    static bool IsBetween(float value, float a, float b)
    {
        return a > value != b > value;
    }

    static float LineIntersectionX(float lineY, Vector2 a, Vector2 b)
    {
        return (a.x - b.x) * (lineY - b.y) / (a.y - b.y) + b.x;
    }
}
}
