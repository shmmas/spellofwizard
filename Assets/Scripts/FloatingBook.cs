using UnityEngine;

public class FloatingBook : MonoBehaviour
{
    public float floatSpeed = 0.5f; // Kecepatan naik turun
    public float floatHeight = 0.1f; // Tinggi osilasi
    public float rotateSpeed = 30f; // Kecepatan rotasi (derajat per detik)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Simpan posisi awal
    }

    void Update()
    {
        // Gerakan melayang naik turun
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // Rotasi perlahan
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
