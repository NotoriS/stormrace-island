using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    public float RelativeVolume { get; set; }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        RelativeVolume = _audioSource.volume;

        AdjustVolume(AudioManager.Instance.MasterVolume);
        AudioManager.OnVolumeChange += AdjustVolume;
    }

    private void OnDestroy()
    {
        AudioManager.OnVolumeChange -= AdjustVolume;
    }

    private void AdjustVolume(float masterVolume)
    {
        _audioSource.volume = masterVolume * RelativeVolume;
    }

    public void Play()
    {
        _audioSource?.Play();
    }

    public void PlayOneShot()
    {
        _audioSource?.PlayOneShot(_audioSource.clip);
    }
}
