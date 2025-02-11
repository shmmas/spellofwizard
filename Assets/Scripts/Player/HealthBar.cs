using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerStats playerStats;

    void Update()
    {
        healthSlider.value = playerStats.health / playerStats.maxHealth;
    }
}
