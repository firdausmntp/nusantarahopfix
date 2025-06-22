using UnityEngine;
using System.Collections.Generic;

public class SpawnTopiSandal : MonoBehaviour
{
    public GameObject topiPrefab;
    public GameObject sandalPrefab;
    public int maxTotalItem = 5;
    public Transform player;
    public float destroyBelowDistance = 5f;

    private List<GameObject> spawnedItems = new List<GameObject>();
    private GameObject[] platform;

    void Start()
    {
        platform = GameObject.FindGameObjectsWithTag("Platform");

        if (platform.Length == 0)
        {
            Debug.LogError("Tidak ada GameObject dengan tag 'Platform'");
            return;
        }

        List<GameObject> availablePlatform = new List<GameObject>(platform);
        Shuffle(availablePlatform);

        int jumlahItem = Mathf.Min(maxTotalItem, availablePlatform.Count);
        for (int i = 0; i < jumlahItem; i++)
        {
            GameObject plat = availablePlatform[i];
            BoxCollider2D col = plat.GetComponent<BoxCollider2D>();
            if (col == null) continue;

            Bounds bounds = col.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = bounds.max.y + 0.5f;
            Vector2 pos = new Vector2(x, y);

            GameObject prefab = Random.value < 0.5f ? topiPrefab : sandalPrefab;
            GameObject item = Instantiate(prefab, pos, Quaternion.identity);
            spawnedItems.Add(item);
        }
    }

    void Update()
    {
        CheckAndDestroyItems();
    }

    void CheckAndDestroyItems()
    {
        if (player == null) return;

        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            GameObject item = spawnedItems[i];
            if (item == null) continue;

            if (item.transform.position.y < player.position.y - destroyBelowDistance)
            {
                Destroy(item);
                spawnedItems.RemoveAt(i);
            }
        }
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randIndex = Random.Range(i, list.Count);
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }
}
