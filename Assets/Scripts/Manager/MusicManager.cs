using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource musicSource;

    private void Awake()
    {
        // Jika belum ada instance, ini jadi instance utama
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicSource = GetComponent<AudioSource>();
        }
        else
        {
            // Jika instance lain dibuat, hapus (biar tidak dobel)
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
    }

    public bool IsMusicPlaying()
    {
        return musicSource.isPlaying;
    }
}
