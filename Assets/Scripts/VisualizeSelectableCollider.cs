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

    new Renderer          renderer;
    MaterialPropertyBlock selectedBlock;
    MaterialPropertyBlock deselectedBlock;

    void Start()
    {
        var selectableCollider = GetComponent<SelectableCollider>();
        renderer = GetComponent<Renderer>();

        selectedBlock   = new MaterialPropertyBlock();
        deselectedBlock = new MaterialPropertyBlock();

        selectedBlock.SetColor("_Color", selectedColor);
        deselectedBlock.SetColor("_Color", deselectedColor);

        selectableCollider.selected   += () => renderer.SetPropertyBlock(selectedBlock);
        selectableCollider.deselected += () => renderer.SetPropertyBlock(deselectedBlock);
    }
}
}
