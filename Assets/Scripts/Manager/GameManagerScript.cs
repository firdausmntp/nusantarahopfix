using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Transform player;     // Drag objek player ke sini di Inspector
    public float fallThreshold = -10f; // Batas bawah, kalau player jatuh sampai Y ini, game over

    void Update()
    {
        if (player.position.y < fallThreshold)
        {
            Debug.Log("Player jatuh! Game Over!");
            SceneManager.LoadScene("GameOver"); // Pastikan scene ini sudah ada di Build Settings
        }
    }
}
