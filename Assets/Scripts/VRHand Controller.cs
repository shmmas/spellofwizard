using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandController : MonoBehaviour
{
    public Animator handAnimator;

    [Header("Input Axis Names")]
    public string gripAxisName = "Grip_Right";   // Nama untuk input tangan kanan
    public string triggerAxisName = "Trigger_Right"; // Nama untuk input tangan kanan

    public bool isLeftHand; // Centang ini jika script digunakan untuk tangan kiri

    void Start()
    {
         // Jika tangan kiri, gunakan parameter dan input axis untuk tangan kiri
        if (isLeftHand)
        {
            gripAxisName = "Grip_Left";
            triggerAxisName = "Trigger_Left";
        }
    }

    void Update()
    {
        float gripValue = Input.GetKey(KeyCode.G) ? 1.0f : 0.0f;  // Ganti dengan input Anda
        float triggerValue = Input.GetKey(KeyCode.T) ? 1.0f : 0.0f;  // Ganti dengan input Anda

        handAnimator.SetFloat(gripAxisName, gripValue);
        handAnimator.SetFloat(triggerAxisName, triggerValue);
    }
}
