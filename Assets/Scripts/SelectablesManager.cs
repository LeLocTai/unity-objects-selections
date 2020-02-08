using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class SelectablesManager : MonoBehaviour
{
    public Camera selectionCamera;

    public List<ISelectable> Selectables => selectables;

    List<ISelectable> selectables = new List<ISelectable>();


    void Start()
    {
        var selectable = FindObjectsOfType<SelectableCollider>();
        for (var i = 0; i < selectable.Length; i++)
        {
            selectable[i].Init(this);
        }

        selectables.AddRange(selectable);
    }

    public Vector2 WorldToScreenPoint(Vector3 point)
    {
        return selectionCamera.WorldToScreenPoint(point);
    }
}
}
