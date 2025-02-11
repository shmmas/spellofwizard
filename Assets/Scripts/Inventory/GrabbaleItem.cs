using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableItem : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        // Item diambil oleh interactor
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        // Item dilepas
    }
}