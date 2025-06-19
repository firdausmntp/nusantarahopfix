using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOverGame : MonoBehaviour
{
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        string nextLevelName = null;

        for (int i = currentIndex + 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName.Contains("Level"))
            {
                nextLevelName = sceneName;
                break;
            }
        }

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
