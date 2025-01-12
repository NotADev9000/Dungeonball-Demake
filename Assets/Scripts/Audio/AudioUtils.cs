using UnityEngine;

public static class AudioUtils
{
    public static void PlaySound(AudioSource source, AudioClip sound = null, float pitchMin = 1f, float pitchMax = 1f)
    {
        source.pitch = Random.Range(pitchMin, pitchMax);
        if (sound != null) source.PlayOneShot(sound);
        else source.Play();
    }
}