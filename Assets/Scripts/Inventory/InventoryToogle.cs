using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI; // UI Inventory
    public Transform inventoryAnchor; // Assign ke "Inventory Anchor" di Controller

    private UnityEngine.XR.InputDevice rightController;

    void Start()
    {
        // Dapatkan Right Hand Controller untuk Oculus Quest
        var devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void Update()
    {
        // Inventory selalu mengikuti posisi Inventory Anchor
        if (inventoryAnchor != null)
        {
            inventoryUI.transform.position = inventoryAnchor.position;
            inventoryUI.transform.rotation = inventoryAnchor.rotation;
        }

        // Keyboard (I) atau Mouse Middle Button untuk toggle inventory
        if (Keyboard.current.iKey.wasPressedThisFrame || Mouse.current.middleButton.wasPressedThisFrame)
        {
            ToggleInventory();
        }

        // VR Controller (Tombol B di Oculus)
        if (rightController.isValid && rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
