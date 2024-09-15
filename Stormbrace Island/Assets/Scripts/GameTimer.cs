using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private bool runClockOnStart;
    [SerializeField]
    private bool triggerLossOnExpiration;

    [Range(0, 59)]
    [SerializeField]
    private int startingClockMinutes = 10;
    [Range(0, 59)]
    [SerializeField]
    private int startingClockSeconds = 0;

    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject lossText;
    [SerializeField]
    private PlayerMovement playerMovement;

    public bool ClockRunning { get; set; }

    public int MinutesOnClock => (int)Mathf.Floor(SecondsRemaining / 60f);
    public int SecondsOnClock => ((int)SecondsRemaining) % 60;
    public float StartingSeconds { get; private set; }
    public float SecondsRemaining { get; private set; }
    public float SecondsElapsed => StartingSeconds - SecondsRemaining;

    private void Awake()
    {
        if (runClockOnStart) ClockRunning = true;
        StartingSeconds = startingClockMinutes * 60 + startingClockSeconds;
        SecondsRemaining = StartingSeconds;
        UpdateTimerText();
    }

    private void Update()
    {
        if (!ClockRunning) return;

        SecondsRemaining -= Time.deltaTime;
        if (SecondsRemaining < 0f)
        {
            ClockRunning = false;
            SecondsRemaining = 0f;
            if (triggerLossOnExpiration) StartCoroutine(TriggerLoss());
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        string secondText;
        if (SecondsOnClock < 10)
        {
            secondText = "0" + SecondsOnClock.ToString();
        }
        else
        {
            secondText = SecondsOnClock.ToString();
        }

        timerText.text = $"{MinutesOnClock}:{secondText}";
    }

    public void RemoveTime(float seconds)
    {
        SecondsRemaining -= seconds;
        if (SecondsRemaining < 0f) SecondsRemaining = 0f;
    }

    private IEnumerator TriggerLoss()
    {
        lossText.SetActive(true);
        playerMovement.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
