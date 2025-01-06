using System;
using UnityEngine;

[Serializable]
public class FlashMaterialsAction : IActions
{
    [SerializeField] private MaterialFlasher _materialFlasher;
    [SerializeField] private MaterialChanger _materialChanger;
    [SerializeField] private VFXFlashData_SO _flashData;

    public void Execute()
    {
        _materialFlasher.Play(_flashData, _materialChanger);
    }
}
