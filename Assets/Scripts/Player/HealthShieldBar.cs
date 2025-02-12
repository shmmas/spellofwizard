using UnityEngine;
using UnityEngine.UI;

public class HealthShieldBar : MonoBehaviour
{
    public Image healthBar;
    public Image shieldBar;

    private float maxHealth = 100f;
    private float maxShield = 50f;
    private float currentHealth;
    private float currentShield;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        UpdateBars();
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield; // Sisanya kena ke health
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateBars();
    }

    void UpdateBars()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        shieldBar.fillAmount = currentShield / maxShield;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

}
