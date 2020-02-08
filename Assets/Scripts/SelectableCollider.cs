using System;
using UnityEngine;

namespace LeTai.Selections
{
public class SelectableCollider : MonoBehaviour, ISelectable
{
    public Vector3   Center   => transform.position;
    public Vector3[] Vertices => vertices;

    public event Action selected;
    public event Action deselected;

    MeshCollider  meshCollider;
    BoxCollider[] boxColliders;
    Vector3[]     vertices = new Vector3[0];


    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        boxColliders = GetComponentsInChildren<BoxCollider>();

        int meshColliderVerticesCount = meshCollider ? meshCollider.sharedMesh.vertexCount : 0;
        int boxColliderVerticesCount  = boxColliders.Length > 0 ? 8 * boxColliders.Length : 0;

        vertices = new Vector3[meshColliderVerticesCount +
                               boxColliderVerticesCount];

        if (meshCollider)
            AddMeshColliderVertices(0);
        if (boxColliders.Length > 0)
            AddBoxColliderVertices(meshColliderVerticesCount);
    }

    void AddMeshColliderVertices(int startOffset)
    {
        for (var i = 0; i < meshCollider.sharedMesh.vertices.Length; i++)
        {
            vertices[startOffset + i] = transform.TransformPoint(meshCollider.sharedMesh.vertices[i]);
        }
    }

    static readonly Vector3[] BOX_VERTICES_OFFSET = {
        new Vector3(-.5f, -.5f, .5f),
        new Vector3(.5f,  -.5f, .5f),
        new Vector3(-.5f, -.5f, -.5f),
        new Vector3(.5f,  -.5f, -.5f),
        new Vector3(-.5f, .5f,  .5f),
        new Vector3(.5f,  .5f,  .5f),
        new Vector3(-.5f, .5f,  -.5f),
        new Vector3(.5f,  .5f,  -.5f)
    };

    void AddBoxColliderVertices(int startOffset)
    {
        for (var colliderIndex = 0; colliderIndex < boxColliders.Length; colliderIndex++)
        {
            var theBox = boxColliders[colliderIndex];
            for (var vOffsetIndex = 0; vOffsetIndex < BOX_VERTICES_OFFSET.Length; vOffsetIndex++)
            {
                var vOffset = theBox.size;
                vOffset.Scale(BOX_VERTICES_OFFSET[vOffsetIndex]);
                var vertexIndex = startOffset + colliderIndex * BOX_VERTICES_OFFSET.Length + vOffsetIndex;
                vertices[vertexIndex] = theBox.center + vOffset;
                vertices[vertexIndex] = theBox.transform.TransformPoint(vertices[vertexIndex]);
            }
        }
    }

    public void OnSelected()
    {
        selected?.Invoke();
    }

    public void OnDeselected()
    {
        deselected?.Invoke();
    }
}
}
