using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableItem : XRGrabInteractable
{
    public VRInventory inventory; // Referensi ke inventory
    public AudioClip grabSound; // Suara saat mengambil item

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        inventory.PlaySound(grabSound); // Mainkan suara saat mengambil
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        // Jika item dilepas di dekat inventory, simpan ke inventory
        if (Vector3.Distance(transform.position, inventory.transform.position) < 0.5f)
        {
            inventory.StoreItem(gameObject);
        }
    }
}