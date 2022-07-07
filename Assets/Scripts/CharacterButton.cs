using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public Character m_character;
    public GameObject m_glow;

    public void Awake()
    {
        m_glow = transform.GetChild(0).gameObject;
    }
    public void Equip()
    {
        CardManager.Instance.currentIndex = m_character.m_id;
        UIManager.Instance.PressSelectonHeroButton();
        /*
        if (CanvasAnimationControl.Instance.m_current == 5)
        {
            CardManager.Instance.currentIndex = m_character.m_id;
            UIManager.Instance.PressSelectonHeroButton();
        }
        else if (CanvasAnimationControl.Instance.m_current == 6)
        {
            CardManager.Instance.currentIndex = m_character.m_id;
            UIManager.Instance.PressSelectonHeroButton();
        }*/
    }

    public void SetGlow(bool active)
    {
        m_glow.SetActive(active);
    }
}
