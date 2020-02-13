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
        var selectableColliders = FindObjectsOfType<SelectableCollider>();

        Func<Vector3, Vector2> worldToScreenPointDelegate = WorldToScreenPoint;
        foreach (var selectableCollider in selectableColliders)
        {
            selectableCollider.Init(worldToScreenPointDelegate);
        }

        selectables.AddRange(selectableColliders);
    }

    public Vector2 WorldToScreenPoint(Vector3 point)
    {
        return selectionCamera.WorldToScreenPoint(point);
    }
}
}
