using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class LassoSelector : Selector
{
    List<Vector3> vertices = new List<Vector3>();
    List<Line>    lines    = new List<Line>();

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

    public void ExtendLasso(Vector3 newPoint)
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