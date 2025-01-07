using System;
using UnityEngine;

[Serializable]
public class FlashMaterialsAction : IAmEndableAction
{
    [SerializeField] private MaterialFlasher _materialFlasher;
    [SerializeField] private MaterialChanger _materialChanger;
    [SerializeField] private MaterialFlashData_SO _flashData;

    public event Action OnActionEnded;

    public void Execute()
    {
        _materialFlasher.Play(_flashData, _materialChanger, ActionEnded);
    }

    public void ActionEnded()
    {
        OnActionEnded?.Invoke();
    }
}
