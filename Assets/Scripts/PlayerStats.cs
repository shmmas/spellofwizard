using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxShield = 50f;
    public float maxMana = 100f;

    private float currentHealth;
    private float currentShield;
    private float currentMana;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        currentMana = maxMana;
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Health: " + currentHealth);
    }

    public void AddShield(float amount)
    {
        currentShield = Mathf.Min(currentShield + amount, maxShield);
        Debug.Log("Shield: " + currentShield);
    }

    public void RestoreMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
        Debug.Log("Mana: " + currentMana);
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            float damageToShield = Mathf.Min(damage, currentShield);
            currentShield -= damageToShield;
            damage -= damageToShield;
        }

        if (damage > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Debug.Log("Player is dead!");
                // Tambahkan logic jika pemain mati
            }
        }

        Debug.Log($"Health: {currentHealth}, Shield: {currentShield}");
    }
}
