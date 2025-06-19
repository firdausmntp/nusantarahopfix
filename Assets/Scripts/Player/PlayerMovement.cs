using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Debug.Log("Player ONENABLE dipanggil");
        StartCoroutine(DelayedJump());
    }

    IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Player lompat otomatis");
        Jump();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (transform.position.x > 6f)
            transform.position = new Vector3(-6f, transform.position.y, transform.position.z);
        else if (transform.position.x < -6f)
            transform.position = new Vector3(6f, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            ContactPoint2D contact = collision.contacts[0];

            if (contact.point.y < transform.position.y - 0.1f)
            {
                Debug.Log("Kena platform → lompat");
                Jump();
            }
        }
    }

    void Jump()
    {
        Debug.Log("Jump() dilakukan");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
