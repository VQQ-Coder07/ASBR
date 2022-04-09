using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard_Manager : MonoBehaviour
{
    public static ItemCard_Manager Instance;

    public ItemCard[] itemCards;

    [Header("PopUps")]
    public GameObject HeliumPop;
    public GameObject heathShotPop;
    public GameObject boostPops;
    public GameObject kit3Pops;
    public GameObject kit1Pops;
    public GameObject kit2Pops;
    public GameObject kit4Pops;
    public GameObject fireRepellentPops;
    public GameObject snorkelPops;
    public GameObject boostSpeedPops;
    public GameObject strongManPops;
    public GameObject healPops;
    public GameObject backpackPops;
    public GameObject doctoPops;
    public GameObject waterHealingPops;

    [Header("Cards UI")]
    public Text cardNameText;
    public Image[] cardSpriteImages;
    public Image[] selectedWeapon;
    public Button[] buttons;
    public Image spriteImage;

    [Header("SlotOneUI")]
    public Text slotOne_Name;
    public Image slotOne_Image;
    public Text slotOne_Description;
    public GameObject slotOne_Glow;

    [Header("SlotTwoUI")]
    public Text slotTwo_Name;
    public Image slotTwo_Image;
    public Text slotTwo_Description;
    public GameObject slotTwo_Glow;

    [Header("SlotThreeUI")]
    public Text slotThree_Name;
    public Image slotThree_Image;
    public Text slotThree_Description;
    public GameObject slotThree_Glow;

    [Header("SlotFourUI")]
    public Text slotFour_Name;
    public Image slotFour_Image;
    public Text slotFour_Description;
    public GameObject slotFour_Glow;

    [Header("Available Slot")]
    public Image currentSlotOne, currentSlotTwo, currentSlotThree, currentSlotFour;

    public int selectInt;

    #region Ites PopUps
    [Header("Helium UI")]
    public Image cardImages;
    public Text cardName;
    public Text cardDuration;
    public Text cardCooldown;

    [Header("Health Shot UI")]
    public Image shotCardImage;
    public Text shotCardName;
    public Text shotCardDuration;
    public Text shotCardCooldown;

    [Header("BoostWeapon UI")]
    public Image boostCardImage;
    public Text boostCardName;
    public Text boostCardDuration;
    public Text boostCardCooldown;

    [Header("ToolKit3 UI")]
    public Image kit3_CardImage;
    public Text kit3_CardName;
    public Text kit3_CardDuration;
    public Text kit3_CardCooldown;

    [Header("Fire Repellent UI")]
    public Image fireRepellent_CardImage;
    public Text fireRepellent_CardName;
    public Text fireRepellent_CardDuration;
    public Text fireRepellent_CardCooldown;

    [Header("Snorkel UI")]
    public Image snorkel_CardImage;
    public Text snorkel_CardName;
    public Text snorkel_CardDuration;
    public Text snorkel_CardCooldown;

    [Header("Tool Kit 1 UI")]
    public Image kit1_CardImage;
    public Text kit1_CardName;
    public Text kit1_CardDuration;
    public Text kit1_CardCooldown;

    [Header("Tool Kit 2 UI")]
    public Image kit2_CardImage;
    public Text kit2_CardName;
    public Text kit2_CardDuration;
    public Text kit2_CardCooldown;

    [Header("Tool Kit 4 UI")]
    public Image kit4_CardImage;
    public Text kit4_CardName;
    public Text kit4_CardDuration;
    public Text kit4_CardCooldown;


    [Header("Boost Speed UI")]
    public Image boostSpeed_CardImage;
    public Text boostSpeed_CardName;
    public Text boostSpeed_CardDuration;
    public Text boostSpeed_CardCooldown;

    [Header("Strong Man  UI")]
    public Image strongMan_CardImage;
    public Text strongMan_CardName;
    public Text strongMan_CardDuration;
    public Text strongMan_CardCooldown;

    [Header("Heal  UI")]
    public Image heal_CardImage;
    public Text heal_CardName;
    public Text heal_CardDuration;
    public Text heal_CardCooldown;

    [Header("Backpack  UI")]
    public Image backpack_CardImage;
    public Text backpack_CardName;
    public Text backpack_CardDuration;
    public Text backpack_CardCooldown;

    [Header("Doctor")]
    public Image doctor_CardImage;
    public Text doctor_Name;
    public Text doctor_CardDuration;
    public Text doctor_CardCooldown;

    [Header("Water Healing ")]
    public Image waterHealing_CardImage;
    public Text waterHealing_Name;
    public Text waterHealing_CardDuration;
    public Text waterHealing_CardCooldown;

    #endregion

    [Header("Equip Item Previews")]
    public ScrollRect m_itemsScrollRect;
    public Image m_equipItemPreview;
    public ItemCard m_activeItem;

    [Header("Character Buttons")]
    public GameObject m_characterButtonHolder;
    public GameObject m_itemsPopupHolder;
    public GameObject m_itemSlotHolder;

    public GameObject m_cantEquipTwicePopup;

    void Awake()
    {
        Instance = this;
    }

    void Start() {
        SetDefaultItems();
    }

    private void Update() {

    }

    public void Equip(ItemCard item)
    {
        m_activeItem = item;
        m_itemsScrollRect.gameObject.SetActive(false);
        foreach (Transform t in m_itemsPopupHolder.transform)
        {
            t.gameObject.SetActive(false);
        }
        m_equipItemPreview.sprite = item.cardSprite;
        m_equipItemPreview.gameObject.SetActive(true);
        SetGlow(true);
    }

    public void EquipBack()
    {
        foreach (Transform t in m_itemsPopupHolder.transform)
        {
            t.gameObject.SetActive(false);
        }
        m_equipItemPreview.gameObject.SetActive(false);
        SetGlow(false);
        m_itemsScrollRect.gameObject.SetActive(true);
    }

    public void SetGlow(bool active)
    {
        slotOne_Glow.SetActive(active);
        slotTwo_Glow.SetActive(active);
        slotThree_Glow.SetActive(active);
        slotFour_Glow.SetActive(active);
    }

    void SetDefaultItems() {
        int itemLength = itemCards.Length - 1;
        for (int i = 0; i < itemCards.Length; i++) {
            if (itemCards[i].locked == false) {
                cardSpriteImages[i].sprite = itemCards[i].cardSprite;
                cardSpriteImages[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                buttons[i].interactable = true;

                if (itemCards[i].cardName == "Health Shot") {
                    shotCardImage.sprite = itemCards[i].cardSprite;
                    shotCardName.text = itemCards[i].cardName.ToString();
                    shotCardDuration.text = itemCards[i].cardDuration.ToString();
                    shotCardCooldown.text = itemCards[i].cardcool.ToString();
                    slotOne_Image.sprite = itemCards[i].cardSprite;
                    slotOne_Name.text = itemCards[i].cardName.ToString();
                    slotOne_Description.text = itemCards[i].card_discription.ToString();
                }
                else if (itemCards[i].cardName == "Helium") {
                    cardImages.sprite = itemCards[i].cardSprite;
                    cardName.text = itemCards[i].cardName.ToString();
                    cardDuration.text = itemCards[i].cardDuration.ToString();
                    cardCooldown.text = itemCards[i].cardcool.ToString();
                    slotTwo_Image.sprite = itemCards[i].cardSprite;
                    slotTwo_Name.text = itemCards[i].cardName.ToString();
                    slotTwo_Description.text = itemCards[i].card_discription.ToString();
                }
                if (itemCards[i].cardName == "Boost Weapon Range") {
                    boostCardImage.sprite = itemCards[i].cardSprite;
                    boostCardName.text = itemCards[i].cardName.ToString();
                    boostCardDuration.text = itemCards[i].cardDuration.ToString();
                    boostCardCooldown.text = itemCards[i].cardcool.ToString();
                    slotThree_Image.sprite = itemCards[i].cardSprite;
                    slotThree_Name.text = itemCards[i].cardName.ToString();
                    slotThree_Description.text = itemCards[i].card_discription.ToString();
                }
                else if (itemCards[i].cardName == "Tool Kit 3") {
                    kit3_CardImage.sprite = itemCards[i].cardSprite;
                    kit3_CardName.text = itemCards[i].cardName.ToString();
                    kit3_CardDuration.text = itemCards[i].cardDuration.ToString();
                    kit3_CardCooldown.text = itemCards[i].cardcool.ToString();
                    slotFour_Image.sprite = itemCards[i].cardSprite;
                    slotFour_Name.text = itemCards[i].cardName.ToString();
                    slotFour_Description.text = itemCards[i].card_discription.ToString();

                }
                else if (itemCards[i].cardName == "Fire Repellent") {
                    fireRepellent_CardImage.sprite = itemCards[i].cardSprite;
                    fireRepellent_CardName.text = itemCards[i].cardName.ToString();
                    fireRepellent_CardDuration.text = itemCards[i].cardDuration.ToString();
                    fireRepellent_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Snorkel") {
                    snorkel_CardImage.sprite = itemCards[i].cardSprite;
                    snorkel_CardName.text = itemCards[i].cardName.ToString();
                    snorkel_CardDuration.text = itemCards[i].cardDuration.ToString();
                    snorkel_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Tool Kit 1") {
                    kit1_CardImage.sprite = itemCards[i].cardSprite;
                    kit1_CardName.text = itemCards[i].cardName.ToString();
                    kit1_CardDuration.text = itemCards[i].cardDuration.ToString();
                    kit1_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Tool Kit 2") {
                    kit2_CardImage.sprite = itemCards[i].cardSprite;
                    kit2_CardName.text = itemCards[i].cardName.ToString();
                    kit2_CardDuration.text = itemCards[i].cardDuration.ToString();
                    kit2_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Tool Kit 4") {
                    kit4_CardImage.sprite = itemCards[i].cardSprite;
                    kit4_CardName.text = itemCards[i].cardName.ToString();
                    kit4_CardDuration.text = itemCards[i].cardDuration.ToString();
                    kit4_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Boost Speed") {
                    boostCardImage.sprite = itemCards[i].cardSprite;
                    boostCardName.text = itemCards[i].cardName.ToString();
                    boostSpeed_CardDuration.text = itemCards[i].cardDuration.ToString();
                    boostSpeed_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Strong Man ") {
                    strongMan_CardImage.sprite = itemCards[i].cardSprite;
                    strongMan_CardName.text = itemCards[i].cardName.ToString();
                    strongMan_CardDuration.text = itemCards[i].cardDuration.ToString();
                    strongMan_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Heal") {
                    heal_CardImage.sprite = itemCards[i].cardSprite;
                    heal_CardName.text = itemCards[i].cardName.ToString();
                    heal_CardDuration.text = itemCards[i].cardDuration.ToString();
                    heal_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Backpack") {
                    backpack_CardImage.sprite = itemCards[i].cardSprite;
                    backpack_CardName.text = itemCards[i].cardName.ToString();
                    backpack_CardDuration.text = itemCards[i].cardDuration.ToString();
                    backpack_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Doctor") {
                    doctor_CardImage.sprite = itemCards[i].cardSprite;
                    doctor_Name.text = itemCards[i].cardName.ToString();
                    doctor_CardDuration.text = itemCards[i].cardDuration.ToString();
                    doctor_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
                else if (itemCards[i].cardName == "Water Healing ") {
                    waterHealing_CardImage.sprite = itemCards[i].cardSprite;
                    waterHealing_Name.text = itemCards[i].cardName.ToString();
                    waterHealing_CardDuration.text = itemCards[i].cardDuration.ToString();
                    waterHealing_CardCooldown.text = itemCards[i].cardcool.ToString();
                }
            }
            else {
                cardSpriteImages[i].sprite = itemCards[i].cardSprite;
                cardSpriteImages[i].GetComponent<Image>().color = new Color(0, 0, 0, 255);
                buttons[i].interactable = false;
            }
        }
    }

    public void On_Kit3Pop() {
        kit3Pops.SetActive(true);
    }
    public void On_BoostPop() {
        boostPops.SetActive(true);
    }
    public void OnHelimPop() {
        HeliumPop.SetActive(true);
    }
    public void On_HealthPop() {
        heathShotPop.SetActive(true);
    }
    public void On_Kit1Pop() {
        kit1Pops.SetActive(true);
    }
    public void On_Kit2Pop() {
        kit2Pops.SetActive(true);
    }
    public void On_Kit4Pop() {
        kit4Pops.SetActive(true);
    }
    public void On_FireRepellentPops() {
        fireRepellentPops.SetActive(true);
    }
    public void On_SnorkelPop() {
        snorkelPops.SetActive(true);
    }
    public void On_BoostSpeedPop() {
        boostSpeedPops.SetActive(true);
    }
    public void On_StrongManPops() {
        strongManPops.SetActive(true);
    }
    public void On_HealPops() {
        healPops.SetActive(true);
    }
    public void BackPack_Pops() {
        backpackPops.SetActive(true);
    }
    public void DoctorPops() {
        doctoPops.SetActive(true);
    }
    public void WaterHealingPops(){
        waterHealingPops.SetActive(true);
    }

    public void On_Equiped(){

        if(HeliumPop.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = cardImages;
                    slotOne_Image = currentSlotOne;
                    GameManager.instance.item_OneSprite = slotOne_Image.sprite;
                    HeliumPop.SetActive(false);
                }
            }
        }
        else if (heathShotPop.activeSelf){
            heathShotPop.SetActive(false);
        }
        if (boostPops.activeSelf){
            boostPops.SetActive(false);
        }
        else if (kit3Pops.activeSelf){
            kit3Pops.SetActive(false);
        }
    }
}
