using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("MainMenu"); // Sesuaikan nama scene jika perlu
    }

    public void Retry()
    {
          SceneManager.LoadScene("Level1");
        // Mengulang scene yang sedang dimainkan
        // string currentScene = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(currentScene);
    }
}
