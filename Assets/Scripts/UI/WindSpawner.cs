using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    public GameObject windObstaclePrefab;
    public float spawnInterval = 3f;
    public float spawnYMin = -2f;
    public float spawnYMax = 2f;
    public float spawnXOffset = 10f;

    [Header("Wind Settings")]
    public float windSpeed = 3f;
    public float pushForce = 2f;

    [Header("Auto Destroy")]
    public float destroyDistance = 20f;

    private float timer = 0f;
    private List<GameObject> spawnedWinds = new List<GameObject>();
    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWindObstacle();
        }

        CheckAndDestroyWinds();
    }

    void SpawnWindObstacle()
    {
        if (player == null) return;

        Vector3 playerPos = player.position;
        bool fromLeft = Random.value > 0.5f;
        float spawnX = fromLeft ? playerPos.x - spawnXOffset : playerPos.x + spawnXOffset;
        float spawnY = playerPos.y + Random.Range(spawnYMin, spawnYMax);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Vector2 moveDir = fromLeft ? Vector2.right : Vector2.left;

        GameObject wind = Instantiate(windObstaclePrefab, spawnPos, Quaternion.identity);
        spawnedWinds.Add(wind);

        WindObstacle wo = wind.GetComponent<WindObstacle>();
        if (wo != null)
        {
            wo.moveDirection = moveDir;
            wo.moveSpeed = windSpeed;
            wo.pushForce = pushForce;
        }

        Debug.Log($"🌬️ Angin muncul dari {(fromLeft ? "kiri" : "kanan")} di Y: {spawnY:F2}");
    }

    void CheckAndDestroyWinds()
    {
        if (player == null) return;

        for (int i = spawnedWinds.Count - 1; i >= 0; i--)
        {
            GameObject wind = spawnedWinds[i];
            if (wind == null) continue;

            float distance = Vector3.Distance(player.position, wind.transform.position);
            if (distance > destroyDistance)
            {
                Destroy(wind);
                spawnedWinds.RemoveAt(i);
                Debug.Log("🗑️ Angin dihapus karena terlalu jauh");
            }
        }
    }
}
