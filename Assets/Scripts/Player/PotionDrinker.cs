using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
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
    [SerializeField] private InputActionProperty rightGripAction; // Input grip tangan kanan

    private GameObject heldPotion; // Potion yang sedang dipegang

    private void Start()
    {
        // Enable input actions
        leftTriggerAction.action.Enable();
        rightTriggerAction.action.Enable();
        rightGripAction.action.Enable();

        // Subscribe ke event ketika objek diambil atau dilepas
        leftHandInteractor.selectEntered.AddListener(OnPotionPickedUp);
        rightHandInteractor.selectEntered.AddListener(OnPotionPickedUp);

        leftHandInteractor.selectExited.AddListener(OnPotionReleased);
        rightHandInteractor.selectExited.AddListener(OnPotionReleased);
    }

    private void Update()
    {
        // Cek input klik kiri mouse (M0)
        if (Input.GetMouseButtonDown(0) )
        {
            DrinkPotionIfHeld();
        }

        // Cek input trigger tangan kanan (Oculus)
        if (rightTriggerAction.action.ReadValue<float>() > 0.5f)
        {
            DrinkPotionIfHeld();
        }

        // Cek input grip tangan kanan (Oculus)
        if (rightGripAction.action.ReadValue<float>() > 0.5f)
        {
            DrinkPotionIfHeld();
        }
    }

    private void OnPotionPickedUp(SelectEnterEventArgs args)
    {
        // Cek apakah objek yang diambil adalah potion
        GameObject pickedObject = args.interactableObject.transform.gameObject;
        if (pickedObject.CompareTag("Potion"))
        {
            Debug.Log("Potion diambil: " + pickedObject.name);
            heldPotion = pickedObject; // Simpan potion yang dipegang
        }
    }

    private void OnPotionReleased(SelectExitEventArgs args)
    {
        // Reset heldPotion saat potion dilepas
        if (args.interactableObject.transform.gameObject == heldPotion)
        {
            heldPotion = null;
        }
    }

    private void DrinkPotionIfHeld()
    {
        if (heldPotion != null)
        {
            DrinkPotion(heldPotion);
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

            heldPotion = null; // Reset heldPotion setelah diminum
        }
    }
}