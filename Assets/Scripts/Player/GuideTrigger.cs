using System.Collections;
using UnityEngine;
using TMPro;

public class GuideTrigger : MonoBehaviour
{
    public AudioSource alertSound; // Suara alert saat trigger aktif
    public GameObject alertText;   // Teks notifikasi sementara
    public float displayDuration = 3f; // Waktu teks ditampilkan (dalam detik)

    private bool hasTriggered = false; // Pastikan hanya dipicu sekali

    private void Start()
    {
        // Pastikan teks tidak terlihat di awal
        if (alertText != null)
        {
            alertText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang masuk adalah XR Origin/XR Rig (player) & belum pernah dipicu
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;  // Set agar tidak bisa dipicu lagi
            alertSound.Play();    // Mainkan suara alert

            if (alertText != null)
            {
                alertText.SetActive(true); // Tampilkan teks notifikasi
                StartCoroutine(HideAlertText()); // Mulai timer untuk menyembunyikan teks
            }
        }
    }

    IEnumerator HideAlertText()
    {
        yield return new WaitForSeconds(displayDuration);

        // Sembunyikan teks setelah beberapa detik
        if (alertText != null)
        {
            alertText.SetActive(false);
        }
    }
}
