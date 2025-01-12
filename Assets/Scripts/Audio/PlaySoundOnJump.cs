using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnJump : PlaySoundCooldown
{
    [SerializeField] private MovementJump _movementJump;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _movementJump.OnJump += PlaySound;
    }

    private void OnDisable()
    {
        _movementJump.OnJump -= PlaySound;
    }
}