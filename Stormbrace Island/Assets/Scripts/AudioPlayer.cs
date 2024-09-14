using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _relativeVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _relativeVolume = _audioSource.volume;

        AdjustVolume(AudioManager.Instance.MasterVolume);
        AudioManager.OnVolumeChange += AdjustVolume;
    }

    private void OnDestroy()
    {
        AudioManager.OnVolumeChange -= AdjustVolume;
    }

    private void AdjustVolume(float masterVolume)
    {
        _audioSource.volume = masterVolume * _relativeVolume;
    }
}
