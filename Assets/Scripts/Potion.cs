using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Potion : MonoBehaviour
{
    public enum PotionType { Heal, Shield, Speed, Damage }
    public PotionType potionType;
    public float effectValue = 20f;
    public float effectDuration = 5f;

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
        Destroy(gameObject); // Menghapus potion setelah diminum
    }

    void ApplyEffect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        if (player == null) return;

        switch (potionType)
        {
            case PotionType.Heal:
                player.Heal(effectValue);
                break;
        }
    }
}
