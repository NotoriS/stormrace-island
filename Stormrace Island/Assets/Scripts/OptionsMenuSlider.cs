using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = AudioManager.Instance.MasterVolume;
        volumeSlider.onValueChanged.AddListener(AudioManager.Instance.AdjustMasterVolume);
    }
}
