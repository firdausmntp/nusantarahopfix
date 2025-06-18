using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameButtons : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
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
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            SceneManager.LoadScene("MainMenu"); // Jika tidak ada level berikutnya
        }
    }
}
