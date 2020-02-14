using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public interface ISelectable
{
    Vector3              Center              { get; }
    IEnumerable<Vector3> Vertices            { get; }
    IEnumerable<Vector2> VerticesScreenSpace { get; }

    int VerticesSelectedThreshold { get; }

    void InvalidateScreenPosition(Func<Vector3, Vector2> worldToScreenPoint);

    void OnSelected();
    void OnDeselected();
}
}
