using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "New ItemCard")]
public class ItemCard : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;

    public bool locked;

    public int cardDuration;
    [TextArea(1, 10)]
    public string card_discription;

    public int cardcool;
}
