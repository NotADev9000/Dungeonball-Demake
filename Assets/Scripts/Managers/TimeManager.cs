using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float _defaultTimeScale = 1f;
    [SerializeField] private float _pauseTimeScale = 0f;

    private void OnEnable()
    {
        GameManager.OnGameOver += PauseTime;
        GameManager.OnGameResumed += ResetTime;
        GameManager.OnGamePaused += PauseTime;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= PauseTime;
        GameManager.OnGameResumed -= ResetTime;
        GameManager.OnGamePaused -= PauseTime;
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