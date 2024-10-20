using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    [SerializeField] private GameObject _cameraRoot;

    [SerializeField] private float _rotationSpeed = 5.0f;

    private Vector2 _input;

    private float _targetPitch;
    private float _rotationVelocity;

    private void OnEnable()
    {
        _inputReader.LookEvent += OnLook;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnLook(Vector2 lookVector)
    {
        _input = lookVector;
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        // if there is an input
        if (_input.sqrMagnitude >= 0.001f)
        {
            bool IsCurrentDeviceMouse = true;
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _targetPitch += _input.y * _rotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _input.x * _rotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _targetPitch = Mathf.Clamp(_targetPitch, -90, 90);

            // Update Cinemachine camera target pitch
            _cameraRoot.transform.localRotation = Quaternion.Euler(_targetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }
}
