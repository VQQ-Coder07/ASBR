using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public LobbyAccessories[] showUnlock;

    public int value()
    {
        return PlayerPrefs.GetInt("Money");
    }
    public Text moneyCounter;

    void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        foreach (LobbyAccessories mb in showUnlock)
        {
           mb.UpdateUnlock();
        }
        moneyCounter.text = value().ToString();
    }
    public void Add(int val)
    {
        PlayerPrefs.SetInt("Money", (value() + val));
        UpdateText();
    }

    public void Take(int val)
    {
        PlayerPrefs.SetInt("Money", (value() - val));
        UpdateText();
    }
}
