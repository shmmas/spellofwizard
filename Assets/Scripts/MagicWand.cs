using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicWand : MonoBehaviour
{
    public GameObject waterballPrefab;
    public Transform shootPoint;
    public float shootForce = 15f;
    public LayerMask fireLayer;

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

        GameObject waterball = Instantiate(waterballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = waterball.GetComponent<Rigidbody>();

        rb.velocity = shootPoint.forward * shootForce;

        Debug.DrawRay(shootPoint.position, shootPoint.forward * 5, Color.blue, 2f);
    }
}
