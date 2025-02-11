using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Potion : MonoBehaviour
{
    public enum PotionType { Heal, Shield, Speed, Damage }
    public PotionType potionType;
    public float effectValue = 20f;

    private XRGrabInteractable grabInteractable;
    private bool isConsumed = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnDrink);
        }
    }

    void OnDrink(SelectEnterEventArgs args)
    {
        if (isConsumed) return;
        isConsumed = true;
        ApplyEffect();
        Destroy(gameObject); // Potion dihapus setelah diminum
    }

    void ApplyEffect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (player == null)
        {
            Debug.LogWarning("PlayerStats tidak ditemukan!");
            return;
        }

        switch (potionType)
        {
            case PotionType.Heal:
                player.RestoreHealth(effectValue); 
                break;
            case PotionType.Shield:
                player.RestoreShield(effectValue); 
                break;
            default:
                Debug.LogWarning("Efek potion tidak dikenali!");
                break;
        }
    }
}
