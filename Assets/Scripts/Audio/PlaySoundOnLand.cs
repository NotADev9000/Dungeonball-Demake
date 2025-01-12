using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnLand : PlaySoundCooldown
{
    [SerializeField] private GroundSensor _groundSensor;

    private void OnEnable()
    {
        _groundSensor.OnLandedThisFrame += PlaySound;
    }

    private void OnDisable()
    {
        _groundSensor.OnLandedThisFrame -= PlaySound;
    }
}