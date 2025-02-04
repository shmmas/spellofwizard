using System.Collections.Generic;
using UnityEngine;

public class GestureDetector : MonoBehaviour
{
    public Transform staffTip; // Titik ujung tongkat
    public float gestureTime = 1.5f; // Durasi maksimum untuk mendeteksi gesture
    public float gestureThreshold = 0.2f; // Lebih longgar untuk variasi gerakan
    public float minRadius = 0.03f; // Radius minimum agar lingkaran kecil bisa dikenali
    public int minPoints = 10; // Jumlah titik minimum untuk deteksi gesture

    private List<Vector3> gesturePoints = new List<Vector3>();
    private float gestureTimer = 0f;

    void Update()
    {
        // Rekam posisi tongkat selama gestureTime detik
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
        if (IsCircle(gesturePoints))
        {
            Debug.Log("✅ Gesture Detected: Circle");
            CastSpell("Fireball");
        }
        else
        {
            Debug.Log("❌ Gesture Not Recognized");
        }
    }

    private bool IsCircle(List<Vector3> points)
    {
        if (points.Count < minPoints) 
        {
            Debug.Log("❌ Gagal: Tidak cukup titik");
            return false;
        }

        Vector3 center = GetCenter(points);
        float totalRadius = 0f;
        float deviation = 0f;

        foreach (var point in points)
        {
            float radius = Vector3.Distance(point, center);
            totalRadius += radius;
        }

        float avgRadius = totalRadius / points.Count;

        foreach (var point in points)
        {
            deviation += Mathf.Abs(Vector3.Distance(point, center) - avgRadius);
        }

        float deviationThreshold = avgRadius * 0.3f; // Lebih longgar

        Debug.Log($"🔍 Checking Gesture: Points={points.Count}, AvgRadius={avgRadius}, Deviation={deviation}, Threshold={deviationThreshold}");

        if (avgRadius < minRadius) 
        {
            Debug.Log("❌ Gagal: Radius terlalu kecil");
            return false;
        }

        if (deviation > deviationThreshold)
        {
            Debug.Log("❌ Gagal: Deviasi terlalu besar");
            return false;
        }

        return true;
    }

    private Vector3 GetCenter(List<Vector3> points)
    {
        if (points.Count == 0) return Vector3.zero;

        Vector3 center = Vector3.zero;
        foreach (var point in points)
        {
            center += point;
        }
        return center / points.Count;
    }

    private void CastSpell(string spellName)
    {
        Debug.Log($"🔥 Casting spell: {spellName}");
    }
}
