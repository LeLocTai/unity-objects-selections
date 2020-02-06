using System.Collections;
using UnityEngine;

namespace LeTai.Selections
{
public class Selectable : MonoBehaviour
{
    public Vector3[] vertices;
    public Vector3   center;
}

public struct Line
{
    public Vector3 a, b;

    public Line(Vector3 a, Vector3 b)
    {
        this.a = a;
        this.b = b;
    }
}
}
