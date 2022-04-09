using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardItem : MonoBehaviour
{
    public TextMeshProUGUI unlocked;
    public GameObject unknown;
    private int index;
    public Image preview;
    private bool activated;
    //public Sprite[] previews;

    private void Start()
    {
        if(!activated)
        {
            this.gameObject.layer = 5;
            unlocked = Chests.instance.herotext;
            unknown.SetActive(true);
            preview.gameObject.SetActive(false);
        }
    }
    public void ShowCard()
    {
        //Debug.LogError("727");
        unknown.SetActive(false);
        preview.gameObject.SetActive(true);
        activated = true;
        //preview.sprite = previews[index];
    }
    public void ShowText()
    {
        unlocked.gameObject.GetComponent<Animator>().SetTrigger("fade");
        //unlocked.text = "Unlocked Hero - Bruce";
    }
}
