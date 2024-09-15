using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField, Tooltip("Determines if this controller will effect the waves.")]
    private bool wavesProgressionEnabled;
    [SerializeField]
    private float startingWaveSpeed;
    [SerializeField]
    private float endingWaveSpeed;
    [SerializeField]
    private float startingWaveHeight;
    [SerializeField]
    private float endingWaveHeight;
    [SerializeField, Range(0f, 1f)]
    private float startingWaveSoundVolume;
    [SerializeField, Range(0f, 1f)]
    private float endingWaveSoundVolume;
    [SerializeField]
    private MeshRenderer waterRenderer;
    private Material _waterMaterial;
    [SerializeField]
    private AudioPlayer waterAudioPlayer;

    [Header("Sunlight")]
    [SerializeField, Tooltip("Determines if this controller will effect the sunlight.")]
    private bool sunlightProgressionEnabled;
    [SerializeField]
    private float startingTemperature;
    [SerializeField]
    private float endingTemperature;
    [SerializeField]
    private float startingIntensity;
    [SerializeField]
    private float endingIntensity;
    [SerializeField]
    private Light sunlightSource;

    [Header("Rain")]
    [SerializeField, Tooltip("Determines if this controller will effect the rain.")]
    private bool rainProgressionEnabled;
    [SerializeField, Range(0f, 1f)]
    private float rainStartTime;
    [SerializeField]
    private int startingRainRate;
    [SerializeField]
    private int endingRainRate;
    [SerializeField, Range(0f, 1f)]
    private float startingRainSoundVolume;
    [SerializeField, Range(0f, 1f)]
    private float endingRainSoundVolume;
    [SerializeField]
    private ParticleSystem rainParticleSystem;
    [SerializeField]
    private AudioPlayer rainAudioPlayer;
    private bool _rainStarted = false;
    private bool _rainVolumeFadedIn = false;

    [Header("Thunder & Lightning")]
    [SerializeField, Tooltip("Determines if thunder and lightning are enabled.")]
    private bool thunderAndLightningEnabled;
    [SerializeField, Range(0f, 1f)]
    private float lightningStartTime;
    [SerializeField]
    private float minTimeBetweenLightningStrikes;
    [SerializeField]
    private float maxTimeBetweenLightningStrikes;
    [SerializeField]
    private Animator lightningAnimator;
    [SerializeField]
    private List<AudioPlayer> thunderSounds;
    private float timeUntilNextLightningStrike = 0f;


    private GameTimer _gameTimer;

    private void Awake()
    {
        _gameTimer = FindAnyObjectByType<GameTimer>();
        _waterMaterial = waterRenderer.material;
    }

    private void Update()
    {
        UpdateWeatherEffects();
    }

    private void UpdateWeatherEffects()
    {
        float lerpValue = _gameTimer.SecondsElapsed / _gameTimer.StartingSeconds;

        if (wavesProgressionEnabled)
        {
            _waterMaterial.SetFloat("_WaveSpeed", Mathf.Lerp(startingWaveSpeed, endingWaveSpeed, lerpValue));
            _waterMaterial.SetFloat("_WaveHeight", Mathf.Lerp(startingWaveHeight, endingWaveHeight, lerpValue));
            waterAudioPlayer.RelativeVolume = Mathf.Lerp(startingWaveSoundVolume, endingWaveSoundVolume, lerpValue);
        }

        if (sunlightProgressionEnabled)
        {
            sunlightSource.colorTemperature = Mathf.Lerp(startingTemperature, endingTemperature, lerpValue);
            sunlightSource.intensity = Mathf.Lerp(startingIntensity, endingIntensity, lerpValue);
        }

        if (rainProgressionEnabled && rainStartTime < lerpValue)
        {
            float timeSinceRainStarted = _gameTimer.SecondsElapsed - (rainStartTime * _gameTimer.StartingSeconds);
            float totalRainTime = _gameTimer.StartingSeconds * (1 - rainStartTime);
            float rainLerpValue = timeSinceRainStarted / totalRainTime;

            var emission = rainParticleSystem.emission;
            emission.rateOverTime = (int)Mathf.Lerp(startingRainRate, endingRainRate, rainLerpValue);

            if (_rainVolumeFadedIn) rainAudioPlayer.RelativeVolume = Mathf.Lerp(startingRainSoundVolume, endingRainSoundVolume, rainLerpValue);

            if (!_rainStarted)
            {
                StartCoroutine(StartRain());
                _rainStarted = true;
            }
        }

        if (thunderAndLightningEnabled && lightningStartTime < lerpValue)
        {
            if (timeUntilNextLightningStrike <= 0f)
            {
                lightningAnimator.SetTrigger("LightningFlash");
                if (thunderSounds.Count > 0)
                {
                    int randomIndex = Random.Range(0, thunderSounds.Count);
                    thunderSounds[randomIndex].Play();
                }
                timeUntilNextLightningStrike = Random.Range(minTimeBetweenLightningStrikes, maxTimeBetweenLightningStrikes);
            }
            timeUntilNextLightningStrike -= Time.deltaTime;
        }
    }

    private IEnumerator StartRain()
    {
        rainParticleSystem.Play();

        rainAudioPlayer.RelativeVolume = 0f;
        rainAudioPlayer.Play();

        for (float volume = 0f; volume <= startingRainSoundVolume; volume += 0.01f)
        {
            rainAudioPlayer.RelativeVolume = volume;
            yield return new WaitForSeconds(0.1f);
        }

        _rainVolumeFadedIn = true;
    }
}
