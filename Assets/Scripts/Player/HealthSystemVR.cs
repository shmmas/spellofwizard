using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HealthSystemVR : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    public float currentHealth; // ✅ Ubah menjadi public agar bisa diakses dari skrip lain

    [Header("Shield Settings")]
    [SerializeField] private float maxShield = 50f;
    public float currentShield; // ✅ Ubah menjadi public agar bisa diakses dari skrip lain

    [Header("UI References")]
    [SerializeField] private Image healthFill; // UI Health Bar
    [SerializeField] private Image shieldFill; // UI Shield Bar

    [Header("Color Gradients")]
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Gradient shieldGradient;

    private InputDevice rightController; // Controller VR tangan kanan

    private void Start()
    {
        // Inisialisasi health dan shield
        currentHealth = maxHealth;
        currentShield = maxShield;

        // Cari controller kanan
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Update UI
        UpdateUI();
    }

    private void Update()
    {
        // Cek apakah tombol primary button ditekan untuk mengurangi health (Testing)
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
        {
            TakeDamage(10f);
            Debug.Log("🎮 [VR] Primary Button ditekan! Mengurangi 10 HP.");
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield; // currentShield bisa bernilai negatif
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);

        UpdateUI();

        Debug.Log($"🔥 Took {damage} damage! HP: {currentHealth}, Shield: {currentShield}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
        Debug.Log($"❤️ Healed {amount} HP! Current HP: {currentHealth}");
    }

    public void AddShield(float amount)
    {
        currentShield += amount;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
        UpdateUI();
        Debug.Log($"🛡️ Added {amount} Shield! Current Shield: {currentShield}");
    }

    public void DrinkPotion(string potionType)
    {
        switch (potionType)
        {
            case "HP":
                Heal(30f);
                Debug.Log("🍷 [Potion HP] Diminum! Health bertambah 30.");
                break;

            case "Shield":
                AddShield(25f);
                Debug.Log("🛡️ [Potion Shield] Diminum! Shield bertambah 25.");
                break;

            case "Poison":
                TakeDamage(20f);
                Debug.Log("☠️ [Potion Racun] Diminum! Health berkurang 20.");
                break;

            default:
                Debug.Log("❌ Potion tidak dikenal.");
                break;
        }
    }

    private void UpdateUI()
    {
        if (healthFill != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthFill.fillAmount = healthPercentage;
            healthFill.color = healthGradient.Evaluate(healthPercentage);
        }

        if (shieldFill != null)
        {
            float shieldPercentage = currentShield / maxShield;
            shieldFill.fillAmount = shieldPercentage;
            shieldFill.color = shieldGradient.Evaluate(shieldPercentage);
        }
    }

    private void Die()
    {
        Debug.Log("💀 Player has died.");
        // Tambahkan logika kematian di sini (misalnya, restart level atau tampilkan layar game over)
    }
}
