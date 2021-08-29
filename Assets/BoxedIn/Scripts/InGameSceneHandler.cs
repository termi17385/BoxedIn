using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameSceneHandler : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
