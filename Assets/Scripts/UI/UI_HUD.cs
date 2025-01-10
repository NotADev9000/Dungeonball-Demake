using UnityEngine;

public class UI_HUD : MonoBehaviour
{
    [SerializeField] protected Canvas _hudCanvas;

    private void OnEnable()
    {
        UI_Menu.OnMenuOpened += OnMenuOpened;
        UI_Menu.OnMenuClosed += OnMenuClosed;
    }

    private void OnDisable()
    {
        UI_Menu.OnMenuOpened -= OnMenuOpened;
        UI_Menu.OnMenuClosed -= OnMenuClosed;
    }

    private void OnMenuOpened()
    {
        SetHUDState(false);
    }

    private void OnMenuClosed()
    {
        SetHUDState(true);
    }

    private void SetHUDState(bool showHUD)
    {
        if (_hudCanvas != null)
        {
            _hudCanvas.gameObject.SetActive(showHUD);
            return;
        }

        Debug.LogWarning("No HUD canvas found on: " + gameObject.name);
    }
}