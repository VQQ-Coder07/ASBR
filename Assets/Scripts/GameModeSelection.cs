using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameModeSelection : MonoBehaviour
{
    public UnityEvent back;
    public Text modetxt;
    private string mode;
    public void SelectMode(string newmode)
    {
        mode = newmode;
        PlayerPrefs.SetString("mode", mode);
        UpdateText();
        back.Invoke();
    }
    private void Awake()
    {
        mode = PlayerPrefs.GetString("mode");
        if(mode == "")
        {
            mode = "Solo";
        }
        UpdateText();
    }
    private void UpdateText()
    {
        modetxt.text = mode;
    }
}
