public class UI_MenuPause : UI_Menu
{
    private void OnEnable()
    {
        GameManager.OnGamePaused += OnPause;
        GameManager.OnGameResumed += OnResume;
    }

    private void OnDisable()
    {
        GameManager.OnGamePaused -= OnPause;
        GameManager.OnGameResumed += OnResume;
    }

    private void OnPause()
    {
        SetMenuState(true);
    }

    private void OnResume()
    {
        SetMenuState(false);
    }
}
