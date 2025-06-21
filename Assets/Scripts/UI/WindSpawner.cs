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

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWindObstacle();
        }
    }

    void SpawnWindObstacle()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("❗ Player tidak ditemukan.");
            return;
        }

        Vector3 playerPos = player.transform.position;

        // Tentukan arah dari kiri atau kanan
        bool fromLeft = Random.value > 0.5f;
        float spawnX = fromLeft ? playerPos.x - spawnXOffset : playerPos.x + spawnXOffset;
        float spawnY = playerPos.y + Random.Range(spawnYMin, spawnYMax);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Vector2 moveDir = fromLeft ? Vector2.right : Vector2.left;

        GameObject wind = Instantiate(windObstaclePrefab, spawnPos, Quaternion.identity);

        WindObstacle wo = wind.GetComponent<WindObstacle>();
        if (wo != null)
        {
            wo.moveDirection = moveDir;
            wo.moveSpeed = windSpeed;
            wo.pushForce = pushForce;
        }

        Debug.Log($"🌬️ Angin muncul dari {(fromLeft ? "kiri" : "kanan")} di Y: {spawnY:F2}");
    }
}
