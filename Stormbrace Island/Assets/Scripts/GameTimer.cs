using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Range(0, 59)]
    [SerializeField]
    private int startingClockMinutes = 10;
    [Range(0, 59)]
    [SerializeField]
    private int startingClockSeconds = 0;

    [SerializeField]
    private TextMeshProUGUI timerText;

    public bool ClockRunning { get; set; }

    public int MinutesOnClock => (int)Mathf.Floor(SecondsRemaining / 60f);
    public int SecondsOnClock => ((int)SecondsRemaining) % 60;
    public float StartingSeconds { get; private set; }
    public float SecondsRemaining { get; private set; }
    public float SecondsElapsed => StartingSeconds - SecondsRemaining;

    private void Awake()
    {
        StartingSeconds = startingClockMinutes * 60 + startingClockSeconds;
        SecondsRemaining = StartingSeconds;
        UpdateTimerText();
    }

    private void Update()
    {
        //if (!ClockRunning) return;

        SecondsRemaining -= Time.deltaTime;
        if (SecondsRemaining < 0f) SecondsRemaining = 0f;

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
}
