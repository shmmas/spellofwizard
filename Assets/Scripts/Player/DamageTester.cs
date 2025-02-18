using UnityEngine;
using UnityEngine.XR;

public class DamageTester : MonoBehaviour
{
    public PlayerStats playerStats;
    private InputDevice rightController;

    private void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Update()
    {
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed) && isPressed)
        {
            playerStats.TakeDamage(10f);
            Debug.Log("🎮 [VR] Primary Button ditekan! Mengurangi 10 HP.");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerStats.TakeDamage(10f);
            Debug.Log("⌨️ [Keyboard] Tombol Space ditekan! Mengurangi 10 HP.");
        }
    }
}
