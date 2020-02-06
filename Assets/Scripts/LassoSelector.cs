using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class LassoSelector : Selector
{
    readonly List<Vector2> vertices = new List<Vector2>();
    readonly List<Line>    lines    = new List<Line>();

    public List<Line> Lines
    {
        get
        {
            if (lines.Count < vertices.Count)
            {
                int lastVertexIndex = vertices.Count - 1;
                lines.Add(new Line(vertices[lastVertexIndex - 1], vertices[lastVertexIndex]));
            }

            return lines;
        }
    }

    public void ExtendLasso(Vector2 newPoint)
    {
        vertices.Add(newPoint);
    }

    public void Reset()
    {
        vertices.Clear();
        lines.Clear();
    }

    public override int GetSelected(ref List<Selectable> result)
    {
        return 0;
    }
}
}
