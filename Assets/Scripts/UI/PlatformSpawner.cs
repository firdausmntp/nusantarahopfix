using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 10;
    public float levelWidth = 3f;
    public float minY = 2f;
    public float maxY = 3.5f;
    public float spawnDelay = 0.5f;
    public float destroyBelowDistance = 5f;

    private float spawnY = 0f;
    private Transform player;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    void Start()
    {
        Debug.Log("🚀 Start PlatformSpawner terpanggil");

        if (platformPrefab == null)
        {
            Debug.LogError("❌ Platform Prefab belum di-assign di inspector!");
            return;
        }

        StartCoroutine(WaitForPlayerAndSpawn());
    }


    IEnumerator WaitForPlayerAndSpawn()
    {
        while (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null && playerObj.activeInHierarchy)
            {
                player = playerObj.transform;
                spawnY = player.position.y - 2f;
                Debug.Log("✅ Player ditemukan oleh PlatformSpawner");
            }
            yield return null;
        }

        Debug.Log("▶️ Siap mulai spawn platform dari Y: " + spawnY);
        yield return StartCoroutine(SpawnPlatformsGradually());
        Debug.Log("✅ Selesai SpawnPlatformsGradually");
    }

    IEnumerator SpawnPlatformsGradually()
    {
        Debug.Log("▶️ Mulai SpawnPlatformsGradually");

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform(i);

            if ((i + 1) % 2 == 0 && player != null)
            {
                CheckAndDestroyPassedPlatforms();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnPlatform(int index)
    {
        if (platformPrefab == null)
        {
            Debug.LogError("❌ platformPrefab NULL saat runtime!");
            return;
        }

        float spawnX = Random.Range(-levelWidth, levelWidth);
        float offsetY = Random.Range(minY, maxY);
        spawnY += offsetY;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        GameObject spawnedPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        spawnedPlatform.name = "Platform_" + index;
        spawnedPlatforms.Add(spawnedPlatform);

        Debug.Log($"🟩 Platform {index} spawned at {spawnPosition}");
    }

    void CheckAndDestroyPassedPlatforms()
    {
        for (int i = spawnedPlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = spawnedPlatforms[i];
            if (platform == null) continue;

            if (platform.transform.position.y < player.position.y - destroyBelowDistance)
            {
                Debug.Log($"🗑️ Menghapus platform: {platform.name}");
                Destroy(platform);
                spawnedPlatforms.RemoveAt(i);
            }
        }
    }
}
