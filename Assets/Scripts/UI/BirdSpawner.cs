using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnInterval = 5f;
    public float spawnYMin = -2f;
    public float spawnYMax = 5f;
    public float spawnXOffset = 12f;
    public float birdSpeed = 4f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnBird();
        }
    }

    void SpawnBird()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        // Tentukan arah datang burung
        bool fromLeft = Random.value > 0.5f;
        float spawnX = fromLeft ? playerPos.x - spawnXOffset : playerPos.x + spawnXOffset;
        float spawnY = playerPos.y + Random.Range(spawnYMin, spawnYMax);

        // Buat burung
        GameObject bird = Instantiate(birdPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        // Atur arah gerak
        BirdObstacle bo = bird.GetComponent<BirdObstacle>();
        if (bo != null)
        {
            bo.moveDirection = fromLeft ? Vector2.right : Vector2.left;
            bo.moveSpeed = birdSpeed;
        }

        // Flip arah visual burung jika datang dari kanan
        if (!fromLeft)
        {
            Vector3 scale = bird.transform.localScale;
            scale.x *= -1; // membalik sprite secara horizontal
            bird.transform.localScale = scale;
        }

        Debug.Log($"🐦 Burung spawn dari {(fromLeft ? "kiri" : "kanan")} menghadap {(fromLeft ? "kanan" : "kiri")}");
    }
}
