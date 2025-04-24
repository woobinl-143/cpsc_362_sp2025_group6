using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;
    public float delayBeforeMainMenu = 2.5f;

    public void TriggerGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(GoToMainMenuAfterDelay());
    }

    private IEnumerator GoToMainMenuAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeMainMenu);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
