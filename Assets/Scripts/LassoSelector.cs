using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class LassoSelector : Selector
{
    readonly List<Vector2> vertices = new List<Vector2>();

    public List<Vector2> Vertices => vertices;

    readonly Func<Vector3, Vector2> projectToLasso;

    public LassoSelector(Func<Vector3, Vector2> projectToLasso)
    {
        this.projectToLasso = projectToLasso;
    }

    public void ExtendLasso(Vector2 newPoint)
    {
        Vertices.Add(newPoint);
    }

    public void Reset()
    {
        Vertices.Clear();
    }

    public override int GetSelected(IEnumerable<ISelectable> selectables, ref List<ISelectable> result)
    {
        int selectedCount = 0;
        foreach (var selectable in selectables)
        {
            int selectedVerticesCount = 0;
            foreach (var vertex in selectable.Vertices)
            {
                if (IsPointInLasso(projectToLasso.Invoke(vertex)))
                {
                    selectedVerticesCount++;
                }
            }

            if (selectedVerticesCount < selectable.Vertices.Length / 2f) continue;

            selectable.OnSelected();
            result.Add(selectable);
            selectedCount++;
        }

        return selectedCount;
    }

    /// <summary>
    /// https://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
    /// </summary>
    bool IsPointInLasso(Vector2 point)
    {
        int vCount = vertices.Count;
        if (vertices.Count < 2) return point == vertices[0];

        bool inside = false;
        for (int i = 1, j = vCount - 1; i < vCount; j = i++)
        {
            var a = vertices[j];
            var b = vertices[i];

            inside ^= IsBetween(point.y, a.y, b.y) &&
                      point.x < (a.x - b.x) * (point.y - b.y) / (a.y - b.y) + b.x;
        }

        return inside;
    }

    static bool IsBetween(float value, float a, float b)
    {
        bool aAbovePoint = a > value;
        bool bAbovePoint = b > value;
        return aAbovePoint != bAbovePoint;
    }
}
}
