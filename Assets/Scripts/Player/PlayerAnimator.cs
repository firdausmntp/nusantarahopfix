using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public Sprite normalSprite;
    public RuntimeAnimatorController normalController;

    public Sprite topiSprite;
    public RuntimeAnimatorController topiController;

    public Sprite sandalSprite;
    public RuntimeAnimatorController sandalController;

    public PlayerMovement playerMovement;
    public float efekTopiDuration = 10f;
    public float efekSandalDuration = 10f;

    private bool isWearingTopi = false;
    private bool isWearingSandal = false;

    public void PakaiTopi()
    {
        isWearingTopi = true;
        UpdateVisual();
        Debug.Log("🎩 Topi dipakai!");

        if (playerMovement != null)
            StartCoroutine(TopiDurationCoroutine());
    }

    public void PakaiSandal()
    {
        isWearingSandal = true;
        UpdateVisual();
        Debug.Log("👟 Sandal dipakai!");

        if (playerMovement != null)
            StartCoroutine(SandalDurationCoroutine());
    }

    IEnumerator TopiDurationCoroutine()
    {
        playerMovement.ignoreWind = true;
        Debug.Log("🌀 Efek topi aktif: ignoreWind = true");

        yield return new WaitForSeconds(efekTopiDuration);

        isWearingTopi = false;
        playerMovement.ignoreWind = false;
        Debug.Log("🛑 Efek topi habis: ignoreWind = false");

        UpdateVisual();
    }

    IEnumerator SandalDurationCoroutine()
    {
        float originalJump = playerMovement.jumpForce;
        playerMovement.jumpForce *= 1.1f; // Tambah 10%
        Debug.Log($"⬆️ Efek sandal aktif: jumpForce naik 10% jadi {playerMovement.jumpForce}");

        yield return new WaitForSeconds(efekSandalDuration);

        isWearingSandal = false;
        playerMovement.jumpForce = originalJump;
        Debug.Log($"🛑 Efek sandal habis: jumpForce dikembalikan ke {originalJump}");

        UpdateVisual();
    }

    public void LepasSemua()
    {
        StopAllCoroutines();
        isWearingTopi = false;
        isWearingSandal = false;
        Debug.Log("❌ Semua item dilepas!");

        if (playerMovement != null)
        {
            playerMovement.ignoreWind = false;
            playerMovement.ResetJumpForce(); // Pastikan metode ini ada di PlayerMovement
            Debug.Log("🔄 Reset jump force & ignoreWind");
        }

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (isWearingTopi)
        {
            spriteRenderer.sprite = topiSprite;
            animator.runtimeAnimatorController = topiController;
            Debug.Log("👕 Visual: Topi");
        }
        else if (isWearingSandal)
        {
            spriteRenderer.sprite = sandalSprite;
            animator.runtimeAnimatorController = sandalController;
            Debug.Log("👕 Visual: Sandal");
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
            animator.runtimeAnimatorController = normalController;
            Debug.Log("👕 Visual: Normal");
        }
    }
}
