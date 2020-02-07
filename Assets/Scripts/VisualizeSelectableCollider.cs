using System;
using UnityEngine;

namespace LeTai.Selections
{
[RequireComponent(typeof(SelectableCollider))]
[RequireComponent(typeof(Renderer))]
public class VisualizeSelectableCollider : MonoBehaviour
{
    public Color selectedColor;
    public Color deselectedColor;
    void Start()
    {
        var selectableCollider = GetComponent<SelectableCollider>();
        var material           = GetComponent<Renderer>().material;

        selectableCollider.selected   += () => material.color = selectedColor;
        selectableCollider.deselected += () => material.color = deselectedColor;
    }
}
}
