using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    private Camera targetCamera;

    public Vector3 offset = new Vector3(0, 0, 5f);


    //Opsi kamera
    [SerializeField]
    private bool followPerspective = true;

    [SerializeField]
    private bool followRotation = true;

    void Start()
    {
        targetCamera = Camera.main;  // Langsung otomatis nyari kamera utama
    }

    void LateUpdate()
    {
        if (targetCamera != null)
        {
            
            if (followPerspective) 
            {
                Vector3 viewportPosition = new Vector3(offset.y, offset.x, offset.z); // Memposisikan UI sesuai offset yg ditentukan
                transform.position = targetCamera.ViewportToWorldPoint(viewportPosition);
            }

            if (followRotation)
            {
                transform.LookAt(transform.position + targetCamera.transform.forward); // Ngubah posisi mengikuti arah kamera
            }
        }
    }
}
