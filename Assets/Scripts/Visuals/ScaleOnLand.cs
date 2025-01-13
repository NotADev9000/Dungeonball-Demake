using UnityEngine;

[RequireComponent(typeof(ScaleEaser))]
public class ScaleOnLand : MonoBehaviour
{
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _cooldown = 0.1f;

    private ScaleEaser _scaleEaser;

    private float _lastScaledTime;

    private void Awake()
    {
        _scaleEaser = GetComponent<ScaleEaser>();
    }
    private void OnEnable()
    {
        _groundSensor.OnLandedThisFrame += Scale;
    }

    private void OnDisable()
    {
        _groundSensor.OnLandedThisFrame -= Scale;
    }

    private void Scale()
    {
        // prevent scaling twice if landed in quick succession
        // (this usually happens to bouncy objects e.g. Slimes)
        if (Time.time - _lastScaledTime > _cooldown)
        {
            _scaleEaser.gameObject.transform.localScale = _targetScale;
            _scaleEaser.EaseScaleToInitial(_scaleSpeed);
            _lastScaledTime = Time.time;
        }
    }
}