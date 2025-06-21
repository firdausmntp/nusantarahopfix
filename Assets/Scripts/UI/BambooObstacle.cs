using UnityEngine;

public class BambooObstacle : MonoBehaviour
{
    public float slowAmount = 2f;
    public float slowDuration = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                Debug.Log("🎍 Player menyentuh bambu! Diperlambat.");
                player.ApplySlow(slowAmount, slowDuration);
            }
        }
    }
}
