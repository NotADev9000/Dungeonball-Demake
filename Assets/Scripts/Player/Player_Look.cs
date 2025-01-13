using UnityEngine;

public class Player_Look : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform _cameraRoot;
    public Transform CameraRoot => _cameraRoot;

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed = 5.0f;
    [SerializeField] private float _maxLookUpAngle = 90.0f;
    [SerializeField] private float _maxLookDownAngle = 90.0f;

    private Vector2 _input;

    private float _targetPitch;
    private float _rotationVelocity;

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    public void SetLookDirection(Vector2 lookVector)
    {
        _input = lookVector;
    }

    // Un-shamelessly stolen from the FPS Starter Asset "Look" method
    private void RotateCamera()
    {
        // if there is an input
        if (_input.sqrMagnitude >= 0.001f && Time.timeScale > 0.0f)
        {
            bool IsCurrentDeviceMouse = true;
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _targetPitch += _input.y * _rotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _input.x * _rotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _targetPitch = Mathf.Clamp(_targetPitch, -_maxLookUpAngle, _maxLookDownAngle);

            // Update Cinemachine camera target pitch
            _cameraRoot.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }
}
