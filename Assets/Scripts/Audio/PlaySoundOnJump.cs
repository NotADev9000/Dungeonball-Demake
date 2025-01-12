using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnJump : PlaySoundCooldown
{
    [SerializeField] private MovementJump _movementJump;

    private void OnEnable()
    {
        _movementJump.OnJump += PlaySound;
    }

    private void OnDisable()
    {
        _movementJump.OnJump -= PlaySound;
    }
}