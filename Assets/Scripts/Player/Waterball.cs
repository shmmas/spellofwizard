using UnityEngine;

public class Waterball : MonoBehaviour
{
    public float destroyDelay = 2f; // Waktu sebelum waterball menghilang
    public LayerMask fireLayer; // Layer untuk api

    void Start()
    {
        Destroy(gameObject, destroyDelay); // Hancurkan otomatis setelah beberapa detik
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Waterball terkena: " + collision.gameObject.name); // Debug nama objek yang terkena

        if (((1 << collision.gameObject.layer) & fireLayer) != 0) 
        {
            Debug.Log("🔥 API Terdeteksi! Memadamkan...");
            Destroy(collision.gameObject); // Memadamkan api
            Destroy(gameObject); // Menghilangkan waterball
        }
        else
        {
            Debug.Log("❌ Bukan api, tidak terjadi apa-apa.");
        }
    }
}
