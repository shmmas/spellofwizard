using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Fungsi ini dipanggil saat tombol "Mulai" ditekan
    public void StartGame()
    {
        // Ganti "GameScene" dengan nama scene yang akan dimuat
        SceneManager.LoadScene("spellofcolosseum");
    }

    // Fungsi ini dipanggil saat tombol "Keluar" ditekan
    public void ExitGame()
    {
        // Di Editor, Application.Quit() tidak berpengaruh.
        Application.Quit();
    }
}
