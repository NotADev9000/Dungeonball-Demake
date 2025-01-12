using UnityEngine;

public class PlaySoundOnPickup : PlaySoundCooldown
{
    [SerializeField] private AmmoCarrier_LeftRight _carrier;

    private void OnEnable()
    {
        _carrier.OnPickup += PlaySound;
    }

    private void OnDisable()
    {
        _carrier.OnPickup -= PlaySound;
    }
}