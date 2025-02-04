using UnityEngine;

public class MagicWandController : MonoBehaviour
{
    public GameObject waterSpellPrefab; // Prefab untuk spell air
    public Transform spellSpawnPoint; // Posisi spell muncul
    public float spellSpeed = 10f; // Kecepatan spell

    public GameObject fireObject; // Objek api untuk dimatikan
    public Transform rightHandTransform; // Transform tangan kanan dari XR Device Simulator

    private Vector3 previousHandPosition = Vector3.zero;
    private float gestureThreshold = 0.1f;
    private float gestureCircleProgress = 0f;
    private float totalAngle = 0f;
    private Vector3 gestureStartDirection;

    void Update()
    {
        if (rightHandTransform != null)
        {
            DetectGestureAndCastSpell();
        }
        else
        {
            Debug.LogError("RightHandTransform belum di-assign!");
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Debug dengan tombol Space
        {
            CastWaterSpell();
        }
    }

    private void DetectGestureAndCastSpell()
    {
        Vector3 currentHandPosition = rightHandTransform.position;

        if (previousHandPosition != Vector3.zero)
        {
            Vector3 movement = currentHandPosition - previousHandPosition;
            float distanceMoved = movement.magnitude;

            if (distanceMoved > gestureThreshold)
            {
                Vector3 movementDirection = movement.normalized;
                if (gestureCircleProgress == 0)
                {
                    gestureStartDirection = movementDirection;
                }

                float angle = Vector3.SignedAngle(gestureStartDirection, movementDirection, Vector3.forward);
                totalAngle += Mathf.Abs(angle);
                gestureCircleProgress += distanceMoved;

                Debug.Log($"Angle: {angle}, Total Angle: {totalAngle}");

                if (totalAngle >= 360f && gestureCircleProgress >= 2f)
                {
                    CastWaterSpell();
                    gestureCircleProgress = 0f;
                    totalAngle = 0f;
                    Debug.Log("Gesture lingkaran berhasil! Spell ditembakkan!");
                }
            }
        }

        previousHandPosition = currentHandPosition;
    }

    private void CastWaterSpell()
    {
        Debug.Log("CastWaterSpell() dipanggil!");
        GameObject waterSpell = Instantiate(waterSpellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation);
        Rigidbody rb = waterSpell.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = spellSpawnPoint.forward * spellSpeed;
            Debug.Log("Velocity: " + rb.velocity);
        }
        else
        {
            Debug.LogError("Rigidbody tidak ditemukan di waterSpellPrefab!");
        }

        CheckFireHit(waterSpell);
    }

    private void CheckFireHit(GameObject spell)
    {
        Collider spellCollider = spell.GetComponent<Collider>();
        if (spellCollider != null && fireObject != null)
        {
            Collider fireCollider = fireObject.GetComponent<Collider>();
            if (fireCollider.bounds.Intersects(spellCollider.bounds))
            {
                Destroy(fireObject);
                Debug.Log("Api berhasil dipadamkan!");
            }
        }
    }
}
