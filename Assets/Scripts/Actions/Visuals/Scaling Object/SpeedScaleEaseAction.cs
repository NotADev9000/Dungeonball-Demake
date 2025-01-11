using System;
using UnityEngine;

/// <summary>
/// Sets object to target scale, then eases back to original scale.
/// Target scale depends on speed of object.
/// </summary>
[Serializable]
public class SpeedScaleEaseAction : ScaleEaseAction
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _minSpeedThreshold;
    [SerializeField] private float _maxSpeedThreshold;
    [SerializeField] private Vector3 _maxScale;

    public override void Execute()
    {
        float speedRatio = Mathf.InverseLerp(_minSpeedThreshold, _maxSpeedThreshold, _rigidbody.velocity.magnitude);
        Vector3 targetScale = Vector3.Lerp(Vector3.one, _maxScale, speedRatio);
        _scaleEaser.gameObject.transform.localScale = targetScale;
        _scaleEaser.EaseScaleToInitial(_scaleSpeed);
    }
}