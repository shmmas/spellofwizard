using UnityEngine;
using UnityEngine.UI;

public class HealthSystemVR : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("Shield Settings")]
    [SerializeField] private float maxShield = 50f;
    private float currentShield;

    [Header("UI References")]
    [SerializeField] private Image healthFill; // Referensi ke UI Health Bar Fill
    [SerializeField] private Image shieldFill; // Referensi ke UI Shield Bar Fill

    [Header("Color Gradients")]
    [SerializeField] private Gradient healthGradient; // Gradient untuk health bar
    [SerializeField] private Gradient shieldGradient; // Gradient untuk shield bar

    private void Start()
    {
        // Inisialisasi health dan shield
        currentHealth = maxHealth;
        currentShield = maxShield;

        // Update UI
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        // Kurangi shield terlebih dahulu
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                // Jika shield habis, kurangi health dengan sisa damage
                currentHealth += currentShield; // currentShield bernilai negatif
                currentShield = 0;
            }
        }
        else
        {
            // Jika shield habis, kurangi health
            currentHealth -= damage;
        }

        // Pastikan health dan shield tidak kurang dari 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);

        // Update UI
        UpdateUI();

        // Debug log untuk testing
        Debug.Log($"Took {damage} damage. Health: {currentHealth}, Shield: {currentShield}");

        // Cek jika health habis
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Pastikan health tidak melebihi maxHealth
        UpdateUI();
        Debug.Log($"Healed {amount} points. Current Health: {currentHealth}");
    }

    public void AddShield(float amount)
    {
        currentShield += amount;
        currentShield = Mathf.Clamp(currentShield, 0, maxShield); // Pastikan shield tidak melebihi maxShield
        UpdateUI();
        Debug.Log($"Added {amount} shield points. Current Shield: {currentShield}");
    }

    private void UpdateUI()
    {
        // Update health bar fill amount dan warna
        if (healthFill != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthFill.fillAmount = healthPercentage;
            healthFill.color = healthGradient.Evaluate(healthPercentage); // Atur warna berdasarkan gradient
        }

        // Update shield bar fill amount dan warna
        if (shieldFill != null)
        {
            float shieldPercentage = currentShield / maxShield;
            shieldFill.fillAmount = shieldPercentage;
            shieldFill.color = shieldGradient.Evaluate(shieldPercentage); // Atur warna berdasarkan gradient
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Tambahkan logika kematian di sini (misalnya, restart level atau tampilkan layar game over)
    }
}