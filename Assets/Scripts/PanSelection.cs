using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanSelection : MonoBehaviour
{
    public UnityEvent back;
    public Text txt;
    private string mode, map;
    public bool ismap;
    public void SelectMode(string newmode)
    {
        mode = newmode;
        PlayerPrefs.SetString("mode", mode);
        UpdateText();
        back.Invoke();
    }
    public void SelectMap(string newmap)
    {
        map = newmap;
        PlayerPrefs.SetString("map", newmap);
        UpdateText();
        back.Invoke();
    }
    private void Awake()
    {
        if(!ismap)
        {
            mode = PlayerPrefs.GetString("mode");
            if(mode == "")
            {
                mode = "Solo";
            }
        }
        else
        {
            map =  PlayerPrefs.GetString("map");
            if(map == "")
            {
                map = "Default";
            }
        }
        UpdateText();
    }
    private void UpdateText()
    {
        if(!ismap)
        txt.text = mode;
        else
        txt.text = map;
    }
}
