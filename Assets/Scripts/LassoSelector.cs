using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LeTai.Selections
{
public class LassoSelector : ISelector
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
                    if (PointInPolygon.WindingNumber(vertex, vertices))
                        selectedVerticesCount++;
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
}
}
