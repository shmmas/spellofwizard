using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPopup : MonoBehaviour
{
    public GameObject popupUI; // Drag UI Panel ke sini dari Inspector
    public float sceneDelay = 1.0f; // Nilai seberapa lama delay untuk berpindah ke scene selanjutnya
    public Animator transitionAnimator;
    public bool useTransition = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan Player memiliki tag "Player"
        {
            popupUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(false);   
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(TransitionAndLoadScene(sceneName));
    }

    private IEnumerator TransitionAndLoadScene(string sceneName)
    {

        if (useTransition)
        {
            transitionAnimator.SetTrigger("Close"); // Set trigger animasi "Close"

            yield return new WaitForSeconds(sceneDelay); // Mendelay perpindahan scene
        }

        SceneManager.LoadScene(sceneName); // Pindah Scene setelah animasi selesai
    }
}
