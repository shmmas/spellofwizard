using UnityEngine;
using UnityEngine.InputSystem;

public class TestDamageInput : MonoBehaviour
{
    public HealthSystemVR healthSystem;
    public float damageAmount = 10f;

    private void Update()
    {
        // Simulate damage input using keyboard (e.g., press 'D' to take damage)
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            healthSystem.TakeDamage(damageAmount);
        }
    }
}