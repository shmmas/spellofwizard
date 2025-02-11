using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
    public Transform respawnPoint; // Titik respawn (opsional)
    public float fallThreshold = -10f; // Batas jatuh

    private Vector3 lastGroundedPosition; // Posisi terakhir sebelum jatuh
    private bool isGrounded = false; // Apakah player berada di collider yang aman?

    void Update()
    {
        // Cek jika posisi Y player lebih kecil dari fallThreshold
        if (transform.position.y < fallThreshold)
        {
            // Teleport player ke posisi terakhir yang aman atau ke respawnPoint
            if (respawnPoint != null)
            {
                transform.position = respawnPoint.position;
            }
            else
            {
                transform.position = lastGroundedPosition;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Simpan posisi terakhir saat player berada di collider yang aman
        if (collision.collider.CompareTag("Ground")) // Pastikan collider memiliki tag "Ground"
        {
            lastGroundedPosition = transform.position;
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Tandai player tidak lagi berada di collider yang aman
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
