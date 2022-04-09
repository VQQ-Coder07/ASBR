using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Chests : MonoBehaviour
{
    public enum type
    {
        wood, gold, magical
    }
    public type[] _chesttype;
    public GameObject OpenText;
    public TextMeshProUGUI herotext;
    public Animator text;
    public static Chests instance;
    private Chest currentchest;
    public TextMeshProUGUI left;
    public GameObject chestScreen;
    public GameObject mainScreen;
    public Card[] cards;
    public Sprite[] chestsprites;
    public GameObject[] emptyslots;
    public GameObject[] chestslots;
    public Text[] chesttimes;
    public Chest[] chests;
    public BlurLerp bl;
    public Animator fader;
    public bool fromShop;
    public GameObject Shop;
    private void Start()
    {
        LoadChests();
    }
    private void LoadChests()
    {
        foreach(Text j in chesttimes)
        {
            //j.gameObject.SetActive(true);
        }
        foreach(GameObject j in chestslots)
        {
            j.gameObject.SetActive(false);
        }
        foreach(GameObject j in emptyslots)
        {
            j.SetActive(false);
        }
        for(int j = 0; j < emptyslots.Length; j++)
        {
            switch(PlayerPrefs.GetInt(string.Format("slot{0}", j)))
            {
                case 0:
                    emptyslots[j].SetActive(true);
                    break;
                case 1:
                    chestslots[j].SetActive(true);
                    chestslots[j].GetComponent<Image>().sprite = chestsprites[0];
                    break;
                case 2:
                    chestslots[j].SetActive(true);
                    chestslots[j].GetComponent<Image>().sprite = chestsprites[1];
                    break;
                case 3:
                    chestslots[j].SetActive(true);
                    chestslots[j].GetComponent<Image>().sprite = chestsprites[2];
                    break;
            }
        }
    }
    public void AddChest(int id)
    {
        int slot = firstavailableslot();
        if(slot == 4)
        {
            return;
        }
        PlayerPrefs.SetInt(string.Format("slot{0}", slot), id);
        LoadChests();
    }
    public int firstavailableslot()
    {
        if(PlayerPrefs.GetInt("slot0") == 0)
        {
            return 0;
        }
        if(PlayerPrefs.GetInt("slot1") == 0)
        {
            return 1;
        }
        if(PlayerPrefs.GetInt("slot2") == 0)
        {
            return 2;
        }
        if(PlayerPrefs.GetInt("slot3") == 0)
        {
            return 3;
        }
        return 4;
    }
    public int chestnumb()
    {
        int value = 0;
        if(PlayerPrefs.GetInt("slot0") > 0)
        {
            value++;
        }
        if(PlayerPrefs.GetInt("slot1") > 0)
        {
            value++;
        }
        if(PlayerPrefs.GetInt("slot2") > 0)
        {
            value++;
        }
        if(PlayerPrefs.GetInt("slot3") > 0)
        {
            value++;
        }
        return value;
    }
    public bool available()
    {
        bool value = false;
        if(chestnumb() < 4)
        {
            value = true;
        }
        return value;
    }
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        LoadChests();
    }
    public void Open(int slotid)
    {
        int chestid = PlayerPrefs.GetInt(string.Format("slot{0}", slotid));
        chestid--;
        if(_chesttype[chestid] == type.wood)
        {
            currentchest = chests[0];
        }
        else if(_chesttype[chestid] == type.gold)
        {
            currentchest = chests[1];
        }
        else if(_chesttype[chestid] == type.magical)
        {
            currentchest = chests[2];   
        }
        openchest();
    }
    public void OpenChest(int type)
    {
        Shop.SetActive(false);
        switch(type)
        {
            case 0:
                currentchest = chests[0];
                break;
            case 1:
                currentchest = chests[1];
                break;
            case 2:
                currentchest = chests[2];
                break;
        }
        openchest();
    }
    private void openchest()
    {
        fader.gameObject.SetActive(true);
        fader.Play("fadein");
        StartCoroutine("delayedopen", 0.5f);
    }
    public void delayedopen()
    {
        OpenText.SetActive(true);
        bl.gameObject.SetActive(true);
        mainScreen.SetActive(false);
        currentchest.gameObject.SetActive(true);
        chestScreen.SetActive(true);
        left.text = "";
        fader.Play("fadeout2");
        StartCoroutine("DestroyFader", 0.5f);
    }
    public void DestroyFader()
    {
        fader.gameObject.SetActive(false);
    }
    public void Next()
    {
        OpenText.SetActive(false);
        currentchest.next();
    }
    void Awake()
    {
        instance = this;
    }
    public void end()
    {
        bl.gameObject.GetComponent<Animator>().Play("out");
        left.gameObject.SetActive(false);
        mainScreen.SetActive(true);
        currentchest.gameObject.SetActive(false);
        chestScreen.SetActive(false);
        LoadChests();
    }
}