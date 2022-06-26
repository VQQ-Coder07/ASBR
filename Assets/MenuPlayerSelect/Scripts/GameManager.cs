using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public enum PlayerSelection1
{
    Shadow = 0, Fatter = 1, Mind = 2, Worm = 3, Harm = 4, Harm2 = 5, Knight = 6, Threesome = 7, Floger = 8,
    Main = 9, Crystal = 10, Wizard = 11, Fivesome = 12, Real = 13, Fighter = 14, Reader = 15, Last = 16, Boss = 17
}

//MARKER This script RUN FIRST EACH TIME
public class GameManager : MonoBehaviour
{
    public bool offline;
    public void FAadd(string item, int value)
    {
        switch (item)
        {
            case "A":
            {
                money += value;
                FindObjectOfType<UIManager>().UpdateMoneyUI();

                return;
            }
            case "B":
            {
                gem += value;
                FindObjectOfType<UIManager>().UpdateMoneyUI();

                return;
            }
        }
    }
    public static GameManager instance;//Singleton Pattern
    private CardManager cardManager;
    private WeaponCard_Manager WeaponCard_Manager;
    private ItemCard_Manager _Manager;

    public int money;
    public int gem;

    public PlayerSelection1 playerSelection1;
    public int playerID;
    public Character m_character;
    //public int weaponID;

    [Header("Character Sprites Resources")]
    public Sprite[] playerSprites;

    [Header("Weapon Sprite")]
    public Sprite weapon_OneSprite;
    public Sprite weapon_TwoSprite;
    public Sprite weapon_ThreeSprite;

    [Header("Item Sprite")]
    public Sprite item_OneSprite;
    public Sprite item_TwoSprite;
    public Sprite item_ThreeSprite;
    public Sprite item_FourSprite;

    [Header("Player Property")]
    public string playerName;
    public int playerHealth;
    public int playerAttack;
    public int playerSpeed; //New
    public int playerRange; //New
    public int playerCool; //New

    [Header("Weapon to Spawn")]
    public string weaponOne_Name;
    public string weaponTwo_Name;
    public string weaponThree_Name;

    [Header("Item Animation to Play")]
    public string ItemOne_Name;
    public string ItemTwo_Name;
    public string ItemThree_Name;
    public string ItemFour_Name;

    public int playerExp;
    public Sprite playerSprite;

    public bool InMain, fromMainMenu, fromCharacter;


    private void Awake(){
        if(offline)
        {
            PhotonNetwork.OfflineMode = true;
        }
        #region Singleton Pattern
        if(instance == null){
            instance = this;
        }
        else{
            if(instance != this){
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        #endregion
    }

    private void Start(){
        cardManager = FindObjectOfType<CardManager>();
        WeaponCard_Manager = FindObjectOfType<WeaponCard_Manager>();
        _Manager = FindObjectOfType<ItemCard_Manager>();

        money = PlayerPrefs.GetInt("Money");
        gem =  PlayerPrefs.GetInt("Gems");
        if(PlayerPrefs.GetInt("startvalue") == 0)
        {
            PlayerPrefs.SetInt("startvalue", 1);
            money = 800;
            gem = 60;
            UIManager.Instance.UpdateMoneyUI();
        }
        if (InMain)
        {/*
            FindObjectOfType<UIManager>().UpdateMoneyUI();//MARKER Have to UPDATE MONEY & GEM
            FindObjectOfType<UIManager>().UpdatePlayerImage();//MARKER Have to UPDATE PLAYER IMAGE


            playerSelection1 = PlayerSelection1.Shadow;//Default Character
            playerID = (int)playerSelection1;
            m_character = Resources.Load<Character>("Characters/Character " + ((((int)playerSelection1) + 1).ToString()));
            

            Debug.Log("Default Character is : " + playerSelection1 + ", ID is " + playerID);

            playerName = cardManager.cards[playerID].cardName;
            playerHealth = cardManager.cards[playerID].cardHealth;
            playerAttack = cardManager.cards[playerID].cardAttack;
            playerSpeed = cardManager.cards[playerID].cardSpeed;//New
            playerRange = cardManager.cards[playerID].cardRange;//New
            playerCool = cardManager.cards[playerID].cardCool;//New
            playerSprite = playerSprites[playerID];
            playerExp = cardManager.cards[playerID].currentExperience;


            cardManager.cards[playerID].currentExperience = playerExp;*/
        }else{
            Debug.Log("Not InMain");
        }
    }

}
