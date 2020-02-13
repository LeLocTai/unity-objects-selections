using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeTai.Selections
{
public class UGUILassoSelector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] SelectablesManager selectablesManager;
    [SerializeField] Camera             rendererCamera;
    [SerializeField] LineRenderer       lineRenderer;

    [SerializeField] bool debug;

    LassoSelector lassoSelector;

    List<ISelectable> selected = new List<ISelectable>();

    void Start()
    {
        rendererCamera = rendererCamera ? rendererCamera : Camera.main;
        lassoSelector  = new LassoSelector();

        lineRenderer.positionCount = 0;
    }

    void OnGUI()
    {
        if (!debug) return;

        var style = new GUIStyle(GUI.skin.box);
        foreach (var vertex in lassoSelector.Vertices)
        {
            GUI.Box(new Rect(vertex.x, Screen.height - vertex.y, 1, 1), GUIContent.none, style);
        }

        foreach (var selectables in selectablesManager.Selectables)
        {
            foreach (var vertex in selectables.VerticesScreenSpace)
            {
                GUI.Box(new Rect(vertex.x, Screen.height - vertex.y, 1, 1), GUIContent.none, style);
            }
        }
    }

    void ExtendLasso(Vector2 position)
    {
        lassoSelector.ExtendLasso(position);

        if (lineRenderer)
        {
            var posWS = rendererCamera.ScreenToWorldPoint(new Vector3(position.x, position.y,
                                                                      rendererCamera.nearClipPlane + 1e-3f));
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
        lassoSelector.GetSelected(selectablesManager.Selectables, selected);
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
