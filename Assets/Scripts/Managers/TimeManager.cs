using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _defaultTimeScale = 1f;
    [SerializeField] private float _pauseTimeScale = 0f;

    public static event Action OnTimePaused;
    public static event Action OnTimeResumed;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStarted += ResetTime;
        UI_Menu.OnMenuOpened += PauseTime;
        UI_Menu.OnMenuClosed += ResetTime;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStarted -= ResetTime;
        UI_Menu.OnMenuOpened -= PauseTime;
        UI_Menu.OnMenuClosed -= ResetTime;
    }

    private void PauseTime()
    {
        Time.timeScale = _pauseTimeScale;
        OnTimePaused?.Invoke();
    }

    private void ResetTime()
    {
        Time.timeScale = _defaultTimeScale;
        OnTimeResumed?.Invoke();
    }
}
