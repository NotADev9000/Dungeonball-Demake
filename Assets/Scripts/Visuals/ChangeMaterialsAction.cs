using System;
using UnityEngine;

[Serializable]
public class ChangeMaterialsAction : IAmActionable
{
    [SerializeField] private MaterialChanger _materialChanger;
    [SerializeField] private Material _material;

    public void Execute()
    {
        _materialChanger.ChangeMaterials(_material);
    }
}