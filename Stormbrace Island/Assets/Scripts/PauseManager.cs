using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuCanvas;
    [SerializeField]
    private AudioListener audioListener;
    [SerializeField]
    private Slider volumeSlider;

    private bool _isPaused;
    private GameActions _gameActions;

    private void Awake()
    {
        _gameActions = new GameActions();
        volumeSlider.value = AudioManager.Instance.MasterVolume;
        volumeSlider.onValueChanged.AddListener(AudioManager.Instance.AdjustMasterVolume);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(AudioManager.Instance.AdjustMasterVolume);
    }

    private void OnEnable()
    {
        _gameActions.Menu.Enable();
        _gameActions.Menu.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        _gameActions.Menu.Pause.performed -= OnPause;
        _gameActions.Menu.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (_isPaused)
        {
            Time.timeScale = 1f;
            pauseMenuCanvas.SetActive(false);
            audioListener.enabled = true;
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenuCanvas.SetActive(true);
            audioListener.enabled = false;
            _isPaused = true;
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
