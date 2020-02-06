using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class LassoSelector : Selector
{
    readonly List<Vector2> vertices = new List<Vector2>();
    readonly List<Line>    lines    = new List<Line>();

    public List<Vector2> Vertices => vertices;
    public List<Line>    Lines    => lines;

    public void ExtendLasso(Vector2 newPoint)
    {
        Vertices.Add(newPoint);
        if (Vertices.Count > 1)
        {
            int lastVertexIndex = Vertices.Count - 1;
            lines.Add(new Line(Vertices[lastVertexIndex - 1], Vertices[lastVertexIndex]));
        }
    }

    public void Reset()
    {
        Vertices.Clear();
        lines.Clear();
    }

    public override int GetSelected(ref List<Selectable> result)
    {
        return 0;
    }
}
}
