using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float shield = 50f;
    public float maxHealth = 100f;
    public float maxShield = 50f;

    void Update()
    {

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            RestoreHealth(10);
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            RestoreShield(10);
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(10);
        }
    }

    public void RestoreHealth(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        Debug.Log("Health restored: " + health);
    }

    public void RestoreShield(float amount)
    {
        shield = Mathf.Min(shield + amount, maxShield);
        Debug.Log("Shield restored: " + shield);
    }

    public void TakeDamage(float amount)
    {
        if (shield > 0)
        {
            shield -= amount;
            if (shield < 0)
            {
                health += shield; // Jika shield habis, damage sisanya ke health
                shield = 0;
            }
        }
        else
        {
            health -= amount;
        }

        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player is dead!");
        }

        Debug.Log($"Health: {health}, Shield: {shield}");
    }
}
