using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float masterVolume = 0.5f;

    private float _previousFrameMasterVolume;

    public float MasterVolume => masterVolume;

    public delegate void VolumeChanged(float masterVolume);
    public static event VolumeChanged OnVolumeChange;

    private void Start()
    {
        OnVolumeChange?.Invoke(masterVolume);
        _previousFrameMasterVolume = masterVolume;
    }

    private void Update()
    {
        if (_previousFrameMasterVolume != masterVolume)
        {
            OnVolumeChange?.Invoke(masterVolume);
        }
        _previousFrameMasterVolume = masterVolume;
    }

    public void AdjustMasterVolume(float volume)
    {
        masterVolume = volume;
    }
}
