using System;
using UnityEngine;

public abstract class UI_Menu : MonoBehaviour
{
    [SerializeField] protected Canvas _menuCanvas;

    public static event Action OnMenuOpened;
    public static event Action OnMenuClosed;

    protected void SetMenuState(bool showMenu)
    {
        if (_menuCanvas != null)
        {
            _menuCanvas.gameObject.SetActive(showMenu);
            if (showMenu)
                OnMenuOpened?.Invoke();
            else
                OnMenuClosed?.Invoke();
            return;
        }

        Debug.LogWarning("No menu canvas found on: " + gameObject.name);
    }
}