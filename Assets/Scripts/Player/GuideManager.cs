using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuideManager : MonoBehaviour
{
    public TextMeshProUGUI guideText;
    public Button nextButton;
    public Button finishButton;
    public AudioSource clickSound;

    [TextArea(3, 10)]
    public string[] guideLines; // Array berisi teks panduan

    private int currentLine = 0;
    private bool isTyping = false;
    private string currentText = "";

    void Start()
    {
        nextButton.onClick.AddListener(NextLine);
        finishButton.onClick.AddListener(CloseGuide);
        finishButton.gameObject.SetActive(false);

        ShowTextLine(); // Mulai dengan teks pertama
    }

    void ShowTextLine()
    {
        if (currentLine < guideLines.Length)
        {
            StartCoroutine(TypeText(guideLines[currentLine]));
        }
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        guideText.text = "";
        currentText = line;

        foreach (char c in line)
        {
            guideText.text += c;
            yield return new WaitForSeconds(0.05f); // Delay per karakter
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (isTyping)
            return;

        clickSound.Play();

        currentLine++;

        if (currentLine < guideLines.Length)
        {
            ShowTextLine();
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            finishButton.gameObject.SetActive(true);
        }
    }

    void CloseGuide()
    {
        clickSound.Play();
        gameObject.SetActive(false);
    }
}
