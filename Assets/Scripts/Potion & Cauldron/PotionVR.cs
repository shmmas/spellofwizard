using UnityEngine;

public class PotionVR : MonoBehaviour
{
    public string potionType;
    public float healAmount = 30f;
    public float shieldAmount = 25f;
    public float poisonDamage = 20f;

    public void DrinkPotion()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            HealthSystemVR healthSystem = player.GetComponent<HealthSystemVR>();
            if (healthSystem)
            {
                switch (potionType)
                {
                    case "HP":
                        healthSystem.Heal(healAmount);
                        Debug.Log($"Minum Potion HP! +{healAmount} HP");
                        break;
                    case "Shield":
                        healthSystem.AddShield(shieldAmount);
                        Debug.Log($"Minum Potion Shield! +{shieldAmount} Shield");
                        break;
                    case "Poison":
                        healthSystem.TakeDamage(poisonDamage);
                        Debug.Log($"Minum Potion Racun! -{poisonDamage} HP");
                        break;
                    default:
                        Debug.Log("Potion tidak memiliki efek.");
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}
