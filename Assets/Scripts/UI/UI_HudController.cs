using UnityEngine;

public class UI_HudController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_UI _inputReader;

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
        GameManager.Instance.TogglePause();
    }
}
