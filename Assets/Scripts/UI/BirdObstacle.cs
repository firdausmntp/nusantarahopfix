using UnityEngine;

public class BirdObstacle : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.left;
    public float moveSpeed = 4f;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🐦 Burung mengenai Player! Mematikan...");

            other.gameObject.SetActive(false); // Matikan player

            GameManagerScript gm = FindObjectOfType<GameManagerScript>();
            if (gm != null)
            {
                gm.GameOver();
            }
        }
    }
}
