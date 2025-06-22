using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType { Topi, Sandal }
    public ItemType itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovementv2 player = other.GetComponent<PlayerMovementv2>();
        if (player == null) return;

        switch (itemType)
        {
            case ItemType.Topi:
                player.PakaiTopi();
                break;
            case ItemType.Sandal:
                player.PakaiSandal();
                break;
        }

        Debug.Log($"🎯 Player mengambil item: {itemType}");
        Destroy(gameObject);
    }
}
