using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 10;
    public float levelWidth = 3f;
    public float minY = 2f;
    public float maxY = 3.5f;
    public float spawnDelay = 0.5f;

    private float spawnY = 0f;

    void Start()
    {
        StartCoroutine(SpawnPlatformsGradually());
    }

    IEnumerator SpawnPlatformsGradually()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnPlatform()
    {
        float spawnX = Random.Range(-levelWidth, levelWidth);
        float offsetY = Random.Range(minY, maxY);
        spawnY += offsetY;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
    }
}
