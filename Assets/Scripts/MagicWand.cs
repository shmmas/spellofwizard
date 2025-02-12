using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicWand : MonoBehaviour
{
    public GameObject waterballPrefab;
    public Transform shootPoint;
    public float shootForce = 15f;
    public LayerMask fireLayer;

    [Header("Audio Settings")]
    public AudioSource audioSource;  // Komponen AudioSource
    public AudioClip fireSound;  // Suara saat menembak

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.activated.AddListener(FireWaterball);
        }

        // Pastikan AudioSource ada di objek ini
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void FireWaterball(ActivateEventArgs args)
    {
        if (!isHeld) return;

        // Instansiasi Waterball
        GameObject waterball = Instantiate(waterballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = waterball.GetComponent<Rigidbody>();

        rb.velocity = shootPoint.forward * shootForce;

        // Memainkan suara spell jika ada
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }

        // Debug untuk melihat arah tembakan
        Debug.DrawRay(shootPoint.position, shootPoint.forward * 5, Color.blue, 2f);
    }
}
