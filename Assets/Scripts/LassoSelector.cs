using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LeTai.Selections
{
public class LassoSelector : Selector
{
    readonly List<Vector2> vertices = new List<Vector2>();

    public List<Vector2> Vertices => vertices;

    public void ExtendLasso(Vector2 newPoint)
    {
        Vertices.Add(newPoint);
    }

    public void Reset()
    {
        Vertices.Clear();
    }

    readonly ConcurrentBag<ISelectable> selectedBag = new ConcurrentBag<ISelectable>();

    public int GetSelected(IEnumerable<ISelectable> selectables, ICollection<ISelectable> result)
    {
        while (selectedBag.TryTake(out _)) { }

        Parallel.ForEach(
            selectables,
            selectable =>
            {
                int selectedVerticesCount = 0;
                foreach (var vertex in selectable.VerticesScreenSpace)
                {
                    if (IsPointInLasso(vertex)) selectedVerticesCount++;
                }

                if (selectedVerticesCount < selectable.VerticesSelectedThreshold) return;

                selectedBag.Add(selectable);
            }
        );

        int selectedCount = 0;
        foreach (var selectable in selectedBag)
        {
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
