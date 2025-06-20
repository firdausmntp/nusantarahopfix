using UnityEngine;

public class WindObstacle : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right;  // Arah gerakan angin
    public float moveSpeed = 3f;                   // Kecepatan gerakan angin
    public float pushForce = 2f;                   // Gaya dorong terhadap player
    public float lifetime = 5f;                    // Lama hidup angin sebelum hancur

    void Start()
    {
        // Hancurkan angin setelah beberapa detik
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Gerakkan angin sesuai arah dan kecepatan
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("💨 Angin mengenai Player!");

            // Ambil script controller player dan dorong menggunakan external force
            PlayerMovement PlayerMovement = other.GetComponent<PlayerMovement>();
            if (PlayerMovement != null)
            {
                PlayerMovement.externalForce += moveDirection.normalized * pushForce;
            }
        }
    }
}
