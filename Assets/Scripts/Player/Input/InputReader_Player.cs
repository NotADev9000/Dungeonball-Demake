using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Game_Controls;


[CreateAssetMenu(menuName = "InputReader_Player")]
public class InputReader_Player : ScriptableObject, IPlayerActions
{
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action LeftFireEvent;
    public event Action RightFireEvent;

    private Game_Controls _playerInput;

    private void OnEnable()
    {
        SetupInput();
        EnableInput();
        TimeManager.OnTimePaused += DisableInput;
        TimeManager.OnTimeResumed += EnableInput;
    }

    private void OnDisable()
    {
        DisableInput();
        TimeManager.OnTimePaused -= DisableInput;
        TimeManager.OnTimeResumed -= EnableInput;
    }

    private void SetupInput()
    {
        if (_playerInput == null)
        {
            _playerInput = new Game_Controls();
            _playerInput.Player.SetCallbacks(this);
        }
    }

    public void EnableInput()
    {
        _playerInput.Player.Enable();
    }

    public void DisableInput()
    {
        _playerInput.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLeftFire(InputAction.CallbackContext context)
    {
        OnFire(context, LeftFireEvent);
    }

    public void OnRightFire(InputAction.CallbackContext context)
    {
        OnFire(context, RightFireEvent);
    }

    private void OnFire(InputAction.CallbackContext context, Action Event)
    {
        if (context.started)
            Event?.Invoke();
    }
}
