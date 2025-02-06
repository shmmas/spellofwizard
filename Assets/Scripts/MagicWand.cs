using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicWand : MonoBehaviour
{
    public GameObject waterballPrefab; // Prefab waterball
    public Transform shootPoint;       // Posisi keluarnya waterball
    public float shootForce = 15f;     // Kekuatan tembakan
    public LayerMask fireLayer;        // Layer untuk api

    private XRGrabInteractable grabInteractable;
    private bool isHeld = false; // Menyimpan status apakah tongkat dipegang

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Event saat tongkat dipegang atau dilepas
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
            grabInteractable.activated.AddListener(FireWaterball); // Tombol di VR
        }
    }

    void Update()
    {
        // Menembak hanya jika tongkat sedang dipegang
        if (isHeld && Input.GetMouseButtonDown(0)) 
        {
            FireWaterball(null);
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
        if (!isHeld) return; // Tidak bisa menembak jika tidak dipegang

        GameObject waterball = Instantiate(waterballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = waterball.GetComponent<Rigidbody>();

        rb.velocity = shootPoint.forward * shootForce; // Menembak ke depan

        Debug.DrawRay(shootPoint.position, shootPoint.forward * 5, Color.blue, 2f); // Debug arah tembakan
    }
}
