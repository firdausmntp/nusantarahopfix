using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    public AudioClip resultSound;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameObject.activeSelf && !hasPlayed)
        {
            audioSource.PlayOneShot(resultSound);
            hasPlayed = true;
        }
    }

    void OnDisable()
    {
        hasPlayed = false; // Reset supaya bisa dimainkan lagi jika panel aktif ulang
    }
}
