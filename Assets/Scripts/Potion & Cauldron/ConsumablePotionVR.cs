using UnityEngine;
using UnityEngine.XR;

public class ConsumablePotionVR : MonoBehaviour
{
    public enum PotionType { Health, Shield }
    public PotionType potionType;
    public float effectAmount = 30f; // Default efek 30 HP atau Shield

    private InputDevice rightController;

    private void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Update()
    {
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isGripped) && isGripped)
        {
            ConsumePotion();
        }

        if (Input.GetMouseButtonDown(0)) // Klik Kiri Mouse
        {
            ConsumePotion();
        }
    }

    private void ConsumePotion()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>(); // Cari skrip PlayerStats
        if (player == null) return;

        if (potionType == PotionType.Health)
        {
            player.Heal(effectAmount);
            Debug.Log("🍷 [Potion HP] Diminum! Health bertambah " + effectAmount);
        }
        else if (potionType == PotionType.Shield)
        {
            player.AddShield(effectAmount);
            Debug.Log("🛡️ [Potion Shield] Diminum! Shield bertambah " + effectAmount);
        }

        Destroy(gameObject); // Hancurkan potion setelah dikonsumsi
    }
}
