using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rate : MonoBehaviour
{
    public Color col;
    public string URL = "https://play.google.com/store/apps/details?id=com.ingka.ikea.app";
    void Start()
    {
        if(PlayerPrefs.GetInt("rated") == 1)
        {
            this.GetComponent<Image>().color = col;
        }
    }
    public void Rate()
    {
        PlayerPrefs.SetInt("rated", 1);
        this.GetComponent<Image>().color = col;
        Application.OpenURL(URL);
    }
}
