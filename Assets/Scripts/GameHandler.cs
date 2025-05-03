using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject gameOverMenu;
    public float delayBeforeMainMenu = 2.5f;

    public static bool Spawn = false;

    public void TriggerGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        // Only use this line if you want auto-return:
        // StartCoroutine(GoToMainMenuAfterDelay());
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator GoToMainMenuAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeMainMenu);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
