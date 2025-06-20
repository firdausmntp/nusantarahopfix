using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Header("Player & Finish")]
    public Transform player;
    public float fallThreshold = -10f;
    public float finishY = 50f;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject winPanel;

    [Header("Progress UI")]
    public Slider progressBar;

    private bool gameOver = false;
    private float startY;
    private float maxYReached;

    void Start()
    {
        if (player != null)
        {
            startY = player.position.y;
            maxYReached = startY;

            // Inisialisasi progress bar di awal
            if (progressBar != null)
            {
                progressBar.minValue = 0f;
                progressBar.maxValue = 1f;
                progressBar.value = 0f;
            }
        }
    }

    void Update()
    {
        if (gameOver || player == null) return;

        float currentY = player.position.y;

        // Cek player jatuh
        if (currentY < fallThreshold)
        {
            gameOver = true;
            Debug.Log("Player jatuh! Game Over!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }



        // Cek player menang
        if (currentY >= finishY)
        {
            gameOver = true;
            Debug.Log("Player mencapai tujuan! Menang!");
            winPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        // Update progress bar
        if (progressBar != null)
        {
            if (currentY > maxYReached)
                maxYReached = currentY;

            float totalDistance = finishY - startY;
            float progress = Mathf.Clamp01((maxYReached - startY) / totalDistance);
            progressBar.value = progress;
        }
    }
    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("‼️ Game Over dipanggil dari obstacle (misal burung)");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
