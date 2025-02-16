using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;  
    public Slider shieldSlider;  
    public Transform cameraTransform; // Transform kamera agar HealthBar mengikutinya

    private float maxHealth = 100f;
    private float currentHealth;
    private float maxShield = 100f;
    private float currentShield;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        shieldSlider.maxValue = maxShield;
        shieldSlider.value = currentShield;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // Gunakan Lerp agar pergerakan lebih smooth
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * 2f + Vector3.up * 0.5f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

            // Menghadap kamera agar selalu terlihat
            transform.LookAt(cameraTransform);
            transform.Rotate(0, 180, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            shieldSlider.value = currentShield;
            Debug.Log("🛡️ Shield terkena damage: " + damage + " | Sisa Shield: " + currentShield);
        }
        else
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            Debug.Log("❤️ Health terkena damage: " + damage + " | Sisa Health: " + currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Player Mati!");
    }
}
