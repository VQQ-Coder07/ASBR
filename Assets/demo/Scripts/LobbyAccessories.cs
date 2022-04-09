using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyAccessories : MonoBehaviour
{
    public GameObject[] buttons;
    public Color highlatedCol;
    public Color disabledCol;
    public Transform accessoriesParent;
    public GameObject unlockItem;
    public Text priceText;
    public MoneyManager _moneyManager;
    public int[] prices;
    public GameObject noMoney;
    private int selected;

    public void Select(int i)
    {
        UpdateOutline();
        unlockItem.SetActive(false);
        if(PlayerPrefs.GetInt("acc_unlocked_" + i.ToString()) == 1)
        {
            UpdateSelected(i);
        }
        else
        {
            ShowUnlock(i);
            ShowSelected(i);
        }
    }
    private int SelectedAccesory()
    {
        return PlayerPrefs.GetInt("SelectedAccesory");
    }
    public void ShowUnlock(int i)
    {
        unlockItem.SetActive(true);
        priceText.text = prices[i].ToString();
        if(_moneyManager.value() >= prices[i])
        {
            priceText.color = Color.green;
        }
        else
        {
            priceText.color = Color.red;
        }
        selected = i;
    }
    public void UpdateUnlock()
    {
        priceText.text = prices[selected].ToString();
        if(_moneyManager.value() >= prices[selected])
        {
            priceText.color = Color.green;
        }
        else
        {
            priceText.color = Color.red;
        }
    }
    public void UnlockItem()
    {
        if(_moneyManager.value() >= prices[selected])
        {
            _moneyManager.Take(prices[selected]);
            PlayerPrefs.SetInt("acc_unlocked_" + selected.ToString(), 1);
        }
        else
        {
            noMoney.SetActive(true);
            Invoke("NoMoneyHide", 0.5f);
        }
        Select(selected);
    }
    private void NoMoneyHide()
    {
        noMoney.SetActive(false);
    }
    private void Start()
    {
        PlayerPrefs.SetInt("acc_unlocked_0", 1);
        UpdateOutline();
        UpdateSelected(SelectedAccesory());
    }
    public void UpdateOutline()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(PlayerPrefs.GetInt("acc_unlocked_" + i.ToString()) == 1)
            {
                buttons[i].GetComponent<UnityEngine.UI.Outline>().enabled = true;
            }
            else
            {
                buttons[i].GetComponent<UnityEngine.UI.Outline>().enabled = false;
            }
        }
    }
    public void UpdateSelected(int value)
    {
        PlayerPrefs.SetInt("SelectedAccesory", value);
        for(int i = 0; i < accessoriesParent.childCount; i++)
        {
            accessoriesParent.GetChild(i).gameObject.SetActive(false);
        }
        foreach(GameObject ob in buttons)
        {
            ob.transform.GetChild(0).gameObject.GetComponent<Image>().color = disabledCol;
        }
        buttons[value].transform.GetChild(0).gameObject.GetComponent<Image>().color = highlatedCol;
        accessoriesParent.GetChild(value).gameObject.SetActive(true);
    }
    public void ShowSelected(int value)
    {
        for(int i = 0; i < accessoriesParent.childCount; i++)
        {
            accessoriesParent.GetChild(i).gameObject.SetActive(false);
        }
        foreach(GameObject ob in buttons)
        {
            ob.transform.GetChild(0).gameObject.GetComponent<Image>().color = disabledCol;
        }
        buttons[value].transform.GetChild(0).gameObject.GetComponent<Image>().color = highlatedCol;
        accessoriesParent.GetChild(value).gameObject.SetActive(true);
    }
}