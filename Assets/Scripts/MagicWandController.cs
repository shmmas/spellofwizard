using System.Collections;
using UnityEngine;

public class MagicWandController : MonoBehaviour
{
    public GameObject waterSpellPrefab; // Prefab untuk spell air
    public Transform spellSpawnPoint; // Posisi di mana spell muncul
    public float spellSpeed = 10f; // Kecepatan spell air

    public GameObject fireObject; // Objek api untuk dimatikan

    private Vector3 previousHandPosition; // Posisi tangan sebelumnya
    private float gestureThreshold = 1f; // Ambang gerakan gesture
    private float gestureCircleProgress = 0f; // Kemajuan gesture lingkaran

    void Update()
    {
        DetectGestureAndCastSpell();

            if (Input.GetKeyDown(KeyCode.Space)) // Debug spawn spell
    {
        CastWaterSpell();
    }

    }

    private void DetectGestureAndCastSpell()
    {
        // Ambil posisi tangan (gunakan XR Device Simulator input di sini)
        Vector3 currentHandPosition = InputTrackingHand();

        if (previousHandPosition != Vector3.zero)
        {
            // Hitung perubahan posisi tangan
            Vector3 delta = currentHandPosition - previousHandPosition;

            // Deteksi gerakan melingkar (contoh: lingkaran horizontal)
            if (Mathf.Abs(delta.x) > gestureThreshold || Mathf.Abs(delta.y) > gestureThreshold)
            {
                gestureCircleProgress += delta.magnitude;

                // Jika progress gesture melingkar cukup besar, keluarkan spell
                if (gestureCircleProgress >= 5f)
                {
                    CastWaterSpell();
                    gestureCircleProgress = 0f; // Reset gesture progress
                }
            }
        }

        previousHandPosition = currentHandPosition;
    }

    private Vector3 InputTrackingHand()
    {
        // Simulasikan tangan dengan XR Device Simulator (gunakan transform tangan kanan)
        Transform rightHand = GameObject.Find("Right Controller").transform;
        return rightHand != null ? rightHand.position : Vector3.zero;
    }

    private void CastWaterSpell()
    {
        
        // Spawn spell air
         Debug.Log("CastWaterSpell() dipanggil!"); // Tambahkan ini
        GameObject waterSpell = Instantiate(waterSpellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
        Rigidbody rb = waterSpell.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spellSpawnPoint.forward * spellSpeed;
            Debug.Log("Velocity: " + rb.velocity); // Debugging
        }
           else
        {
            Debug.LogError("Rigidbody tidak ditemukan di waterSpellPrefab!");
        }

        // Hancurkan api jika terkena spell
        CheckFireHit(waterSpell);
    }

    private void CheckFireHit(GameObject spell)
    {
        // Deteksi jika spell mengenai api
        Collider spellCollider = spell.GetComponent<Collider>();
        if (spellCollider != null && fireObject != null)
        {
            Collider fireCollider = fireObject.GetComponent<Collider>();
            if (fireCollider.bounds.Intersects(spellCollider.bounds))
            {
                Destroy(fireObject); // Matikan api
                Debug.Log("Api berhasil dipadamkan!");
            }
        }
    }
}
