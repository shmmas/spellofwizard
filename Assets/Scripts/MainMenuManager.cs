using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("spellofcolosseum"); // Ganti dengan nama scene game kamu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
