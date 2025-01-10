using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _defaultTimeScale = 1f;
    [SerializeField] private float _pauseTimeScale = 0f;

    private void OnEnable()
    {
        UI_Menu.OnMenuOpened += PauseTime;
        UI_Menu.OnMenuClosed += ResetTime;
    }

    private void OnDisable()
    {
        UI_Menu.OnMenuOpened -= PauseTime;
        UI_Menu.OnMenuClosed -= ResetTime;
    }

    private void PauseTime()
    {
        Time.timeScale = _pauseTimeScale;
    }

    private void ResetTime()
    {
        Time.timeScale = _defaultTimeScale;
    }
}