using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard_Manager : MonoBehaviour
{
    public static WeaponCard_Manager Instance;

    [Header("PopsWindows")]
    public GameObject SoccerPops;
    public GameObject Basketball_Pops;
    public GameObject Football_Pops;
    public GameObject Airsoft_Pops;
    public GameObject Arrow_Pops;
    public GameObject Frisbee_Pops;
    public GameObject Hockey_Pops;
    public GameObject Golf_Pops;
    public GameObject TableTennis_Pops;
    public GameObject Tennis_Pops;
    public GameObject Baseball_Pops;
    public GameObject Volley_Pops;
    public GameObject Bowling_Pops;

    public int currentIndex;
    public int selectInt;

    public WeaponCard[] weaponCards;

    [Header("Cards UI")]
    public Text cardNameText;
    public Image[] cardSpriteImages;
    public Button[] buttons;
    public Image spriteImage;

    [Header("Selected Weapon Slot")]
    public Image slotOneImage;
    public GameObject slotOne_Glow;
    public Image slotTwoImage;
    public GameObject slotTwo_Glow;
    public Image slotThreeImage;
    public GameObject slotThree_Glow;

    [Header("Available Slot")]
    public Image currentSlotOne, currentSlotTwo, currentSlotThree;

    #region Weapon PopUps
    [Header("SoccerPops")]
    public Transform a1;
    public Image soccerspriteImage;
    public Text soccerDamageText;
    public Text soccerRangeText;//New
    public Text soccerCoolText;//New

    [Header("FootballPops")]
    public Transform a2;
    public Image football_spriteImage;
    public Text football_DamageText;
    public Text football_RangeText;//New
    public Text football_CoolText;//New

    [Header("BasketballPops")]
    public Transform a3;
    public Image basketball_spriteImage;
    public Text basketball_DamageText;
    public Text basketball_RangeText;//New
    public Text basketball_CoolText;//New

    [Header("AirsoftPops")]
    public Transform a4;
    public Image airsoft_spriteImage;
    public Text airsoft_DamageText;
    public Text airsoft_RangeText;
    public Text airsoft_CoolText;

    [Header("Arrow")]
    public Transform a5;
    public Image arrow_spriteImage;
    public Text arrow_DamageText;
    public Text arrow_RangeText;
    public Text arrow_CoolText;

    [Header("Frisbee")]
    public Transform a6;
    public Image frisbee_spriteImage;
    public Text frisbee_DamageText;
    public Text frisbee_RangeText;
    public Text frisbee_CoolText;

    [Header("Hockey")]
    public Transform a7;
    public Image hockey_spriteImage;
    public Text hockey_DamageText;
    public Text hockey_RangeText;
    public Text hockey_CoolText;

    [Header("Golf")]
    public Transform a8;
    public Image golf_spriteImage;
    public Text golf_DamageText;
    public Text golf_RangeText;
    public Text golf_CoolText;

    [Header("TableTennis")]
    public Transform a9;
    public Image tableTennis_spriteImage;
    public Text tableTennis_DamageText;
    public Text tableTennis_RangeText;
    public Text tableTennis_CoolText;

    [Header("Tennis")]
    public Transform a10;
    public Image tennis_spriteImage;
    public Text tennis_DamageText;
    public Text tennis_RangeText;
    public Text tennis_CoolText;

    [Header("Baseball")]
    public Transform a11;
    public Image baseball_spriteImage;
    public Text baseball_DamageText;
    public Text baseball_RangeText;
    public Text baseball_CoolText;
    
    [Header("Volleyball")]
    public Transform a12;
    public Image volleyball_spriteImage;
    public Text volleyball_DamageText;
    public Text volleyball_RangeText;
    public Text volleyball_CoolText;

    [Header("Bowling")]
    public Transform a13;
    public Image bowling_spriteImage;
    public Text bowling__DamageText;
    public Text bowling_RangeText;
    public Text bowling_CoolText;
    #endregion

    [SerializeField] private int[] levelNeededeExp = new int[] { 10, 30, 60, 110, 180, 280, 480, 780, 1280, 2280 };

    [Header("New Stuff")]
    public ScrollRect m_weaponsScrollRect;
    public Image m_equipWeaponPreview;
    public WeaponCard m_activeWeapon;

    [Header("Character Buttons")]
    public GameObject m_characterButtonHolder;
    public GameObject m_weaponsPopupHolder;
    public GameObject m_weaponsSlotHolder;

    public GameObject m_cantEquipTwicePopup;

    public void Awake()
    {
        soccerspriteImage = SoccerPops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        soccerRangeText = SoccerPops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        soccerDamageText = SoccerPops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        soccerCoolText = SoccerPops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        football_spriteImage = Football_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        football_RangeText = Football_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        football_DamageText = Football_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        football_CoolText = Football_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        basketball_spriteImage = Basketball_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        basketball_RangeText = Basketball_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        basketball_DamageText = Basketball_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        basketball_CoolText = Basketball_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        airsoft_spriteImage = Airsoft_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        airsoft_RangeText = Airsoft_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        airsoft_DamageText = Airsoft_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        airsoft_CoolText = Airsoft_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        arrow_spriteImage = Arrow_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        arrow_RangeText = Arrow_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        arrow_DamageText = Arrow_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        arrow_CoolText = Arrow_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        frisbee_spriteImage = Frisbee_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        frisbee_RangeText = Frisbee_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        frisbee_DamageText = Frisbee_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        frisbee_CoolText = Frisbee_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        hockey_spriteImage = Hockey_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        hockey_RangeText = Hockey_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        hockey_DamageText = Hockey_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        hockey_CoolText = Hockey_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        golf_spriteImage = Golf_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        golf_RangeText = Golf_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        golf_DamageText  = Golf_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        golf_CoolText = Golf_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        tableTennis_spriteImage = TableTennis_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        tableTennis_RangeText = TableTennis_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        tableTennis_DamageText  = TableTennis_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        tableTennis_CoolText = TableTennis_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        tennis_spriteImage = Tennis_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        tennis_RangeText = Tennis_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        tennis_DamageText  = Tennis_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        tennis_CoolText = Tennis_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        baseball_spriteImage = Baseball_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        baseball_RangeText = Baseball_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        baseball_DamageText = Baseball_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        baseball_CoolText = Baseball_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        volleyball_spriteImage = Volley_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        volleyball_RangeText = Volley_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        volleyball_DamageText  = Volley_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        volleyball_CoolText = Volley_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();

        bowling_spriteImage = Bowling_Pops.transform.GetChild(3).GetChild(5).GetChild(0).GetComponent<Image>();
        bowling_RangeText = Bowling_Pops.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Text>();
        bowling__DamageText  = Bowling_Pops.transform.GetChild(3).GetChild(1).GetChild(2).GetComponent<Text>();
        bowling_CoolText = Bowling_Pops.transform.GetChild(3).GetChild(2).GetChild(2).GetComponent<Text>();
        Instance = this;
    }

    public void Equip(WeaponCard weapon)
    {
        m_activeWeapon = weapon;
        m_weaponsScrollRect.gameObject.SetActive(false);
        foreach (Transform t in m_weaponsPopupHolder.transform)
        {
            t.gameObject.SetActive(false);
        }
        m_equipWeaponPreview.sprite = weapon.cardSprite;
        m_equipWeaponPreview.gameObject.SetActive(true);
        SetGlow(true);
    }

    public void EquipBack()
    {
        foreach (Transform t in m_weaponsPopupHolder.transform)
        {
            t.gameObject.SetActive(false);
        }
        m_equipWeaponPreview.gameObject.SetActive(false);
        SetGlow(false);
        m_weaponsScrollRect.gameObject.SetActive(true);
    }

    public void SetGlow(bool active)
    {
        slotOne_Glow.SetActive(active);
        slotTwo_Glow.SetActive(active);
        slotThree_Glow.SetActive(active);
    }

    void Start(){
        currentIndex = 0;
        SetUpDefultWeapon();
    }

    void Update(){
    }
    public void SetUpDefultWeapon(){
        for(int i = 0; i < weaponCards.Length; i++){
            //Debug.Log(" " + i);
            if(weaponCards[i].locked == false){
                cardSpriteImages[i].sprite = weaponCards[i].cardSprite;
                cardSpriteImages[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                buttons[i].interactable = true;
                if(weaponCards[i].cardName == "Soccer"){
                    soccerspriteImage.sprite = weaponCards[i].cardSprite;
                    soccerDamageText.text = weaponCards[i].damage.ToString();
                    soccerRangeText.text = weaponCards[i].Range.ToString();
                    soccerCoolText.text = weaponCards[i].CoolDown.ToString();
                    slotOneImage.sprite = weaponCards[i].cardSprite;
                    currentSlotOne = slotOneImage;
                    GameManager.instance.weaponOne_Name = weaponCards[i].cardName;
                    GameManager.instance.weapon_OneSprite = weaponCards[i].cardSprite;
                }
                else if(weaponCards[i].cardName == "Football"){
                    football_spriteImage.sprite = weaponCards[i].cardSprite;
                    football_DamageText.text = weaponCards[i].damage.ToString();
                    football_RangeText.text = weaponCards[i].Range.ToString();
                    football_CoolText.text = weaponCards[i].CoolDown.ToString();
                    slotThreeImage.sprite = weaponCards[i].cardSprite;
                    currentSlotTwo = slotThreeImage;
                    GameManager.instance.weaponTwo_Name = weaponCards[i].cardName;
                    GameManager.instance.weapon_TwoSprite = weaponCards[i].cardSprite;
                }
                if(weaponCards[i].cardName == "Basketball"){
                    basketball_spriteImage.sprite = weaponCards[i].cardSprite;
                    basketball_DamageText.text = weaponCards[i].damage.ToString();
                    basketball_RangeText.text = weaponCards[i].Range.ToString();
                    basketball_CoolText.text = weaponCards[i].CoolDown.ToString();
                    slotTwoImage.sprite = weaponCards[i].cardSprite;
                    currentSlotThree = slotTwoImage;
                    GameManager.instance.weaponThree_Name = weaponCards[i].cardName;
                    GameManager.instance.weapon_ThreeSprite = weaponCards[i].cardSprite;
                }
                else if(weaponCards[i].cardName == "Airsoft"){
                    airsoft_spriteImage.sprite = weaponCards[i].cardSprite;
                    airsoft_DamageText.text = weaponCards[i].damage.ToString();
                    airsoft_RangeText.text = weaponCards[i].Range.ToString();
                    airsoft_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                if(weaponCards[i].cardName == "Arrow"){
                    arrow_spriteImage.sprite = weaponCards[i].cardSprite;
                    arrow_DamageText.text = weaponCards[i].damage.ToString();
                    arrow_RangeText.text = weaponCards[i].Range.ToString();
                    arrow_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                else if(weaponCards[i].cardName == "Frisbee"){
                    frisbee_spriteImage.sprite = weaponCards[i].cardSprite;
                    frisbee_DamageText.text = weaponCards[i].damage.ToString();
                    frisbee_RangeText.text = weaponCards[i].Range.ToString();
                    frisbee_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                if(weaponCards[i].cardName == "Hockey"){
                    hockey_spriteImage.sprite = weaponCards[i].cardSprite;
                    hockey_DamageText.text = weaponCards[i].damage.ToString();
                    hockey_RangeText.text = weaponCards[i].Range.ToString();
                    hockey_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                else if(weaponCards[i].cardName == "Golf"){
                    golf_spriteImage.sprite = weaponCards[i].cardSprite;
                    golf_DamageText.text = weaponCards[i].damage.ToString();
                    golf_RangeText.text = weaponCards[i].Range.ToString();
                    golf_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                if(weaponCards[i].cardName == "TableTennis"){
                    tableTennis_spriteImage.sprite = weaponCards[i].cardSprite;
                    tableTennis_DamageText.text = weaponCards[i].damage.ToString();
                    tableTennis_RangeText.text = weaponCards[i].Range.ToString();
                    tableTennis_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                else if(weaponCards[i].cardName == "Tennis"){
                    tennis_spriteImage.sprite = weaponCards[i].cardSprite;
                    tennis_DamageText.text = weaponCards[i].damage.ToString();
                    tennis_RangeText.text = weaponCards[i].Range.ToString();
                    tennis_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                if(weaponCards[i].cardName == "Baseball"){
                    baseball_spriteImage.sprite = weaponCards[i].cardSprite;
                    baseball_DamageText.text = weaponCards[i].damage.ToString();
                    baseball_RangeText.text = weaponCards[i].Range.ToString();
                    baseball_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                else if(weaponCards[i].cardName == "Volleyball"){
                    volleyball_spriteImage.sprite = weaponCards[i].cardSprite;
                    volleyball_DamageText.text = weaponCards[i].damage.ToString();
                    volleyball_RangeText.text = weaponCards[i].Range.ToString();
                    volleyball_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
                if(weaponCards[i].cardName == "Bowling"){
                    bowling_spriteImage.sprite = weaponCards[i].cardSprite;
                    bowling__DamageText.text = weaponCards[i].damage.ToString();
                    bowling_RangeText.text = weaponCards[i].Range.ToString();
                    bowling_CoolText.text = weaponCards[i].CoolDown.ToString();
                }
            }else{
                cardSpriteImages[i].sprite = weaponCards[i].cardSprite;
                cardSpriteImages[i].GetComponent<Image>().color = new Color(0, 0, 0, 255);
                buttons[i].interactable = false;
            }
        }
    }
    void AddWeapon_ToSlot(){
    }
    public void On_SoccerPress(){
        EquipBack();
        SoccerPops.SetActive(true);
    }
    public void On_FootballPress(){
        EquipBack();
        Football_Pops.SetActive(true);
    }
    public void On_BasketPress(){
        EquipBack();
        Basketball_Pops.SetActive(true);
    }
    public void On_AirsoftPress(){
        EquipBack();
        Airsoft_Pops.SetActive(true);
    }
    public void On_ArrowPress(){
        EquipBack();
        Arrow_Pops.SetActive(true);
    }
    public void On_FrisbeePress(){
        EquipBack();
        Frisbee_Pops.SetActive(true);
    }
    public void On_HockeyPress(){
        EquipBack();
        Hockey_Pops.SetActive(true);
    }
    public void On_GolfPress(){
        EquipBack();
        Golf_Pops.SetActive(true);
    }
    public void On_TableTennis_Press(){
        EquipBack();
        TableTennis_Pops.SetActive(true);
    }
    public void On_Tennis(){
        EquipBack();
        Tennis_Pops.SetActive(true);
    }
    public void On_BaseballPress(){
        EquipBack();
        Baseball_Pops.SetActive(true);
    }
    public void On_VolleyballPress(){
        EquipBack();
        Volley_Pops.SetActive(true);
    }
    public void On_BowlingPress(){
        EquipBack();
        Bowling_Pops.SetActive(true);
    }


    public void OnEquiped() {
        #region Soccer PopsChecks
        if (SoccerPops.activeSelf) {
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = soccerspriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Soccer" && currentSlotTwo.sprite.name == "Soccer" || currentSlotOne != null && currentSlotOne.sprite.name != "Soccer" && currentSlotThree.sprite.name == "Soccer"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Soccer" && currentSlotTwo.sprite.name != "Soccer" && currentSlotThree.sprite.name != "Soccer"){
                    currentSlotOne = soccerspriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }
          else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = soccerspriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Soccer" && currentSlotOne.sprite.name == "Soccer" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Soccer" && currentSlotThree.sprite.name == "Soccer"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Soccer" && currentSlotOne.sprite.name != "Soccer" && currentSlotThree.sprite.name != "Soccer"){
                    currentSlotTwo = soccerspriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }
            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = soccerspriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Soccer" && currentSlotOne.sprite.name == "Soccer" || currentSlotThree != null && currentSlotThree.sprite.name != "Soccer" && currentSlotTwo.sprite.name == "Soccer"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Soccer" && currentSlotOne.sprite.name != "Soccer" && currentSlotTwo.sprite.name != "Soccer"){
                    currentSlotThree = soccerspriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            
            SoccerPops.SetActive(false);
        }
        #endregion

        #region Football PopsChecks
        else if (Football_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = football_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Football" && currentSlotTwo.sprite.name == "Football" || currentSlotOne != null && currentSlotOne.sprite.name != "Football" && currentSlotThree.sprite.name == "Football"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Football" && currentSlotTwo.sprite.name != "Football" && currentSlotThree.sprite.name != "Football"){
                    currentSlotOne = football_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = football_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Football" && currentSlotOne.sprite.name == "Football" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Football" && currentSlotThree.sprite.name == "Football"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Football" && currentSlotOne.sprite.name != "Football" && currentSlotThree.sprite.name != "Football"){
                    currentSlotTwo = football_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = football_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Football" && currentSlotOne.sprite.name == "Football" || currentSlotThree != null && currentSlotThree.sprite.name != "Football" && currentSlotTwo.sprite.name == "Football"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Football" && currentSlotOne.sprite.name != "Football" && currentSlotTwo.sprite.name != "Football"){
                    currentSlotThree = football_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Football_Pops.SetActive(false);
        }
        #endregion

        #region Basketball PopsChecks
        if (Basketball_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = basketball_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "basketball" && currentSlotTwo.sprite.name == "basketball" || currentSlotOne != null && currentSlotOne.sprite.name != "basketball" && currentSlotThree.sprite.name == "basketball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "basketball" && currentSlotTwo.sprite.name != "basketball" && currentSlotThree.sprite.name != "basketball"){
                    currentSlotOne = basketball_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

           else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = basketball_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "basketball" && currentSlotOne.sprite.name == "basketball" || currentSlotTwo != null && currentSlotTwo.sprite.name != "basketball" && currentSlotThree.sprite.name == "basketball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "basketball" && currentSlotOne.sprite.name != "basketball" && currentSlotThree.sprite.name != "basketball"){
                    currentSlotTwo = basketball_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = basketball_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;

                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "basketball" && currentSlotOne.sprite.name == "basketball" || currentSlotThree != null && currentSlotThree.sprite.name != "basketball" && currentSlotTwo.sprite.name == "basketball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "basketball" && currentSlotOne.sprite.name != "basketball" && currentSlotTwo.sprite.name != "basketball"){
                    currentSlotThree = basketball_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Basketball_Pops.SetActive(false);
        }
        #endregion

        #region Airsoft_PopsChecks
        else if (Airsoft_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = airsoft_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Airsoft" && currentSlotTwo.sprite.name == "Airsoft" || currentSlotOne != null && currentSlotOne.sprite.name != "Airsoft" && currentSlotThree.sprite.name == "Airsoft"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Airsoft" && currentSlotTwo.sprite.name != "Airsoft" && currentSlotThree.sprite.name != "Airsoft"){
                    currentSlotOne = airsoft_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = airsoft_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Airsoft" && currentSlotOne.sprite.name == "Airsoft" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Airsoft" && currentSlotThree.sprite.name == "Airsoft"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Airsoft" && currentSlotOne.sprite.name != "Airsoft" && currentSlotThree.sprite.name != "Airsoft"){
                    currentSlotTwo = airsoft_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = airsoft_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Airsoft" && currentSlotOne.sprite.name == "Airsoft" || currentSlotThree != null && currentSlotThree.sprite.name != "Airsoft" && currentSlotTwo.sprite.name == "Airsoft"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Airsoft" && currentSlotOne.sprite.name != "Airsoft" && currentSlotTwo.sprite.name != "Airsoft"){
                    currentSlotThree = airsoft_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Airsoft_Pops.SetActive(false);
        }
        #endregion

        #region Arrow_PopsChecks
        if (Arrow_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = arrow_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Archery" && currentSlotTwo.sprite.name == "Archery" || currentSlotOne != null && currentSlotOne.sprite.name != "Archery" && currentSlotThree.sprite.name == "Archery"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Archery" && currentSlotTwo.sprite.name != "Archery" && currentSlotThree.sprite.name != "Archery"){
                    currentSlotOne = arrow_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if (selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = arrow_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Archery" && currentSlotOne.sprite.name == "Archery" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Archery" && currentSlotThree.sprite.name == "Archery"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Archery" && currentSlotOne.sprite.name != "Archery" && currentSlotThree.sprite.name != "Archery"){
                    currentSlotTwo = arrow_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = arrow_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Archery" && currentSlotOne.sprite.name == "Archery" || currentSlotThree != null && currentSlotThree.sprite.name != "Archery" && currentSlotTwo.sprite.name == "Archery"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Archery" && currentSlotOne.sprite.name != "Archery" && currentSlotTwo.sprite.name != "Archery"){
                    currentSlotThree = arrow_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Arrow_Pops.SetActive(false);
        }
        #endregion

        #region Frisbee_PopsChecks
        else if (Frisbee_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = frisbee_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Frisbee" && currentSlotTwo.sprite.name == "Frisbee" || currentSlotOne != null && currentSlotOne.sprite.name != "Frisbee" && currentSlotThree.sprite.name == "Frisbee"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Frisbee" && currentSlotTwo.sprite.name != "Frisbee" && currentSlotThree.sprite.name != "Frisbee"){
                    currentSlotOne = frisbee_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = frisbee_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Frisbee" && currentSlotOne.sprite.name == "Frisbee" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Frisbee" && currentSlotThree.sprite.name == "Frisbee"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Frisbee" && currentSlotOne.sprite.name != "Frisbee" && currentSlotThree.sprite.name != "Frisbee"){
                    currentSlotTwo = frisbee_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponThree_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = frisbee_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Frisbee" && currentSlotOne.sprite.name == "Frisbee" || currentSlotThree != null && currentSlotThree.sprite.name != "Frisbee" && currentSlotTwo.sprite.name == "Frisbee"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Frisbee" && currentSlotOne.sprite.name != "Frisbee" && currentSlotTwo.sprite.name != "Frisbee"){
                    currentSlotThree = frisbee_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Frisbee_Pops.SetActive(false);
        }
        #endregion

        #region Hockey PopChecks
        if (Hockey_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = hockey_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Hockey" && currentSlotTwo.sprite.name == "Hockey" || currentSlotOne != null && currentSlotOne.sprite.name != "Hockey" && currentSlotThree.sprite.name == "Hockey"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Hockey" && currentSlotTwo.sprite.name != "Hockey" && currentSlotThree.sprite.name != "Hockey"){
                    currentSlotOne = hockey_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if (selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = hockey_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Hockey" && currentSlotOne.sprite.name == "Hockey" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Hockey" && currentSlotThree.sprite.name == "Hockey"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Hockey" && currentSlotOne.sprite.name != "Hockey" && currentSlotThree.sprite.name != "Hockey"){
                    currentSlotTwo = hockey_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = hockey_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Hockey" && currentSlotOne.sprite.name == "Hockey" || currentSlotThree != null && currentSlotThree.sprite.name != "Hockey" && currentSlotTwo.sprite.name == "Hockey"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Hockey" && currentSlotOne.sprite.name != "Hockey" && currentSlotTwo.sprite.name != "Hockey"){
                    currentSlotThree = hockey_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Hockey_Pops.SetActive(false);
        }
        #endregion

        #region Golf_PopChecks
        else if (Golf_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = golf_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Golf" && currentSlotTwo.sprite.name == "Golf" || currentSlotOne != null && currentSlotOne.sprite.name != "Golf" && currentSlotThree.sprite.name == "Golf"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Golf" && currentSlotTwo.sprite.name != "Golf" && currentSlotThree.sprite.name != "Golf"){
                    currentSlotOne = golf_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = golf_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Golf" && currentSlotOne.sprite.name == "Golf" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Golf" && currentSlotThree.sprite.name == "Golf"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Golf" && currentSlotOne.sprite.name != "Golf" && currentSlotThree.sprite.name != "Golf"){
                    currentSlotTwo = golf_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = golf_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Golf" && currentSlotOne.sprite.name == "Golf" || currentSlotThree != null && currentSlotThree.sprite.name != "Golf" && currentSlotTwo.sprite.name == "Golf"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Golf" && currentSlotOne.sprite.name != "Golf" && currentSlotTwo.sprite.name != "Golf"){
                    currentSlotThree = golf_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Golf_Pops.SetActive(false);
        }
        #endregion

        #region TableTennis_PopChecks
        if (TableTennis_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = tableTennis_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Tabletennis" && currentSlotTwo.sprite.name == "Tabletennis" || currentSlotOne != null && currentSlotOne.sprite.name != "Tabletennis" && currentSlotThree.sprite.name == "Tabletennis"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Tabletennis" && currentSlotTwo.sprite.name != "Tabletennis" && currentSlotThree.sprite.name != "Tabletennis"){
                    currentSlotOne = tableTennis_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = tableTennis_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Tabletennis" && currentSlotOne.sprite.name == "Tabletennis" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Tabletennis" && currentSlotThree.sprite.name == "Tabletennis"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Tabletennis" && currentSlotOne.sprite.name != "Tabletennis" && currentSlotThree.sprite.name != "Tabletennis"){
                    currentSlotTwo = tableTennis_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = tableTennis_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Tabletennis" && currentSlotOne.sprite.name == "Tabletennis" || currentSlotThree != null && currentSlotThree.sprite.name != "Tabletennis" && currentSlotTwo.sprite.name == "Tabletennis"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Tabletennis" && currentSlotOne.sprite.name != "Tabletennis" && currentSlotTwo.sprite.name != "Tabletennis"){
                    currentSlotThree = tableTennis_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            TableTennis_Pops.SetActive(false);
        }
        #endregion

        #region Tennis_PopChecks
        else if (Tennis_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = tennis_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Tennis" && currentSlotTwo.sprite.name == "Tennis" || currentSlotOne != null && currentSlotOne.sprite.name != "Tennis" && currentSlotThree.sprite.name == "Tennis"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Tennis" && currentSlotTwo.sprite.name != "Tennis" && currentSlotThree.sprite.name != "Tennis"){
                    currentSlotOne = tennis_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = tennis_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Tennis" && currentSlotOne.sprite.name == "Tennis" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Tennis" && currentSlotThree.sprite.name == "Tennis"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Tennis" && currentSlotOne.sprite.name != "Tennis" && currentSlotThree.sprite.name != "Tennis"){
                    currentSlotTwo = tennis_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = tennis_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Tennis" && currentSlotOne.sprite.name == "Tennis" || currentSlotThree != null && currentSlotThree.sprite.name != "Tennis" && currentSlotTwo.sprite.name == "Tennis"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Tennis" && currentSlotOne.sprite.name != "Tennis" && currentSlotTwo.sprite.name != "Tennis"){
                    currentSlotThree = tennis_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Tennis_Pops.SetActive(false);
        }
        #endregion

        #region Baseball_PopChecks
        if (Baseball_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = baseball_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Baseball" && currentSlotTwo.sprite.name == "Baseball" || currentSlotOne != null && currentSlotOne.sprite.name != "Baseball" && currentSlotThree.sprite.name == "Baseball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Baseball" && currentSlotTwo.sprite.name != "Baseball" && currentSlotThree.sprite.name != "Baseball"){
                    currentSlotOne = baseball_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = baseball_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Baseball" && currentSlotOne.sprite.name == "Baseball" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Baseball" && currentSlotThree.sprite.name == "Baseball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Baseball" && currentSlotOne.sprite.name != "Baseball" && currentSlotThree.sprite.name != "Baseball"){
                    currentSlotTwo = baseball_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = baseball_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Baseball" && currentSlotOne.sprite.name == "Baseball" || currentSlotThree != null && currentSlotThree.sprite.name != "Baseball" && currentSlotTwo.sprite.name == "Baseball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Baseball" && currentSlotOne.sprite.name != "Baseball" && currentSlotTwo.sprite.name != "Baseball"){
                    currentSlotThree = baseball_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Baseball_Pops.SetActive(false);
        }
        #endregion

        #region Volleyball_PopCheck
        else if (Volley_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = volleyball_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Volleyball" && currentSlotTwo.sprite.name == "Volleyball" || currentSlotOne != null && currentSlotOne.sprite.name != "Volleyball" && currentSlotThree.sprite.name == "Volleyball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Volleyball" && currentSlotTwo.sprite.name != "Volleyball" && currentSlotThree.sprite.name != "Volleyball"){
                    currentSlotOne = volleyball_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = volleyball_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Volleyball" && currentSlotOne.sprite.name == "Volleyball" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Volleyball" && currentSlotThree.sprite.name == "Volleyball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Volleyball" && currentSlotOne.sprite.name != "Volleyball" && currentSlotThree.sprite.name != "Volleyball"){
                    currentSlotTwo = volleyball_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = volleyball_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Volleyball" && currentSlotOne.sprite.name == "Volleyball" || currentSlotThree != null && currentSlotThree.sprite.name != "Volleyball" && currentSlotTwo.sprite.name == "Volleyball"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Volleyball" && currentSlotOne.sprite.name != "Volleyball" && currentSlotTwo.sprite.name != "Volleyball"){
                    currentSlotThree = volleyball_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Volley_Pops.SetActive(false);
        }
        #endregion

        #region Bowling
        if (Bowling_Pops.activeSelf){
            if(selectInt == 0){
                if(currentSlotOne == null){
                    currentSlotOne = bowling_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Bowling" && currentSlotTwo.sprite.name == "Bowling" || currentSlotOne != null && currentSlotOne.sprite.name != "Bowling" && currentSlotThree.sprite.name == "Bowling"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotOne != null && currentSlotOne.sprite.name != "Bowling" && currentSlotTwo.sprite.name != "Bowling" && currentSlotThree.sprite.name != "Bowling"){
                    currentSlotOne = bowling_spriteImage;
                    slotOneImage.sprite = currentSlotOne.sprite;
                    GameManager.instance.weaponOne_Name = slotOneImage.sprite.name;
                    GameManager.instance.weapon_OneSprite = slotOneImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 1){
                if(currentSlotTwo == null){
                    currentSlotTwo = bowling_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Bowling" && currentSlotOne.sprite.name == "Bowling" || currentSlotTwo != null && currentSlotTwo.sprite.name != "Bowling" && currentSlotThree.sprite.name == "Bowling"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotTwo != null && currentSlotTwo.sprite.name != "Bowling" && currentSlotOne.sprite.name != "Bowling" && currentSlotThree.sprite.name != "Bowling"){
                    currentSlotTwo = bowling_spriteImage;
                    slotTwoImage.sprite = currentSlotTwo.sprite;
                    GameManager.instance.weaponTwo_Name = slotTwoImage.sprite.name;
                    GameManager.instance.weapon_TwoSprite = slotTwoImage.sprite;
                }
                selectInt += 1;
            }

            else if(selectInt == 2){
                if(currentSlotThree == null){
                    currentSlotThree = bowling_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Bowling" && currentSlotOne.sprite.name == "Bowling" || currentSlotThree != null && currentSlotThree.sprite.name != "Bowling" && currentSlotTwo.sprite.name == "Bowling"){
                    Debug.Log("Already have this weapon in other slot");
                }
                else if(currentSlotThree != null && currentSlotThree.sprite.name != "Bowling" && currentSlotOne.sprite.name != "Bowling" && currentSlotTwo.sprite.name != "Bowling"){
                    currentSlotThree = bowling_spriteImage;
                    slotThreeImage.sprite = currentSlotThree.sprite;
                    GameManager.instance.weaponThree_Name = slotThreeImage.sprite.name;
                    GameManager.instance.weapon_ThreeSprite = slotThreeImage.sprite;
                }
                selectInt = 0;
            }
            Bowling_Pops.SetActive(false);
        }
        #endregion
    }
}
