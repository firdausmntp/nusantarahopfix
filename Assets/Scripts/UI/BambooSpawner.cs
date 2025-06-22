using UnityEngine;
using System.Collections.Generic;

public class BambooSpawner : MonoBehaviour
{
    public GameObject bambooPrefab;
    [Range(0f, 1f)]
    public float bambooSpawnChance = 0.3f;

    public Transform player;
    public float destroyBelowDistance = 5f;

    private List<GameObject> spawnedBamboo = new List<GameObject>();

    public void TrySpawnBambooAbove(Transform platformTransform)
    {
        if (bambooPrefab == null || platformTransform == null) return;

        if (Random.value < bambooSpawnChance)
        {
            Vector3 spawnPosition = platformTransform.position + new Vector3(0f, 0.7f, 0f);
            GameObject bamboo = Instantiate(bambooPrefab, spawnPosition, Quaternion.identity);
            spawnedBamboo.Add(bamboo);
            Debug.Log("🎍 Bambu spawned di atas " + platformTransform.name);
        }
    }

    void Update()
    {
        CheckAndDestroyPassedBamboo();
    }

    void CheckAndDestroyPassedBamboo()
    {
        if (player == null) return;

        for (int i = spawnedBamboo.Count - 1; i >= 0; i--)
        {
            GameObject bamboo = spawnedBamboo[i];
            if (bamboo == null) continue;

            if (bamboo.transform.position.y < player.position.y - destroyBelowDistance)
            {
                Destroy(bamboo);
                spawnedBamboo.RemoveAt(i);
            }
        }
    }
}
