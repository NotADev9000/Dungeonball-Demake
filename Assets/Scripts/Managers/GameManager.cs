using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused,
    Restarting,
    GameOver,
    Title
}

public class GameManager : MonoBehaviour
{
    // SINGLETON SETUP ////////////////////////////
    // Singleton Snippet From: https://discussions.unity.com/t/self-creating-singleton-from-a-prefab/590233/4
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance && !_isQuitting)
            {
                _instance = new GameObject().AddComponent<GameManager>();
                // name it for easy recognition
                _instance.name = _instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    ////////////////////////////////////////////////

    private Player_Combat _playerCombat;

    private GameState _gameState = GameState.Playing;
    public GameState GameState => _gameState;

    public event Action OnGameStarted;
    public event Action OnGameOver;
    public event Action OnGamePaused;
    public event Action OnGameResumed;
    public event Action OnGameRestarted;
    public event Action OnGameWon;

    public event Action OnFirstTimeInGame;

    private static bool _isQuitting;
    private static bool _isFirstTimeInGame = true;

    private void Awake()
    {
        // Prevents instances of GameManager from being created when quitting the application
        // (Not necessary for webgl but was annoying during testing)
        Application.quitting += () => _isQuitting = true;
    }

    private void Start()
    {
        StartGame();

        // setting up title screen (only showed on first time in game)
        if (_isFirstTimeInGame)
        {
            _isFirstTimeInGame = false;
            _gameState = GameState.Title;
            OnFirstTimeInGame?.Invoke();
        }
    }

    private void OnDisable()
    {
        _playerCombat.OnDeath -= OnPlayerDeath;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void StartGame()
    {
        SetupPlayerReference();
        _gameState = GameState.Playing;
        OnGameStarted?.Invoke();
    }

    private void SetupPlayerReference()
    {
        _playerCombat = FindObjectOfType<Player_Combat>();
        _playerCombat.OnDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        _gameState = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    public void TogglePause()
    {
        // if (_gameState == GameState.GameOver || _gameState == GameState.Restarting)
        //     return;

        if (_gameState == GameState.Playing)
            PauseGame();
        else if (_gameState == GameState.Paused)
            ResumeGame();
    }

    private void PauseGame()
    {
        _gameState = GameState.Paused;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        _gameState = GameState.Playing;
        OnGameResumed?.Invoke();
    }

    public void RestartGame()
    {
        if (_gameState == GameState.Restarting) return;

        _gameState = GameState.Restarting;
        TransitionToScene();
    }

    public void WinGame()
    {
        _gameState = GameState.GameOver;
        OnGameWon?.Invoke();
    }

    private void TransitionToScene()
    {
        // Start transition (will transition in) and load scene when transition ends
        UI_Transitioner.Instance.OnTransitionEnd += LoadScene;
        UI_Transitioner.Instance.BeginTransition();
    }

    private void LoadScene()
    {
        UI_Transitioner.Instance.OnTransitionEnd -= LoadScene;
        // Load scene (synchronous) and then start transition again (will transition out)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(StartLevel());
    }

    private IEnumerator StartLevel()
    {
        yield return null;
        UI_Transitioner.Instance.BeginTransition();
        OnGameRestarted?.Invoke();
        StartGame();
    }
}
