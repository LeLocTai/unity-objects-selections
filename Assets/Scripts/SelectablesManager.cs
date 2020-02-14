using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class SelectablesManager : MonoBehaviour
{
    public Camera selectionCamera;

    public List<ISelectable> Selectables => selectables;

    List<ISelectable>      selectables = new List<ISelectable>();
    Func<Vector3, Vector2> worldToScreenPointDelegate;

    Vector3    lastCameraPosition;
    Quaternion lastCameraRotation;

    void Start()
    {
        var selectableColliders = FindObjectsOfType<SelectableCollider>();

        worldToScreenPointDelegate = worldPos => selectionCamera.WorldToScreenPoint(worldPos);

        foreach (var selectableCollider in selectableColliders)
        {
            selectableCollider.Init(worldToScreenPointDelegate);
        }

        selectables.AddRange(selectableColliders);

        lastCameraPosition = selectionCamera.transform.position;
        lastCameraRotation = selectionCamera.transform.rotation;
    }

    void LateUpdate()
    {
        var cameraTransform = selectionCamera.transform;
        bool changed = false;

        if (cameraTransform.position != lastCameraPosition)
        {
            changed = true;
            lastCameraPosition = cameraTransform.position;
        }

        if (cameraTransform.rotation != lastCameraRotation)
        {
            changed = true;
            lastCameraRotation = cameraTransform.rotation;
        }

        if (changed)
        {
            foreach (var selectable in selectables)
            {
                selectable.InvalidateScreenPosition(worldToScreenPointDelegate);
            }
        }
    }
}
}
