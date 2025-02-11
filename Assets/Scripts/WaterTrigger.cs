using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public AudioSource audioSource; // Untuk sumber suara
    public AudioClip splashWater; // SFX Air
    public GameObject objectToActivate; // GameObject yang akan dimunculkan


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook")) // Periksa apakah objek yang masuk memiliki tag "Hook"
        {
            objectToActivate.SetActive(true); // Aktifkan GameObject
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                audioSource.PlayOneShot(splashWater); // Memainkan audio jika audio tersedia.
                rb.drag = 5; // Tambahkan drag untuk stabilitas di air
                rb.angularDrag = 5; // Kurangi putaran berlebihan
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hook")) // Periksa apakah objek yang keluar memiliki tag "Hook"
        {
            objectToActivate.SetActive(false); // Nonaktifkan GameObject
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.drag = 1; // Kembalikan drag ke nilai normal
                rb.angularDrag = 1f; // Kembalikan angular drag ke nilai normal
            }
        }
    }
}
