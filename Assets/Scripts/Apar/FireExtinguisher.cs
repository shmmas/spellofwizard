using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem extinguisherParticles; // Partikel pemadam
    public float extinguishRange = 5f; // Jarak maksimal untuk memadamkan api
    public LayerMask fireLayer; // Layer untuk api
    public float extinguishRate = 1f; // Laju pengurangan intensitas api per detik

    private bool isExtinguishing = false;

    void Update()
    {
        // Periksa apakah tombol pemicu ditekan (disesuaikan dengan perangkat VR)
        if (Input.GetButtonDown("Fire1")) // Ganti "Fire1" sesuai input VR Anda
        {
            StartExtinguishing();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopExtinguishing();
        }

        if (isExtinguishing)
        {
            ExtinguishFire();
        }
    }

    void StartExtinguishing()
    {
        isExtinguishing = true;
        if (extinguisherParticles != null)
        {
            extinguisherParticles.Play();
        }
    }

    void StopExtinguishing()
    {
        isExtinguishing = false;
        if (extinguisherParticles != null)
        {
            extinguisherParticles.Stop();
        }
    }

    void ExtinguishFire()
    {
        Collider[] fires = Physics.OverlapSphere(transform.position, extinguishRange, fireLayer);
        foreach (Collider fire in fires)
        {
            FireBehavior fireBehavior = fire.GetComponent<FireBehavior>();
            if (fireBehavior != null)
            {
                fireBehavior.ReduceFireIntensity(extinguishRate * Time.deltaTime);
            }
        }
    }
}
