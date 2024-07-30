using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour
{
     void Awake()
    {
        Reference.canvas = gameObject;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void BackMenu()
    {
        Time.timeScale = 1;
        LoadingScreen.sceneToLoad = 0;
        SceneManager.LoadScene(1);
    }
}
