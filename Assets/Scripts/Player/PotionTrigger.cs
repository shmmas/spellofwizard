using UnityEngine;

public class PotionTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PotionVR potion = GetComponentInParent<PotionVR>();
            if (potion != null)
            {
                potion.DrinkPotion();
            }
        }
    }
}
