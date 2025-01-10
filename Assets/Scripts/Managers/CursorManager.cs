using UnityEngine;

public class CursorManager : MonoBehaviour
{
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnMenuClosed()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
