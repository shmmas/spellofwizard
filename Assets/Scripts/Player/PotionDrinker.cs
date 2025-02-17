using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PotionDrinker : MonoBehaviour
{
    [Header("Health System Reference")]
    [SerializeField] private HealthSystemVR healthSystem; // Referensi ke HealthSystemVR

    [Header("XR Interaction Settings")]
    [SerializeField] private XRDirectInteractor leftHandInteractor; // Interactor tangan kiri
    [SerializeField] private XRDirectInteractor rightHandInteractor; // Interactor tangan kanan

    [Header("Input Settings")]
    [SerializeField] private InputActionProperty leftTriggerAction; // Input trigger tangan kiri
    [SerializeField] private InputActionProperty rightTriggerAction; // Input trigger tangan kanan

    private void Start()
    {
        // Enable input actions
        leftTriggerAction.action.Enable();
        rightTriggerAction.action.Enable();
    }

    private void Update()
    {
        // Cek input trigger tangan kiri
        if (leftTriggerAction.action.ReadValue<float>() > 0.5f && leftHandInteractor.selectTarget != null)
        {
            DrinkPotion(leftHandInteractor.selectTarget.gameObject);
        }

        // Cek input trigger tangan kanan
        if (rightTriggerAction.action.ReadValue<float>() > 0.5f && rightHandInteractor.selectTarget != null)
        {
            DrinkPotion(rightHandInteractor.selectTarget.gameObject);
        }
    }

    private void DrinkPotion(GameObject potion)
    {
        if (potion.CompareTag("Potion"))
        {
            if (potion.name.Contains("Heal"))
            {
                healthSystem.Heal(20); // Heal 20 poin
                Debug.Log("Meminum Heal Potion. Health bertambah!");
                Destroy(potion); // Hancurkan potion setelah diminum
            }
            else if (potion.name.Contains("Shield"))
            {
                healthSystem.AddShield(20); // Tambah shield 20 poin
                Debug.Log("Meminum Shield Potion. Shield bertambah!");
                Destroy(potion); // Hancurkan potion setelah diminum
            }
            else
            {
                Debug.LogWarning("Jenis potion tidak dikenali: " + potion.name);
            }
        }
    }
}