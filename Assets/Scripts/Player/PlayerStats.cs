using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerStats : MonoBehaviour
{
    [Header("Health & Shield Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxShield = 50f;

    public float currentHealth;
    public float currentShield;

    [Header("UI References")]
    [SerializeField] private Image healthFill;
    [SerializeField] private Image shieldFill;
    [SerializeField] private Gradient healthGradient;
    [SerializeField] private Gradient shieldGradient;

    private void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield; // Jika shield negatif, kurangi health
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
        Debug.Log($"🔥 Damage Taken: {damage} | HP: {currentHealth} | Shield: {currentShield}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateUI();
        Debug.Log($"❤️ Healed {amount} HP | Current HP: {currentHealth}");
    }

    public void AddShield(float amount)
    {
        currentShield = Mathf.Clamp(currentShield + amount, 0, maxShield);
        UpdateUI();
        Debug.Log($"🛡️ Shield Added: {amount} | Current Shield: {currentShield}");
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
        // Tambahkan logika kematian (restart level, game over, dll.)
    }
}
