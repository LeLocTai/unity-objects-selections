using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeTai.Selections
{
public class UGUILassoSelector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public SelectablesManager selectablesManager;
    public LineRenderer       lineRenderer;
    public Camera             rendererCamera;

    LassoSelector lassoSelector;

    List<ISelectable> selected = new List<ISelectable>();

    void Start()
    {
        rendererCamera = rendererCamera ? rendererCamera : Camera.main;
        lassoSelector  = new LassoSelector(ProjectToLasso);

        lineRenderer.positionCount = 0;
    }

    Vector2 ProjectToLasso(Vector3 posWS)
    {
        return rendererCamera.WorldToScreenPoint(posWS);
    }

    void ExtendLasso(Vector2 position)
    {
        lassoSelector.ExtendLasso(position);

        if (lineRenderer)
        {
            var posWS = rendererCamera.ScreenToWorldPoint(new Vector3(position.x, position.y,
                                                                      rendererCamera.nearClipPlane));
            lineRenderer.SetPosition(lineRenderer.positionCount++, posWS);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UnSelectAll();
        ExtendLasso(eventData.position);
    }

    void UnSelectAll()
    {
        foreach (var selectable in selected)
        {
            selectable.OnDeselected();
        }

        selected.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UnSelectAll();
        ExtendLasso(eventData.position);
        lassoSelector.GetSelected(selectablesManager.Selectables, ref selected);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        lassoSelector.Reset();

        if (lineRenderer)
        {
            lineRenderer.positionCount = 0;
        }
    }
}
}
