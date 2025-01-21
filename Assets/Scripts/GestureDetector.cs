using System.Collections.Generic;
using UnityEngine;

public class GestureDetector : MonoBehaviour
{
    public Transform staffTip; // Titik ujung tongkat
    public float gestureTime = 1.5f; // Durasi maksimum untuk mendeteksi gesture
    public float gestureThreshold = 0.1f; // Toleransi gerakan untuk mendeteksi perubahan

    private List<Vector3> gesturePoints = new List<Vector3>();
    private float gestureTimer = 0f;

    void Update()
    {
        // Rekam posisi tongkat
        if (gestureTimer < gestureTime)
        {
            gesturePoints.Add(staffTip.position);
            gestureTimer += Time.deltaTime;
        }
        else
        {
            // Analisis gesture saat timer habis
            RecognizeGesture();
            gesturePoints.Clear();
            gestureTimer = 0f;
        }
    }

    private void RecognizeGesture()
    {
        // Contoh deteksi gerakan lingkaran
        if (IsCircle(gesturePoints))
        {
            Debug.Log("Gesture Detected: Circle");
            CastSpell("Fireball"); // Cast spell
        }
    }

    private bool IsCircle(List<Vector3> points)
    {
        if (points.Count < 10) return false;

        // Hitung radius rata-rata
        Vector3 center = GetCenter(points);
        float avgRadius = 0f;
        foreach (var point in points)
        {
            avgRadius += Vector3.Distance(point, center);
        }
        avgRadius /= points.Count;

        // Toleransi untuk bentuk lingkaran
        foreach (var point in points)
        {
            if (Mathf.Abs(Vector3.Distance(point, center) - avgRadius) > gestureThreshold)
                return false;
        }
        return true;
    }

    private Vector3 GetCenter(List<Vector3> points)
    {
        Vector3 center = Vector3.zero;
        foreach (var point in points)
        {
            center += point;
        }
        return center / points.Count;
    }

    private void CastSpell(string spellName)
    {
        // Panggil logika spell casting di sini
        Debug.Log($"Casting spell: {spellName}");
    }
}
