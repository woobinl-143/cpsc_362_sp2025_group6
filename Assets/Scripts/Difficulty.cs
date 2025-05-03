using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    // Exit the Scene
    public void ExitScene()
    {
        SceneManager.LoadScene("Main Menu");       
    }

    // Choose Difficulty
    public void Normal()
    {
        GameHandler.Spawn = false;
        Debug.Log("Set to Normal Difficulty");
    }

    public void Hard()
    {
        GameHandler.Spawn = true;
        Debug.Log("Set to Hard Difficulty");
    }
}
