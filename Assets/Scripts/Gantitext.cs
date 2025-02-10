using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gantitext : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private string _text1;

    [SerializeField]
    private Image gambar1;

    [SerializeField]
    private List<Bahan> Dialog = new List<Bahan>();

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    public void gantitext()
    {
        if (index < Dialog.Count -1 )
        {
            index++;
            _text.text = Dialog[index].play;
            gambar1.sprite = Dialog[index].gambar;
        }
    }
}
