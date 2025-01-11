public class UI_MenuGameOver : UI_Menu
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        SetMenuState(true);
    }

    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }
}
