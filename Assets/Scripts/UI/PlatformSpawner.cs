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
        if (platformPrefab == null)
        {
            Debug.LogError("Platform Prefab belum di-assign di inspector!");
            return;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan. Destroy logic akan di-skip.");
        }

        StartCoroutine(SpawnPlatformsGradually());
    }

    IEnumerator SpawnPlatformsGradually()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform(i);

            // Setiap 2 platform, lakukan pengecekan
            if ((i + 1) % 2 == 0 && player != null)
            {
                CheckAndDestroyPassedPlatforms();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnPlatform(int index)
    {
        float spawnX = Random.Range(-levelWidth, levelWidth);
        float offsetY = Random.Range(minY, maxY);
        spawnY += offsetY;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        try
        {
            GameObject spawnedPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            spawnedPlatform.name = "Platform_" + index;
            spawnedPlatforms.Add(spawnedPlatform);
            Debug.Log($"Platform {index} spawned at {spawnPosition}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saat spawn platform {index}: {e.Message}\n{e.StackTrace}");
        }
    }

    void CheckAndDestroyPassedPlatforms()
    {
        for (int i = spawnedPlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = spawnedPlatforms[i];
            if (platform == null) continue;

            if (platform.transform.position.y < player.position.y - destroyBelowDistance)
            {
                Debug.Log($"Menghapus platform: {platform.name}");
                Destroy(platform);
                spawnedPlatforms.RemoveAt(i);
            }
        }
    }
}
