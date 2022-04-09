using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWrap : MonoBehaviour
{
    public string text;
    public float wrapSpeed;
    public float underLineDelay;
    public Text text_prefab;
    private char c;
    private int i = 0;
    private string currentText = "";
    public bool underLine;
    public bool playOnStart;
    public bool waitBefore;
    public float waitTime;
    private string y;
    public bool PlayAudio;
    public AudioSource Sound;
    public float minPitch;
    public float maxPitch;

    void Start()
    {
        if(!waitBefore && playOnStart)
        {
            text_prefab.text = "";
            Repeat();
        }
        else if(waitBefore && !playOnStart)
        {
            text_prefab.text = "";
            Invoke("Repeat", waitTime);
            //Repeat();
        }
        else if(waitBefore && playOnStart)
        {
            Debug.LogError("waitBefore & playOnStart can't be both enabled !");
        }
    }
    void Repeat()
    {
        Invoke("NewLetter", wrapSpeed);
    }
    void NewLetter()
    {
        if(i < text.Length)
        {
            if(PlayAudio && (i < text.Length -1))
            {
                Sound.pitch = Random.Range(minPitch, maxPitch);
                Sound.Play();
            }
            currentText += text[i];
            i++;
            text_prefab.text = currentText;
            Repeat();
        }
        else
        {
            if(underLine)
            {
                if(!currentText.Contains("_"))
                {
                    currentText += "_";
                }
                text_prefab.text = currentText;
                Invoke("UnderLineRepeat", underLineDelay);
            }
        }
    }
    void UnderLineRepeat()
    {
        currentText = currentText.TrimEnd('_');
        text_prefab.text = currentText;
        Invoke("NewLetter", underLineDelay);
    }

}