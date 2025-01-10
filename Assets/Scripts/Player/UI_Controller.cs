using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class UI_Controller : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_UI _inputReader;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        _inputReader.PauseEvent += OnPauseInput;
    }

    private void OnDisable()
    {
        _inputReader.PauseEvent -= OnPauseInput;
    }

    private void OnPauseInput()
    {
        _gameManager.TogglePause();
    }
}