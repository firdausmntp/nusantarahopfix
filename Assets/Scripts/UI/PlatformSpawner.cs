using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float levelWidth = 3f;
    public float minY = 2f;
    public float maxY = 3.5f;
    public float destroyBelowDistance = 5f;

    public GameManagerScript gameManager;
    public BambooSpawner bambooSpawner;

    private float spawnY = 0f;
    private Transform player;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private GameObject lastSpawnedPlatform;
    private bool hasWon = false;

    void Start()
    {
        StartCoroutine(WaitForPlayer());
    }

    IEnumerator WaitForPlayer()
    {
        while (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null && playerObj.activeInHierarchy)
            {
                player = playerObj.transform;
                spawnY = player.position.y - 2f;
            }
            yield return null;
        }
    }

    void Update()
    {
        if (player == null || gameManager == null) return;

        if (!gameManager.IsProgressFull() && player.position.y + 10f > spawnY)
        {
            SpawnPlatform(spawnedPlatforms.Count);
            CheckAndDestroyPassedPlatforms();
        }

        // ✅ Deteksi kemenangan jika sudah melewati platform terakhir
        if (!hasWon && lastSpawnedPlatform != null && player.position.y > lastSpawnedPlatform.transform.position.y + 1f)
        {
            hasWon = true;
            gameManager.WinGame();
            Debug.Log("🏁 Player menang karena melewati platform terakhir");
        }
    }

    void SpawnPlatform(int index)
    {
        if (platformPrefab == null) return;

        float spawnX = Random.Range(-levelWidth, levelWidth);
        float offsetY = Random.Range(minY, maxY);
        spawnY += offsetY;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        platform.name = "Platform_" + index;
        spawnedPlatforms.Add(platform);

        lastSpawnedPlatform = platform;

        if (bambooSpawner != null)
        {
            bambooSpawner.TrySpawnBambooAbove(platform.transform);
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
                Destroy(platform);
                spawnedPlatforms.RemoveAt(i);
            }
        }
    }
}
