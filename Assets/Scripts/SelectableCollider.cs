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

    MeshCollider meshCollider;
    BoxCollider  boxCollider;
    Vector3[]    vertices = new Vector3[0];


    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        boxCollider  = GetComponent<BoxCollider>();

        int meshColliderVerticesCount = meshCollider ? meshCollider.sharedMesh.vertexCount : 0;
        int boxColliderVerticesCount  = boxCollider ? 8 : 0;

        vertices = new Vector3[meshColliderVerticesCount +
                               boxColliderVerticesCount];

        AddMeshColliderVertices(0);
        AddBoxColliderVertices(meshColliderVerticesCount);
    }

    void AddMeshColliderVertices(int offset)
    {
        for (var i = 0; i < meshCollider.sharedMesh.vertices.Length; i++)
        {
            vertices[i + offset] = transform.TransformPoint(meshCollider.sharedMesh.vertices[i]);
        }
    }

    void AddBoxColliderVertices(int offset)
    {
        //TODO
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
