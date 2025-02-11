using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class DialogSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // Meminta TextMeshPro untuk kalimat dialog.
    public GameObject dialogBox; // Meminta GameObject yang memuat Dialog Box.

    public List<string> dialogLines; // List yang berisi dialog NPC.
    public List<string> respondLines; // List yang berisi dialog respond pada next button

    private int currentLineIndex = 0; // Urutan List dialog.
    private bool isTyping = false; // Tidak sedang mengetik (Karena false).
    public float textSpeed = 0.05f; // Kecepatan teks mengetik.

    public AudioSource audioSource; // Untuk suara ketikan.
    public AudioClip typingSound;   // Audio ketikan.

    private Animator animator;
    public Animator npcAnimator;

    private bool isInteracting = false;
    public GameObject interactButton;
    public GameObject teleportReady;

    private Button dialogButton;
    public TextMeshProUGUI nextButtonText;

    private void Start()
    {
        dialogBox.SetActive(false); // Memastikan GameObject Dialog Box tidak aktif saat permainan dimulai.
        animator = GetComponent<Animator>();

        dialogButton = dialogBox.GetComponent<Button>();
        if (dialogButton != null)
        {
            dialogButton.onClick.AddListener(NextDialog); // Menjadikan dialogBox sebagai tombol
        }
    }

    public void ShowButton()
    {
        if (!isInteracting) // Hanya tampil jika tidak sedang berbicara
        {
            interactButton.SetActive(true);
            return;
        }
    }

    public void StartDialog()
    {
        dialogBox.SetActive(true); // Menaktifkan GameObject ketika fungsi dijalankan.
        currentLineIndex = 0; // Memastikan indeks tetap berada di urutan 0.

        isInteracting = true; // Tandai sedang berbicara
        interactButton.SetActive(false); // Sembunyikan tombol

        animator.SetTrigger("Open");
        npcAnimator.SetTrigger("Talk");

        UpdateNextButtonText();
        StartCoroutine(TypeText(dialogLines[currentLineIndex])); // Memulai efek teks muncul per huruf tanpa menghentikan game.
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true; // Menandakan isTyping menjadi true
        dialogText.text = ""; // Mengosongkan isi GameObject text

        // Menjalankan Looping untuk mengkonversi string menjadi array
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter; // Menambah huruf terus-menerus kedalam GameObject text

            // Mainkan suara ketikan jika ada
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound); // Memainkan audio jika audio tersedia.
            }

            yield return new WaitForSeconds(textSpeed); // Menjeda proses looping. Jeda dapat di ubah pada variabel textSpeed.
        }
        isTyping=false; // Menandakan isTyping menjadi false
        npcAnimator.SetTrigger("Idle");
    }

    public void NextDialog()
    {
        //if (isTyping) return; // jika isTyping = true maka kode selanjutnya dihentikan sampai isTyping = false pada esekusi fungsi NextDialog() selanjutnya

        if (isTyping) 
        {
            StopAllCoroutines(); // Memberhentikan efek ketikan dan menampilkan teks penuh.
            dialogText.text = dialogLines[currentLineIndex];
            isTyping = false; // 
            npcAnimator.SetTrigger("Idle");
        }

        currentLineIndex++; // Menambah nilai index/urutan list.
        if (currentLineIndex < dialogLines.Count)
        {
            npcAnimator.SetTrigger("Talk");

            StartCoroutine(TypeText(dialogLines[currentLineIndex])); // Menjalankan lagi Coroutine
            UpdateNextButtonText();
        }
        else
        {
            isInteracting = false; // Menandai interaksi selesai
            animator.SetTrigger("Close");
            npcAnimator.SetTrigger("Idle");
            StartCoroutine(CloseDialog());
        }
    }
     
    public void UpdateNextButtonText() 
    {
        if (currentLineIndex < respondLines.Count) 
        {
            nextButtonText.text = respondLines[currentLineIndex];
        }
        else
        {
            nextButtonText.text = "Selesai";
        }
    }

    private IEnumerator CloseDialog()
    {
        yield return new WaitForSeconds(0.5f);
        dialogBox.SetActive(false); // Jika indeks sudah sampai ujung maka menonaktifkan GameObject Dialog Box
        teleportReady.SetActive(true);

    }

}
