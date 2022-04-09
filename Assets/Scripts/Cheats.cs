using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Cheats : MonoBehaviour
{
    public GameObject button;
    public GameObject panel;
    public Card[] cards;
    public GameManager inst;
    public bool toggled = false;
    public GameObject invalid;
    public GameObject valid;
    public InputField input;
    public string URL;
    
    public void _check()
    {
        StartCoroutine("Check");
    }
    IEnumerator Check()
    {
        WWW w = new WWW(URL);
        yield return w;
        //Debug.LogError(w.text);
        if(w.text.Contains(input.text) && input.text.Length == 19)
        {
            PlayerPrefs.SetInt("cheats", 1);
        }
        CheckBtn();
    }
    private void Start()
    {
        CheckBtn();
    }
    private void CheckBtn()
    {
        if(PlayerPrefs.GetInt("cheats") == 0)
        {
            //panel.SetActive(false);
            //button.SetActive(false);
            //valid.SetActive(false);
            //invalid.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
            button.SetActive(true);
            valid.SetActive(true);
            invalid.SetActive(false);
        }
    }
    public void Toggle()
    {   
        toggled = !toggled;
        panel.SetActive(toggled);
    }
    public void UnlockAll()
    {
        foreach (Card card in cards)
        {
            card.locked = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void FAdd(string item)
    {
//        Debug.LogError("triggered");
        inst.FAadd(item, 100);
    }
}
