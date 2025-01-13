public class UI_MenuTitle : UI_Menu
{
    private void OnEnable()
    {
        GameManager.Instance.OnFirstTimeInGame += OnFirstTimeInGame;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnFirstTimeInGame -= OnFirstTimeInGame;
    }

    private void OnFirstTimeInGame()
    {
        SetMenuState(true);
    }

    public void OnPlayButton()
    {
        GameManager.Instance.ResumeGame();
        SetMenuState(false);
    }
}
