using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cam);
        transform.Rotate(0, 180, 0); // Supaya tidak terbalik
    }
}
