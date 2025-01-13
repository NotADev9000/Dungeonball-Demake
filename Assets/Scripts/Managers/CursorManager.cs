using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameRestarted += LockCursor;
        UI_Menu.OnMenuOpened += OnMenuOpened;
        UI_Menu.OnMenuClosed += OnMenuClosed;
    }

    private void OnDisable()
    {
        UI_Menu.OnMenuOpened -= OnMenuOpened;
        UI_Menu.OnMenuClosed -= OnMenuClosed;

        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnGameRestarted -= LockCursor;
    }

    private void OnMenuOpened()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnMenuClosed()
    {
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
