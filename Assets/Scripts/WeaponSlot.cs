using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image m_image;
    public GameObject m_glow;

    public void Equip()
    {
        if (m_glow.activeInHierarchy)
        {
            foreach (Transform t in transform.parent)
            {
                if (t.GetComponent<WeaponSlot>().m_image.sprite == WeaponCard_Manager.Instance.m_activeWeapon.cardSprite)
                {
                    WeaponCard_Manager.Instance.m_cantEquipTwicePopup.SetActive(true);
                    WeaponCard_Manager.Instance.EquipBack();
                    return;
                }
            }
            m_image.sprite = WeaponCard_Manager.Instance.m_activeWeapon.cardSprite;
            WeaponCard_Manager.Instance.SetGlow(false);
            WeaponCard_Manager.Instance.m_equipWeaponPreview.gameObject.SetActive(false);
            WeaponCard_Manager.Instance.m_weaponsScrollRect.gameObject.SetActive(true);

            switch (transform.GetSiblingIndex())
            {
                case 0:
                    GameManager.instance.weaponOne_Name = m_image.sprite.name;
                    GameManager.instance.weapon_OneSprite = m_image.sprite;
                    break;
                case 1:
                    GameManager.instance.weaponTwo_Name = m_image.sprite.name;
                    GameManager.instance.weapon_TwoSprite = m_image.sprite;
                    break;
                case 2:
                    GameManager.instance.weaponThree_Name = m_image.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = m_image.sprite;
                    break;
            }

            int index = 0;
            foreach (WeaponCard wc in WeaponCard_Manager.Instance.weaponCards)
            {
                if (wc == WeaponCard_Manager.Instance.m_activeWeapon)
                {
                    PlayerPrefs.SetInt("Character " + GameManager.instance.playerID + " Weapon Slot " + transform.GetSiblingIndex(), index);
                    break;
                }
                index++;
            }
        }
    }

    public void Set(WeaponCard wc)
    {
        m_image.sprite = wc.cardSprite;
        WeaponCard_Manager.Instance.SetGlow(false);
        WeaponCard_Manager.Instance.m_equipWeaponPreview.gameObject.SetActive(false);
        WeaponCard_Manager.Instance.m_weaponsScrollRect.gameObject.SetActive(true);

        switch (transform.GetSiblingIndex())
        {
            case 0:
                GameManager.instance.weaponOne_Name = m_image.sprite.name;
                GameManager.instance.weapon_OneSprite = m_image.sprite;
                break;
            case 1:
                GameManager.instance.weaponTwo_Name = m_image.sprite.name;
                GameManager.instance.weapon_TwoSprite = m_image.sprite;
                break;
            case 2:
                GameManager.instance.weaponThree_Name = m_image.sprite.name;
                GameManager.instance.weapon_ThreeSprite = m_image.sprite;
                break;
        }

        int index = 0;
        foreach (WeaponCard wc2 in WeaponCard_Manager.Instance.weaponCards)
        {
            if (wc2 == wc)
            {
                PlayerPrefs.SetInt("Character " + GameManager.instance.playerID + " Weapon Slot " + transform.GetSiblingIndex(), index);
                break;
            }
            index++;
        }
    }
}
