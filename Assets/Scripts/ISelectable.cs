using System;
using UnityEngine;

namespace LeTai.Selections
{
public interface ISelectable
{
    Vector3   Center              { get; }
    Vector3[] Vertices            { get; }
    Vector2[] VerticesScreenSpace { get; }

    event Action selected;
    event Action deselected;

    void OnSelected();
    void OnDeselected();
}
}
