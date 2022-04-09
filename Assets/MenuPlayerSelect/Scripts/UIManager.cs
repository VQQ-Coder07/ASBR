using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private CardManager cardManager;

    public Text[] moneyText, gemsText;
    public Text crownsText;
    public Image playerImage;

    public GameObject soccerPopUp, fireRep_Popups, m_characterPreviewHolder;

    public GameObject m_unifiedSelectionMenuWeaponsHolder, m_unifiedSelectionMenuItemsHolder;

    public Image m_unifiedSelectionMenuCharacterPreview;
    public GameObject playerselection;
    public GameObject playerinformation;
    public GameObject playerpreview;
    public Slider expbar;
    private void Awake()
    {
        Instance = this;
    }

    private void Start(){
        if(PlayerPrefs.GetInt("crowns") < 0)
        {
            PlayerPrefs.SetInt("crowns", 0);
        }
        crownsText.text = PlayerPrefs.GetInt("crowns").ToString();
        cardManager = FindObjectOfType<CardManager>();
        UpdateMoneyUI();
        UpdatePlayerImage();
        PressSelectonHeroButton();
    }

    private void Update(){
        LoadCharacterData();
    }

    public void UpdateMoneyUI(){
        PlayerPrefs.SetInt("Money", GameManager.instance.money);
        PlayerPrefs.SetInt("Gems", GameManager.instance.gem);
        foreach(Text t in moneyText)
        {
            t.text = GameManager.instance.money.ToString();
        }
        foreach(Text t in gemsText)
        {
            t.text = GameManager.instance.gem.ToString();
        }
    }

    public void UpdatePlayerImage(){
        playerImage.sprite = GameManager.instance.playerSprites[GameManager.instance.playerID];
    }

    #region REMOVE from CHARACTER SELECTION script
    //MARKER PRESS one of Characters on the DISPLAY(SECOND) SCREEN
    public void SelectCharacter(int _id){
        FindObjectOfType<CanvasAnimationControl>().SelectHero();
        playerselection.GetComponent<Animator>().ResetTrigger("in");
        playerselection.GetComponent<Animator>().SetTrigger("out");
        cardManager.currentIndex = _id;
        FindObjectOfType<CanvasAnimationControl>().SelectHero();//MARKER MAKE ANIMATION
        cardManager.DisplayInfo();
        cardManager.UpdateLevel();
        cardManager.ShowPageUI();
        playerinformation.GetComponent<Animator>().Play("infoin");
        playerselection.SetActive(false);
        playerinformation.SetActive(true);
        playerpreview.SetActive(true);
        expbar.value = CardManager.Instance.slots[_id].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount;
    }
    public void Back()
    {
        playerselection.SetActive(true);
        playerselection.GetComponent<Animator>().SetTrigger("in");
        playerinformation.GetComponent<Animator>().Play("infoout");
        playerpreview.SetActive(false);
    }
    //MARKER Hero SELECTION_BUTTON on the THIRD SCREEN
    public void PressSelectonHeroButton(){
        if(cardManager.cards[cardManager.currentIndex].locked == false){
            GameManager.instance.playerID = cardManager.currentIndex;
            GameManager.instance.playerSelection1 = (PlayerSelection1)cardManager.currentIndex;
            FindObjectOfType<UIManager>().playerImage.sprite = cardManager.cards[cardManager.currentIndex].cardSprite;
            Debug.Log("PLayer Default Class is : " + GameManager.instance.playerSelection1 + ", And ITS ID is " + GameManager.instance.playerID);

            LoadCharacterData();

            foreach(Transform child in cardManager.previews[1].transform.parent)
            {
                child.gameObject.SetActive(false);
            }
            cardManager.previews[GameManager.instance.playerID].SetActive(true);
            //Destroy(m_characterPreviewHolder.transform.GetChild(0).gameObject);
            //Instantiate(Resources.Load("Character Previews/Player " + (GameManager.instance.playerID + 1)), m_characterPreviewHolder.transform);
            
            GameManager.instance.m_character = Resources.Load<Character>("Characters/Character " + ((((int)cardManager.currentIndex) + 1).ToString()));

            int index = 0;
            foreach (Transform t in WeaponCard_Manager.Instance.m_characterButtonHolder.transform)
            {
                CharacterButton cb = t.GetComponent<CharacterButton>();
                if (index == GameManager.instance.playerID)
                {
                    cb.SetGlow(true);
                    int i;
                    GameManager.instance.m_character.m_weapons = new WeaponCard[3];
                    for (i = 0; i < 3; i++)
                    {
                        if (PlayerPrefs.HasKey("Character " + GameManager.instance.playerID + " Weapon Slot " + i))
                        {
                            GameManager.instance.m_character.m_weapons[i] = WeaponCard_Manager.Instance.weaponCards[PlayerPrefs.GetInt("Character " + GameManager.instance.playerID + " Weapon Slot " + i)];
                        }
                        else
                        {
                            GameManager.instance.m_character.m_weapons[i] = GameManager.instance.m_character.m_defaultWeapons[i];
                        }
                    }

                    i = 0;
                    foreach (WeaponCard ic in cb.m_character.m_weapons)
                    {
                        WeaponCard_Manager.Instance.m_weaponsSlotHolder.transform.GetChild(i).GetComponent<WeaponSlot>().Set(ic);
                        m_unifiedSelectionMenuWeaponsHolder.transform.GetChild(i).GetComponent<WeaponSlot>().Set(ic);
                        i++;
                    }
                }
                else
                {
                    cb.SetGlow(false);
                }
                index++;
            }

            index = 0;
            foreach (Transform t in ItemCard_Manager.Instance.m_characterButtonHolder.transform)
            {
                CharacterButton cb = t.GetComponent<CharacterButton>();
                if (index == GameManager.instance.playerID)
                {
                    cb.SetGlow(true);
                    int i;
                    GameManager.instance.m_character.m_items = new ItemCard[4];
                    for (i = 0; i < 4; i++)
                    {
                        if (PlayerPrefs.HasKey("Character " + GameManager.instance.playerID + " Item Slot " + i))
                        {
                            GameManager.instance.m_character.m_items[i] = ItemCard_Manager.Instance.itemCards[PlayerPrefs.GetInt("Character " + GameManager.instance.playerID + " Item Slot " + i)];
                        }
                        else
                        {
                            GameManager.instance.m_character.m_items[i] = GameManager.instance.m_character.m_defaultItems[i];
                        }
                    }

                    i = 0;
                    foreach (ItemCard ic in cb.m_character.m_items)
                    {
                        ItemCard_Manager.Instance.m_itemSlotHolder.transform.GetChild(i).GetComponent<ItemSlot>().Set(ic);
                        m_unifiedSelectionMenuItemsHolder.transform.GetChild(i).GetComponent<ItemSlot>().Set(ic);
                        i++;
                    }
                }
                else
                {
                    cb.SetGlow(false);
                }
                index++;
            }

            m_unifiedSelectionMenuCharacterPreview.sprite = cardManager.cards[GameManager.instance.playerID].cardSprite;
        }
        else{
            Debug.Log("SORRY, YOU DONT OWN THIS CHARACTER");
        }
    }
    #endregion

    //PRESS the START button on the MENU screen, MOVE to the second Scene(PLAY THE GAME)
    public void StartButton(){
        FindObjectOfType<SceneFader>().FadeTo("01_Game");//MARKER SceneManager.LoadScene("01_Game");
    }

    //MARKER BEFORE you play the game, Load your SELECTED HERO DATA
    private void LoadCharacterData(){
        GameManager.instance.playerName = cardManager.cards[GameManager.instance.playerID].cardName;
        GameManager.instance.playerExp = cardManager.cards[GameManager.instance.playerID].currentExperience;
        GameManager.instance.playerHealth = cardManager.cards[GameManager.instance.playerID].cardHealth;
        GameManager.instance.playerAttack = cardManager.cards[GameManager.instance.playerID].cardAttack;
        GameManager.instance.playerSpeed = cardManager.cards[GameManager.instance.playerID].cardSpeed;//New
        GameManager.instance.playerRange = cardManager.cards[GameManager.instance.playerID].cardRange;//New
        GameManager.instance.playerCool = cardManager.cards[GameManager.instance.playerID].cardCool;//New
        GameManager.instance.playerSprite = GameManager.instance.playerSprites[GameManager.instance.playerID];//MARKER UPDATE player Profile on the first screen
    }
}
