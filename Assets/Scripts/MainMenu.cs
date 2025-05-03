using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("Snake");
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Snakes");
    }

    public void Difficulty()
    {
        SceneManager.LoadScene("Difficulty");
    }
}
