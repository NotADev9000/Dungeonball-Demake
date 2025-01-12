using System;
using UnityEngine;

[Serializable]
public class PlaySoundAction : IAmActionable
{
    [SerializeField] private PlaySoundCooldown _playSoundCooldown;

    public void Execute()
    {
        _playSoundCooldown.PlaySound();
    }
}