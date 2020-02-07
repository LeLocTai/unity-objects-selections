using System;
using UnityEngine;

namespace LeTai.Selections
{
[RequireComponent(typeof(SelectableCollider))]
[RequireComponent(typeof(Renderer))]
public class VisualizeSelectableCollider : MonoBehaviour
{
    void Start()
    {
        var selectableCollider = GetComponent<SelectableCollider>();
        var material           = GetComponent<Renderer>().material;

        selectableCollider.selected   += () => material.color = Color.green;
        selectableCollider.deselected += () => material.color = Color.white;
    }
}
}
