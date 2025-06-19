using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Transform player;
    public float fallThreshold = -10f;
    public GameObject gameOverPanel; // Drag panel GameOver dari Canvas ke sini
    public GameObject winPanel; // Drag panel Win dari Canvas ke sini

    private bool gameOver = false;

    void Update()
    {
        if (!gameOver && player.position.y < fallThreshold)
        {
            gameOver = true;
            Debug.Log("Player jatuh! Game Over!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Optional: pause game
        }
        if (!gameOver && player.position.y > 50f) // Ganti 10f dengan ketinggian yang sesuai untuk menang
        {
            gameOver = true;
            Debug.Log("Player mencapai tujuan! Menang!");
            winPanel.SetActive(true);
            Time.timeScale = 0f; // Optional: pause game
        }
    }
}
