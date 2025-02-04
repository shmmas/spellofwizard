using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectibleItem : MonoBehaviour
{
    private QuartzSpawner spawner;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnTaken);
        }
    }

    public void SetSpawner(QuartzSpawner quartzSpawner)
    {
        spawner = quartzSpawner;
    }

    void OnTaken(SelectExitEventArgs args)
    {
        if (spawner != null)
        {
            spawner.ItemTaken();
        }
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.DecreaseItemCount();
        }
    }
}
