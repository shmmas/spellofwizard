using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    public float fireIntensity = 10f; // Intensitas awal api
    public float extinguishThreshold = 0f; // Batas intensitas untuk memadamkan api
    public ParticleSystem fireParticles; // Partikel api
    public Light fireLight; // Cahaya api

    void Update()
    {
        if (fireIntensity <= extinguishThreshold)
        {
            Extinguish();
        }
    }

    public void ReduceFireIntensity(float amount)
    {
        fireIntensity -= amount;
        fireIntensity = Mathf.Max(fireIntensity, extinguishThreshold);

        // Atur partikel dan cahaya berdasarkan intensitas
        if (fireParticles != null)
        {
            var emission = fireParticles.emission;
            emission.rateOverTime = fireIntensity * 10;
        }

        if (fireLight != null)
        {
            fireLight.intensity = fireIntensity / 10f;
        }
    }

    void Extinguish()
    {
        if (fireParticles != null)
        {
            fireParticles.Stop();
        }

        if (fireLight != null)
        {
            fireLight.enabled = false;
        }

        // Opsional: Hapus objek api setelah padam
        Destroy(gameObject, 2f);
    }
}
