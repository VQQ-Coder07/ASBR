using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image m_image;
    public Text m_name, m_description;
    public GameObject m_glow;

    public void Equip()
    {
        if (m_glow.activeInHierarchy)
        {
            foreach (Transform t in transform.parent)
            {
                if (t.GetComponent<ItemSlot>().m_image.sprite == ItemCard_Manager.Instance.m_activeItem.cardSprite)
                {
                    ItemCard_Manager.Instance.m_cantEquipTwicePopup.SetActive(true);
                    ItemCard_Manager.Instance.EquipBack();
                    return;
                }
            }
            m_image.sprite = ItemCard_Manager.Instance.m_activeItem.cardSprite;
            m_name.text = ItemCard_Manager.Instance.m_activeItem.cardName;
            m_description.text = ItemCard_Manager.Instance.m_activeItem.card_discription;
            ItemCard_Manager.Instance.SetGlow(false);
            ItemCard_Manager.Instance.m_equipItemPreview.gameObject.SetActive(false);
            ItemCard_Manager.Instance.m_itemsScrollRect.gameObject.SetActive(true);

            int index = 0;
            foreach (ItemCard ic in ItemCard_Manager.Instance.itemCards)
            {
                if (ic == ItemCard_Manager.Instance.m_activeItem)
                {
                    PlayerPrefs.SetInt("Character " + GameManager.instance.playerID + " Item Slot " + transform.GetSiblingIndex(), index);
                    break;
                }
                index++;
            }
        }
    }

    public void Set(ItemCard ic)
    {
        m_image.sprite = ic.cardSprite;
        if (m_name)
            m_name.text = ic.cardName;
        if (m_description)
            m_description.text = ic.card_discription;

        if (m_name && m_description)
        {
            ItemCard_Manager.Instance.SetGlow(false);
            ItemCard_Manager.Instance.m_equipItemPreview.gameObject.SetActive(false);
            ItemCard_Manager.Instance.m_itemsScrollRect.gameObject.SetActive(true);

            int index = 0;
            foreach (ItemCard ic2 in ItemCard_Manager.Instance.itemCards)
            {
                if (ic2 == ic)
                {
                    PlayerPrefs.SetInt("Character " + GameManager.instance.playerID + " Item Slot " + transform.GetSiblingIndex(), index);
                    break;
                }
                index++;
            }
        }
    }
}
