using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;

    public void LoadGameplay()
    {
        Debug.Log("test");
        SceneManager.LoadScene("MainScene");
    }

    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenHomeMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
