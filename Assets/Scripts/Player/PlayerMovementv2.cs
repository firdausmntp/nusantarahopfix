using UnityEngine;
using System.Collections;

public class PlayerMovementv2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;
    private Animator animator;

    [HideInInspector]
    public Vector2 externalForce = Vector2.zero;

    private float originalMoveSpeed;
    private bool isSlowed = false;

    public void ApplySlow(float slowAmount, float duration)
    {
        if (isSlowed) return;

        isSlowed = true;
        originalMoveSpeed = moveSpeed;
        moveSpeed = Mathf.Max(1f, moveSpeed - slowAmount); // jangan nol total
        StartCoroutine(RemoveSlow(duration));
    }

    IEnumerator RemoveSlow(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = originalMoveSpeed;
        isSlowed = false;
        Debug.Log("⚡ Player kembali ke kecepatan normal.");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // ambil komponen Animator
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
        float totalX = (horizontal * moveSpeed) + externalForce.x;

        rb.velocity = new Vector2(totalX, rb.velocity.y);
        externalForce = Vector2.Lerp(externalForce, Vector2.zero, Time.deltaTime * 2f);

        // Wrap posisi
        if (transform.position.x > 6f)
            transform.position = new Vector3(-6f, transform.position.y, transform.position.z);
        else if (transform.position.x < -6f)
            transform.position = new Vector3(6f, transform.position.y, transform.position.z);

        // Update parameter animator
        animator.SetBool("isJump", rb.velocity.y > 0.1f);
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
