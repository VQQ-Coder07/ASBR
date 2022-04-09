using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.Events;
[System.Serializable]
public class IntEvent : UnityEvent<int>
{

}
public class Weapons : MonoBehaviour
{
    public Transform weaponsParent;
    public WeaponCard_Manager wmanager;
    public int[] initdamage;
    public Text[] level;
    public int[] upgradeCost;
    public ParticleSystem part1, part2;
    //public IntEvent onsomeintchange = new IntEvent();
    //public Button fuckingbutton;
    public void Awake()
    {
        //fuckingbutton.onClick.AddListener(new myIntEvent());
    }
    public void Start()
    {
        for (int i = 0; i < level.Length; i++)
        {
            level[i] = weaponsParent.GetChild(i).GetChild(3).GetChild(5).GetChild(2).GetComponent<Text>();
        }
        for (int i = 0; i < initdamage.Length; i++)
        {
            //Debug.LogError(level[i].transform.parent.parent.GetChild(3).GetChild(0));
            int j = i;
            WeaponCard wcard = wmanager.weaponCards[i];
            //Destroy(level[i].transform.parent.parent.GetChild(3).GetChild(0).GetComponent<Button>());
            level[i].transform.parent.parent.GetChild(3).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            level[i].transform.parent.parent.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() => Buy(j));
            level[i].transform.parent.parent.GetChild(3).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            level[i].transform.parent.parent.GetChild(3).GetChild(0).GetComponent<Button>().onClick.AddListener(delegate{wmanager.Equip(wcard);});
            wmanager.weaponCards[i].damage = (initdamage[i] + (PlayerPrefs.GetInt("weaponLvl" + i.ToString()) * 2));
            level[i].text = "Level " + (PlayerPrefs.GetInt("weaponLvl" + i.ToString()) +1).ToString();
        }
        wmanager.SetUpDefultWeapon();
    }
    public void Buy(int item)
    {
        Debug.LogError(item);
        if (GameManager.instance.money >= upgradeCost[item])
        {
            part1.Play();
            part2.Play();
            PlayerPrefs.SetInt("weaponLvl" + item.ToString(), PlayerPrefs.GetInt("weaponLvl" + item.ToString()) + 1);
            GameManager.instance.money -= upgradeCost[item];
            FindObjectOfType<UIManager>().UpdateMoneyUI();
            UpdateUI(item);
        }
    }

    void UpdateUI(int item)
    {
        wmanager.weaponCards[item].damage = (initdamage[item] + (PlayerPrefs.GetInt("weaponLvl" + item.ToString()) * 2));
        //wmanager.weaponCards[item].damage = (initdamage[item] + (PlayerPrefs.GetInt("weaponLvl" + item.ToString()) * 2)).ToString();
        level[item].text = "Level " + PlayerPrefs.GetInt("weaponLvl" + item.ToString()).ToString();
    }
}