using System;
using UnityEngine;

[Serializable]
public class ChangeLayerAction : IAmActionable
{
    [SerializeField] private GameObject _objectToChange;
    [SerializeField] private int _layer;

    public void Execute()
    {
        _objectToChange.layer = _layer;
    }
}