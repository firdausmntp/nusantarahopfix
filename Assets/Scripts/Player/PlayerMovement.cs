using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jump(); // Memastikan player bisa lompat saat mulai
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // Gerakan kiri-kanan
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // Opsional: wrap around screen (kayak Pou atau Doodle Jump)
        if (transform.position.x > 6f)
            transform.position = new Vector3(-6f, transform.position.y, transform.position.z);
        else if (transform.position.x < -6f)
            transform.position = new Vector3(6f, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            // Ambil titik kontak
            ContactPoint2D contact = collision.contacts[0];

            // Cek apakah kontaknya ada di bawah player
            if (contact.point.y < transform.position.y - 0.1f)
            {
                Debug.Log("Lompat!");
                Jump();
            }
        }
    }



    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
