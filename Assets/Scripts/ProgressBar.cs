using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar; // Slider untuk progress bar
    public float progressIncreaseRate = 20f; // Kecepatan penambahan progress
    public float progressDecreaseRate = 5f; // Kecepatan pengurangan progress
    private float currentProgress = 0f; // Nilai progress saat ini
    private bool isButtonPressed = false; // Status apakah tombol UI sedang ditekan

    [SerializeField]
    private GameObject objectToShow; // GameObject yang akan dimunculkan

    [SerializeField]
    public List<UnityEvent> functionList = new List<UnityEvent>();

    [SerializeField]
    private Transform bobberPosition; // Sebagai referensi untuk properti Transform 

    private void Start()
    {
        objectToShow.SetActive(true);
    }

    void Update()
    {
        // Jika tombol sedang ditekan, tambahkan progress
        if (isButtonPressed)
        {
            currentProgress += progressIncreaseRate * Time.deltaTime;
        }
        else
        {
            // Jika tombol tidak ditekan, kurangi progress secara otomatis
            currentProgress -= progressDecreaseRate * Time.deltaTime;
        }

        // Batasi progress agar tetap dalam rentang 0 - 100
        currentProgress = Mathf.Clamp(currentProgress, 0f, 100f);

        // Perbarui nilai pada slider (progress bar)
        progressBar.value = currentProgress;

        if (currentProgress >= 100f)
        {
            // Mengaktifkan GameObject ketika bar mencapai 100%
            Instantiate(objectToShow, bobberPosition.position, bobberPosition.rotation); // Memunculkan prefab dari posisi dan rotasi GameObject Bobber
            Execute();
            Debug.Log("Bar Max");
            currentProgress = 0f; // Mereset progress bar saat mencapai 100%
        }
    }

    // Fungsi ini dipanggil saat tombol Target diklik
    public void OnButtonPressed()
    {
        isButtonPressed = true;
        Invoke(nameof(StopButtonPress), 0.1f); // Berhenti setelah waktu pendek untuk meniru klik
    }

    // Fungsi untuk menghentikan tombol setelah ditekan
    private void StopButtonPress()
    {
        isButtonPressed = false;
    }

    public void Release()
    {
        objectToShow.SetActive(false);
    }

    void Execute()
    {
        foreach (var unityEvent in functionList)
        {
            unityEvent.Invoke();
        }
    }

}
