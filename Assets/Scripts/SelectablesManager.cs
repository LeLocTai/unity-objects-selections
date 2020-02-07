using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeTai.Selections
{
public class SelectablesManager : MonoBehaviour
{
    List<ISelectable> selectables = new List<ISelectable>();

    public List<ISelectable> Selectables => selectables;

    void Start()
    {
        selectables.AddRange(FindObjectsOfType<SelectableCollider>());
    }
}
}
