using UnityEngine;
using UnityEngine.UI;

public class UI_CrosshairScaler : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Player_Controller _playerController;

    [Header("UI Elements")]
    [SerializeField] private Image _crosshair;

    [Header("Crosshair Settings")]
    [SerializeField] private float _crosshairDefaultScale = 1f;
    [SerializeField] private float _crosshairGrowScale = 2f;
    [Tooltip("Approximate time it takes for crosshair to change size")]
    [SerializeField] private float _crosshairSmoothTime = 0.3f;

    private float _crosshairTargetScale = 1f;
    private Vector3 _crosshairCurrentVelocity = Vector3.zero;

    private void Awake()
    {
        _crosshairTargetScale = _crosshairDefaultScale;
    }

    private void OnEnable()
    {
        _playerController.OnScannedForGrabbablesChange += UpdateCrosshairTargetScale;
    }

    private void OnDisable()
    {
        _playerController.OnScannedForGrabbablesChange -= UpdateCrosshairTargetScale;
    }

    private void Update()
    {
        UpdateCrosshairScale();
    }

    private void UpdateCrosshairTargetScale(bool targetHovered)
    {
        _crosshairTargetScale = targetHovered ? _crosshairGrowScale : _crosshairDefaultScale;
    }

    private void UpdateCrosshairScale()
    {
        if (_crosshair.transform.localScale == Vector3.one * _crosshairTargetScale) return;

        _crosshair.transform.localScale = Vector3.SmoothDamp(_crosshair.transform.localScale, _crosshairTargetScale * Vector3.one, ref _crosshairCurrentVelocity, _crosshairSmoothTime);
    }
}
