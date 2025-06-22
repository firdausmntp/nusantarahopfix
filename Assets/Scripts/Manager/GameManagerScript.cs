using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Header("Player & Finish")]
    public Transform player;
    public float fallOffset = 2.5f;
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
        float cameraBottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;

        if (currentY < cameraBottomY - fallOffset)
        {
            gameOver = true;
            Debug.Log("Player jatuh di bawah layar! Game Over!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        if (currentY >= finishY)
        {
            gameOver = true;
            Debug.Log("Player mencapai tujuan! Menang!");
            winPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        if (progressBar != null)
        {
            if (currentY > maxYReached)
                maxYReached = currentY;

            float totalDistance = finishY - startY;
            float progress = Mathf.Clamp01((maxYReached - startY) / totalDistance);
            progressBar.value = progress;
        }
    }
    public bool IsProgressFull()
    {
        bool full = progressBar != null && progressBar.value >= 0.85f;
        return full;
    }

    public void WinGame()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("🎉 Player MENANG karena melewati platform terakhir!");

        if (progressBar != null)
            progressBar.value = 1f; // 🌟 Diisi penuh saat menang

        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0f;
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
