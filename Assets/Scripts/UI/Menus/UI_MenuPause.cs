public class UI_MenuPause : UI_Menu
{
    private void OnEnable()
    {
        GameManager.Instance.OnGamePaused += OnPause;
        GameManager.Instance.OnGameResumed += OnResume;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnGamePaused -= OnPause;
        GameManager.Instance.OnGameResumed -= OnResume;
    }

    private void OnPause()
    {
        SetMenuState(true);
    }

    private void OnResume()
    {
        SetMenuState(false);
    }

    public void OnResumeButton()
    {
        GameManager.Instance.TogglePause();
    }

    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }
}
