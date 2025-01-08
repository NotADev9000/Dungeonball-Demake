using System;
using UnityEngine;

[Serializable]
public class DestroyAction : IAmActionable
{
    [SerializeField] private ObjectDestroyer _objectDestroyer;
    [SerializeField] private float _timeUntilDestroy;

    public void Execute()
    {
        _objectDestroyer.DestroyInSeconds(_timeUntilDestroy);
    }
}