public class UI_MenuGameWon : UI_Menu
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameWon += OnGameWon;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnGameWon -= OnGameWon;
    }

    private void OnGameWon()
    {
        SetMenuState(true);
    }

    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }
}
