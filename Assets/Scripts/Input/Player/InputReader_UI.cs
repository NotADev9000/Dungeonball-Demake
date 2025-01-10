using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Game_Controls;


[CreateAssetMenu(menuName = "InputReader_UI")]
public class InputReader_UI : ScriptableObject, IUIActions
{
    public event Action PauseEvent;

    private Game_Controls _uiInput;

    private void OnEnable()
    {
        SetupInput();
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void SetupInput()
    {
        if (_uiInput == null)
        {
            _uiInput = new Game_Controls();
            _uiInput.UI.SetCallbacks(this);
        }
    }

    private void EnableInput()
    {
        _uiInput.UI.Enable();
    }

    private void DisableInput()
    {
        _uiInput.UI.Disable();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            PauseEvent?.Invoke();
    }
}
