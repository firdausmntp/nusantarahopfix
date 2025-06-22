using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType { Topi, Sandal }
    public ItemType itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerAnimator playerAnimator = other.GetComponent<PlayerAnimator>();
        if (playerAnimator == null) return;

        switch (itemType)
        {
            case ItemType.Topi:
                playerAnimator.PakaiTopi();
                break;
            case ItemType.Sandal:
                playerAnimator.PakaiSandal();
                break;
        }

        Debug.Log($"🎯 Player mengambil item: {itemType}");
        Destroy(gameObject);
    }

}
