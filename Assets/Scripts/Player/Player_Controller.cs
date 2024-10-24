using System;
using UnityEngine;

[RequireComponent(typeof(Player_Move), typeof(Player_Look), typeof(Thrower))]
public class Player_Controller : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    private Player_Move _movement;
    private Player_Look _look;
    private AmmoCarrier_LeftRight _ammoCarrier;
    private Thrower _thrower;

    private void Awake()
    {
        _movement = GetComponent<Player_Move>();
        _look = GetComponent<Player_Look>();
        _ammoCarrier = GetComponent<AmmoCarrier_LeftRight>();
        _thrower = GetComponent<Thrower>();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMovementInput;
        _inputReader.LookEvent += OnLookInput;
        _inputReader.LeftFireEvent += OnLeftFireInput;
        _inputReader.RightFireEvent += OnRightFireInput;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMovementInput;
        _inputReader.LookEvent -= OnLookInput;
        _inputReader.LeftFireEvent -= OnLeftFireInput;
        _inputReader.RightFireEvent -= OnRightFireInput;
    }

    private void OnMovementInput(Vector2 directionVector)
    {
        _movement.SetMoveDirection(directionVector);
    }

    private void OnLookInput(Vector2 lookVector)
    {
        _look.SetLookDirection(lookVector);
    }

    private void OnLeftFireInput()
    {
        OnFireInput(_ammoCarrier.LeftHold);
    }

    private void OnRightFireInput()
    {
        OnFireInput(_ammoCarrier.RightHold);
    }

    private void OnFireInput(AmmoCarryPoint ammoCarryPoint)
    {
        if (_ammoCarrier.TryGetAmmo(ammoCarryPoint, out Throwable throwable))
        {
            _thrower.Throw(throwable, GetAimOrigin(), GetAimDirection());
        }
    }

    private Vector3 GetAimOrigin()
    {
        return _look.CameraRoot.position;
    }

    private Vector3 GetAimDirection()
    {
        return _look.CameraRoot.forward;
    }
}
