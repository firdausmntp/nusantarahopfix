using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;

    [HideInInspector]
    public Vector2 externalForce = Vector2.zero;

    private float originalMoveSpeed;
    private bool isSlowed = false;

    [HideInInspector]
    public bool ignoreWind = false;
    private float defaultJumpForce;

    public AudioClip jumpSound;
    private AudioSource audioSource;

    private Animator animator;

    private float lastJumpTime = 0f;
    public float jumpCooldown = 0.2f;

    public void ApplySlow(float slowAmount, float duration)
    {
        if (isSlowed) return;

        isSlowed = true;
        originalMoveSpeed = moveSpeed;
        moveSpeed = Mathf.Max(1f, moveSpeed - slowAmount);
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
        defaultJumpForce = jumpForce;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
            Debug.LogWarning("⚠️ AudioSource belum terpasang di GameObject!");
        if (jumpSound == null)
            Debug.LogWarning("⚠️ jumpSound belum diisi di Inspector!");
        if (animator == null)
            Debug.LogWarning("⚠️ Animator belum terpasang!");
    }

    void OnEnable()
    {
        StartCoroutine(DelayedJump());
    }

    public void ResetJumpForce()
    {
        jumpForce = defaultJumpForce;
    }

    IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(0.1f);
        Jump();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float totalX = horizontal * moveSpeed;

        if (!ignoreWind)
            totalX += externalForce.x;

        rb.velocity = new Vector2(totalX, rb.velocity.y);
        externalForce = Vector2.Lerp(externalForce, Vector2.zero, Time.deltaTime * 2f);

        if (transform.position.x > 6f)
            transform.position = new Vector3(-6f, transform.position.y, transform.position.z);
        else if (transform.position.x < -6f)
            transform.position = new Vector3(6f, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    Jump();
                    break;
                }
            }
        }
    }

    void Jump()
    {
        if (Time.time - lastJumpTime < jumpCooldown)
        {
            Debug.Log("⏳ Lompat dibatalkan karena cooldown.");
            return;
        }

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        lastJumpTime = Time.time;

        // 🔛 Aktifkan animasi lompat
        if (animator != null)
        {
            animator.SetBool("isJump", true);
            StartCoroutine(ResetJumpBoolAfterDelay(0.2f)); // reset animasi
        }

        // 🔊 Mainkan suara lompat
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        else
        {
            Debug.LogWarning("❌ Tidak bisa putar jumpSound: Komponen hilang atau belum diisi.");
        }
    }

    IEnumerator ResetJumpBoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (animator != null)
            animator.SetBool("isJump", false);
    }
}
