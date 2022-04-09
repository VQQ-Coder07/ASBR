using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Character")]
public class Character : ScriptableObject
{
    public int m_id;
    public Sprite m_sprite;
    public ItemCard[] m_defaultItems;
    public ItemCard[] m_items;
    public WeaponCard[] m_defaultWeapons;
    public WeaponCard[] m_weapons;
}
