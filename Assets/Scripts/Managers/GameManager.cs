using System;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    Restarting,
    GameOver
}

public class GameManager : MonoBehaviour
{
    // player ref
    [SerializeField] private Player_Combat _playerCombat;

    private GameState _gameState = GameState.Playing;
    public GameState GameState => _gameState;

    public static event Action OnGameOver;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    private void OnEnable()
    {
        _playerCombat.OnDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _playerCombat.OnDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player died");
        _gameState = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    public void TogglePause()
    {
        if (_gameState == GameState.GameOver || _gameState == GameState.Restarting)
            return;

        if (_gameState == GameState.Playing)
            PauseGame();
        else
            ResumeGame();
    }

    private void PauseGame()
    {
        _gameState = GameState.Paused;
        OnGamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        _gameState = GameState.Playing;
        OnGameResumed?.Invoke();
    }
}
