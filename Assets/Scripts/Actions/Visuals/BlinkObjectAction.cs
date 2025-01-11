using System;
using UnityEngine;

[Serializable]
public class BlinkObjectAction : IAmActionable
{
    [SerializeField] private ObjectBlinker _blinker;
    [SerializeField] private GameObject _objectToBlink;
    [SerializeField] private float _blinkDuration = 0f;

    public void Execute()
    {
        _blinker.Play(_objectToBlink, _blinkDuration);
    }
}