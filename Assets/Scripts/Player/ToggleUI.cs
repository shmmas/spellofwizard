using UnityEngine;
using UnityEngine.XR;

public class ToggleUI : MonoBehaviour
{
    public GameObject healthBarUI;
    public GameObject shieldBarUI;
    public XRNode controllerNode = XRNode.RightHand; // Gunakan controller kanan

    private bool isUIVisible = false; // Mulai dalam keadaan tersembunyi

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.isValid)
        {
            bool buttonPressed;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
            {
                ToggleUIVisibility();
            }
        }
    }

    void ToggleUIVisibility()
    {
        isUIVisible = !isUIVisible;
        UpdateUI();
    }

    void UpdateUI()
    {
        healthBarUI.SetActive(isUIVisible);
        shieldBarUI.SetActive(isUIVisible);
    }
}
