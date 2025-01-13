using System;
using UnityEngine;

[Serializable]
public class StateScanForTarget : StateBase
{
    [SerializeField] private float _targetRange = 10f;
    public float TargetRange => _targetRange;

    private Transform _myTransform;
    private Transform _target;

    public event Action OnTargetInRange;

    public void SetTarget(Transform myTransform, Transform target)
    {
        _myTransform = myTransform;
        _target = target;
    }

    public override void Update()
    {
        // check if target is in range
        if (Vector3.Distance(_myTransform.position, _target.position) <= _targetRange)
        {
            OnTargetInRange?.Invoke();
        }
    }
}
