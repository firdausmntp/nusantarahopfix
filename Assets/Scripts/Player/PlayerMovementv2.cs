using UnityEngine;

public class PlayerMovementv2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rb;

    [Header("Sprite & Animator")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("Normal")]
    public Sprite normalSprite;
    public RuntimeAnimatorController normalController;

    [Header("Topi")]
    public Sprite topiSprite;
    public RuntimeAnimatorController topiController;

    [Header("Sandal")]
    public Sprite sandalSprite;
    public RuntimeAnimatorController sandalController;

    [Header("Topi + Sandal (Optional)")]
    public Sprite topiSandalSprite;
    public RuntimeAnimatorController topiSandalController;

    private bool isWearingTopi = false;
    private bool isWearingSandal = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(DelayedJump());
    }

    System.Collections.IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(0.1f);
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
                Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        if (isWearingTopi)
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

        Debug.Log($"🎭 Visual player kini: {(isWearingTopi ? "Topi" : isWearingSandal ? "Sandal" : "Normal")}");
    }

}
