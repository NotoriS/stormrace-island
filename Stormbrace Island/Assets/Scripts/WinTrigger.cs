using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject winText;
    [SerializeField]
    private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TriggerWin());
        }
    }

    private IEnumerator TriggerWin()
    {
        winText.SetActive(true);
        FindFirstObjectByType<GameTimer>().ClockRunning = false;
        playerMovement.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }


}
