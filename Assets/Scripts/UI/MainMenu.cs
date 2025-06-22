using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panelSetting;

    void Start()
    {
        // Pastikan musik menyala saat game mulai
        if (!MusicManager.instance.IsMusicPlaying())
        {
            MusicManager.instance.PlayMusic();
        }
    }

    public void MulaiPetualangan()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PilihGunung()
    {
        SceneManager.LoadScene("SelectMountain");
    }

    public void Keluar()
    {
        Application.Quit();
        Debug.Log("Keluar dari game");
    }

    public void BukaPanelSetting()
    {
        panelSetting.SetActive(true);
    }

    public void TutupPanelSetting()
    {
        panelSetting.SetActive(false);
    }

    public void MusikOn()
    {
        MusicManager.instance.PlayMusic();
    }

    public void MusikOff()
    {
        MusicManager.instance.StopMusic();
    }
}
