using UnityEngine;

public class PlaySoundOnLaunch : PlaySoundCooldown
{
    [SerializeField] private Launcher _launcher;

    private void OnEnable()
    {
        _launcher.OnLaunched += PlaySound;
    }

    private void OnDisable()
    {
        _launcher.OnLaunched -= PlaySound;
    }
}