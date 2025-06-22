using UnityEngine;
using System.Collections;

public class BambooObstacle : MonoBehaviour
{
    public float newMoveSpeed = 2f;
    public float duration = 2f;

    private bool isSlowing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isSlowing)
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                Debug.Log("🎍 Player menyentuh bambu! Kecepatan diubah sementara.");
                StartCoroutine(SlowTemporarily(player));
            }
        }
    }

    IEnumerator SlowTemporarily(PlayerMovement player)
    {
        isSlowing = true;
        float originalSpeed = player.moveSpeed;
        player.moveSpeed = Mathf.Max(1f, newMoveSpeed);
        yield return new WaitForSeconds(duration);
        player.moveSpeed = originalSpeed;
        Debug.Log("⚡ Kecepatan player kembali normal dari BambooObstacle.");
        isSlowing = false;
    }
}
