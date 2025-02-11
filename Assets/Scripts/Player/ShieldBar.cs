using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider shieldSlider;
    public PlayerStats playerStats;

    void Update()
    {
        shieldSlider.value = playerStats.shield / playerStats.maxShield;
    }
}
