using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New WeaponCard")]
public class WeaponCard : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;
    public GameObject effect;

    public bool locked;

    //[TextArea(1, 10)]
    //public string cardHistory;

    //public int cardHealth;
    //public int cardAttack;
    //public int cardSpeed;//New
    public int damage;
    public int Range;//New
    public int CoolDown;//New
    public int weaponID;

    //[TextArea(1, 8)]
    //public string cardDescription;

    ////public int experience;

    //public int currentExperience;//MARKER Current EXP INSTEAD OF character's TOTAL EXP

    //public int upgradeCost;

    //public int cardLevel;

}
