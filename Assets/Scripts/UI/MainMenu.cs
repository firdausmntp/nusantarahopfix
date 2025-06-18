using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
}
