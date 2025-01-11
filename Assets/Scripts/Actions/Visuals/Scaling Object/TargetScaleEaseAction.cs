using System;
using UnityEngine;

/// <summary>
/// Sets object to target scale, then eases back to original scale.
/// </summary>
[Serializable]
public class TargetScaleEaseAction : ScaleEaseAction
{
    [SerializeField] private Vector3 _targetScale;

    public override void Execute()
    {
        _scaleEaser.gameObject.transform.localScale = _targetScale;
        _scaleEaser.EaseScaleToInitial(_scaleSpeed);
    }
}
