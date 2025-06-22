using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    [Header("Tombol Scene")]
    public Button buttonLevel1;
    public Button buttonLevel2;
    public Button buttonLevel3;

    [Header("Image yang bisa ditoggle")]
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    private int currentActive = 0;

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    void Start()
    {
        // Tambahkan event ke tombol
        buttonLevel1.onClick.AddListener(() => LoadLevel("Level1"));
        buttonLevel2.onClick.AddListener(() => LoadLevel("Level2"));
        buttonLevel3.onClick.AddListener(() => LoadLevel("Level3"));

        // Nonaktifkan semua image di awal
        SetAllImagesInactive();
    }

    // Fungsi untuk pindah scene
    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Fungsi toggle image (hanya satu aktif)
    public void ToggleExclusiveImage(int imageIndex)
    {
        if (currentActive == imageIndex)
        {
            SetAllImagesInactive();
            currentActive = 0;
            return;
        }

        SetAllImagesInactive();

        switch (imageIndex)
        {
            case 1:
                image1.SetActive(true);
                break;
            case 2:
                image2.SetActive(true);
                break;
            case 3:
                image3.SetActive(true);
                break;
        }

        currentActive = imageIndex;
    }

    // Matikan semua gambar
    void SetAllImagesInactive()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
    }
}