using UnityEngine;
using System.Collections;

public class PlayerMovementv2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public Sprite normalSprite;
    public RuntimeAnimatorController normalController;

    public Sprite topiSprite;
    public RuntimeAnimatorController topiController;

    public Sprite sandalSprite;
    public RuntimeAnimatorController sandalController;

    public Sprite topiSandalSprite;
    public RuntimeAnimatorController topiSandalController;

    private bool isWearingTopi = false;
    private bool isWearingSandal = false;

    [HideInInspector]
    public Vector2 externalForce = Vector2.zero;

    private float originalMoveSpeed;
    private bool isSlowed = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(DelayedJump());
    }

    IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(0.1f);
        Jump();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float totalX = (horizontal * moveSpeed) + externalForce.x;
        rb.velocity = new Vector2(totalX, rb.velocity.y);
        externalForce = Vector2.Lerp(externalForce, Vector2.zero, Time.deltaTime * 2f);

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
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

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
    }

    public void PakaiTopi()
    {
        isWearingTopi = true;
        isWearingSandal = false;
        UpdateVisual();
    }

    public void PakaiSandal()
    {
        isWearingSandal = true;
        isWearingTopi = false;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (isWearingTopi && isWearingSandal)
        {
            spriteRenderer.sprite = topiSandalSprite;
            animator.runtimeAnimatorController = topiSandalController;
        }
        else if (isWearingTopi)
        {
            spriteRenderer.sprite = topiSprite;
            animator.runtimeAnimatorController = topiController;
        }
        else if (isWearingSandal)
        {
            spriteRenderer.sprite = sandalSprite;
            animator.runtimeAnimatorController = sandalController;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
            animator.runtimeAnimatorController = normalController;
        }
    }
}
