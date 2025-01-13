using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _music;

    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += Deactivate;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnGameOver -= Deactivate;
    }

    private void Start()
    {
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        int randomIndex = Random.Range(0, _music.Length);
        _musicSource.clip = _music[randomIndex];
        _musicSource.Play();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}