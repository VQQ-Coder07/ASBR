using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rewards : MonoBehaviour
{
    public static rewards instance;
    public Transform parent;
    public Slider bar;
    public Text crownstxt;
    public int[] reqcrowns;
    public int[] coinsrewards;
    public int[] gemsrewards;
    public Animator indicator;
    public int fakecrowns;
    private int crowns()
    {
        return fakecrowns;//7;//PlayerPrefs.GetInt("crowns");
    }
    private void Awake()
    {
        instance = this;
        UpdateRewards();
        this.gameObject.SetActive(false);
    }
    private void Start()
    {
        UpdateRewards();
    }
    private bool ready;
    public Slider menubar;
    private void UpdateRewards()
    {
        bar.value = crowns();
        crownstxt.text = "Crowns: " + crowns().ToString();
        for(int i = 0; i < parent.childCount - 1; i++)
        {
            parent.GetChild(i).GetChild(0).GetComponent<Text>().text = reqcrowns[i].ToString();
            Button btn = parent.GetChild(i).GetComponent<Button>();
            btn.enabled = true;
            Image img = parent.GetChild(i).GetComponent<Image>();
            if(PlayerPrefs.GetInt(string.Format("unlocked{0}", i)) == 1)
            {
                btn.interactable = false;
            }
            else
            {
                if(crowns() < reqcrowns[i])
                {
                    img.color = Color.black;
                    btn.enabled = false;
                }
                else if(crowns() >= reqcrowns[i])
                {
                    ready = true;
                }
            }
        }
        indicator.SetBool("ready", ready);
        ready = false;
    }
    public void unlock(int index)
    {
        if(crowns() >= reqcrowns[index])
        {
            PlayerPrefs.SetInt(string.Format("unlocked{0}", index), 1);
        }
        UpdateRewards();
    }
    public void CollectBox(int i)
    {  
        if(PlayerPrefs.GetInt(string.Format("unlocked{0}", i)) != 0)
        {
            Chests.instance.fromShop = true;
            Chests.instance.OpenChest(i);
            UpdateRewards();
        }
    }
    public void CollectCoins(int i)
    {
        if(PlayerPrefs.GetInt(string.Format("unlocked{0}", i)) != 0)
        {
            GameManager.instance.FAadd("A", coinsrewards[i]);
            UpdateRewards();
        }
    }
    public void CollectGems(int i)
    {
        if(PlayerPrefs.GetInt(string.Format("unlocked{0}", i)) != 0)
        {
            GameManager.instance.FAadd("B", gemsrewards[i]);
            UpdateRewards();
        }
    }
}
