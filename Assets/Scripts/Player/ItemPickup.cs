using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player menyentuh: " + gameObject.name);
            Destroy(gameObject); // hapus item setelah diambil
        }
    }
}
