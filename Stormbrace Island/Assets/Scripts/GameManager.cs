using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float textTime = 3f;
    [SerializeField]
    private float timeBetweenText = 2f;
    [SerializeField]
    private GameObject startText;
    [SerializeField]
    private GameObject movementTutorialText;
    [SerializeField]
    private GameObject jumpTutorialText;
    [SerializeField]
    private PlayerMovement playerMovement;

    void Start()
    {
        StartCoroutine(StartGamplay());
    }

    private IEnumerator StartGamplay()
    {
        yield return new WaitForSeconds(textTime);
        startText.SetActive(false);
        playerMovement.enabled = true;
        FindFirstObjectByType<GameTimer>().ClockRunning = true;
        yield return new WaitForSeconds(timeBetweenText);

        movementTutorialText.SetActive(true);
        yield return new WaitForSeconds(textTime);
        movementTutorialText.SetActive(false);
        yield return new WaitForSeconds(timeBetweenText);

        jumpTutorialText.SetActive(true);
        yield return new WaitForSeconds(textTime);
        jumpTutorialText.SetActive(false);
    }
}
