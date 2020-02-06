using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeTai.Selections
{
public class UGUILassoSelector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public LineRenderer lineRenderer;
    public Camera       rendererCamera;

    LassoSelector lassoSelector;

    void Start()
    {
        lassoSelector  = new LassoSelector();
        rendererCamera = rendererCamera ? rendererCamera : Camera.main;

        lineRenderer.positionCount = 0;
    }

    void ExtendLasso(Vector2 position)
    {
        var posWS = rendererCamera.ScreenToWorldPoint(new Vector3(
                                                          position.x,
                                                          position.y,
                                                          rendererCamera.nearClipPlane
                                                      ));

        lassoSelector.ExtendLasso(posWS);

        if (lineRenderer)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount++, posWS);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ExtendLasso(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ExtendLasso(eventData.position);
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
