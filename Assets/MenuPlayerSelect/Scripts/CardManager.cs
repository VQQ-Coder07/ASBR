using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject locks;
    public Slider upgrade;
    public static CardManager Instance;

    public WeaponCard_Manager weaponCard;
    public ItemCard_Manager itemCard;
    public int currentIndex;

    public bool InMain;
     
    public Card[] cards;

    [Header("Cards UI")]
    public Text cardNameText;
    public Image cardSpriteImage;
    public Image characterImage;
    public Image weaponOne;
    public Image weaponTwo;
    public Image weaponThree;
    public Image itemOne;
    public Image itemTwo;
    public Image itemThree;
    public Image itemFour;
    public Sprite notAvailabeImage;
    public Text cardHisText;
    public Text cardHealthText;
    public Text cardAttText;
    public Text cardSpeedText;//New
    public Text cardRangeText;//New
    public Text cardCoolText;//New
    public Text cardDesText;
    public Text cardLvText; 
     
    [SerializeField] public int[] levelNeededeExp = new int[] { 10, 30, 60, 110, 180, 280, 480, 780, 1280, 2280};

    [Header("CharacterInfo Slots")]
    public GameObject[] slots;
    public GameObject[] charSlots;
    public GameObject[] itemCharSlots, previews, infopreviews;

    //MARKER SELECT BUTTON ON THE THIRD SCREEN, JUST used for showing active/inactive(Color) Hero
    public Image SelectButton;
    public Image histroyImage;//CHARACTER HISTORY WINDOW when Mouse POINT ENTER or EXIT

    public Text titleText;//"HERO SELECTION XX/18" ON THE TOP OF THE SECOND SCREEN
    public Text pageText;//"X/18" PAGE UI on the THIRD SCREEN (TOP RIGHT CORNER)

    private int healthIncrement = 100;//TODO Customize increment curver later
    private int attackIncrement = 250;//TODO Customize increment curver later 
    private int speedIncrement = 250;//TODO Customize increment curver later //New
    private int rangeIncrement = 250;//TODO Customize increment curver later //New
    private int coolIncrement = 250;//TODO Customize increment curver later //New

    [SerializeField] private bool CanUpgradeHero;
    public Text healthIncrementText;
    public Text attackIncrementText;
    public Text speedIncrementText;//New
    public Text rangeIncrementText;//New
    public Text coolIncrementText;//New


    [Header("VFX")]
    public ParticleSystem heroLevelupEffect;
    public ParticleSystem healthLevelUpEffect;
    public ParticleSystem attackLevelupEffect;
    public ParticleSystem speedLevelupEffect;//New //particle effects 
    public ParticleSystem rangeLevelupEffect;//New //particle effects 
    public ParticleSystem coolLevelupEffect;//New //particle effects 


    public Animator tipAnim;

    private void Awake()
    {
        Instance = this;
    }
    public void UpdateXpBar()
    {
        upgrade.value = (float)cards[currentIndex].currentExperience / levelNeededeExp[0];
    }
    private void Start(){
        weaponCard = GameObject.FindObjectOfType<WeaponCard_Manager>();
        itemCard = GameObject.FindObjectOfType<ItemCard_Manager>();

        currentIndex = 0;
        histroyImage.gameObject.SetActive(false);//DEFAULT histroy Image is turn off(SetActive to be false)

        ShowPageUI();
        DisplayInfo();
        if (InMain){
            DisplayCharacterInfo();
        }
        DisplayLevelBar();

        DisplayCharacters();
        DisplayAvailableCharacter();
        DisplayItemCharOpen();
    }

    private void Update(){
        //DisplayLevelBar();TODO REMOVE LATER
        CanUpgrade();//TODO REMOVE LATER
    }

    //MARKER CALL on BUTTON EVENT LISTENER
    public void NextButton(){
        currentIndex++;
        if(currentIndex >= cards.Length){
            currentIndex = 0;
        }

        UpdateLevel();
        DisplayInfo();
        if(InMain){
            DisplayCharacterInfo();
        }
        ShowPageUI();
    }

    //MARKER CALL on BUTTON EVENT LISTENER
    public void BackButton(){
        currentIndex--;
        if(currentIndex < 0){
            currentIndex = cards.Length - 1;
        }

        UpdateLevel();
        DisplayInfo();//MARKER Each time When you PRESS NEXT or BACK button, DIAPLY CARD making sure current Card is WHAT YOU WANT
        ShowPageUI();
        if (InMain){
            DisplayCharacterInfo();
        }
    }

    //PRESS UPGRADE button
    public void UpgradeButton(){

        if(CanUpgrade()){
            if(GameManager.instance.money >= cards[currentIndex].upgradeCost){

                    GameManager.instance.money -= cards[currentIndex].upgradeCost;
                    FindObjectOfType<UIManager>().UpdateMoneyUI();

                    DisplayInfo();

                    PlayParticles();
                    StartCoroutine(UpgradeHealthEffect());
                    StartCoroutine(UpgradeAttackEffect());
                    StartCoroutine(UpgradeSpeedEffect());//New // particle effects
                    StartCoroutine(UpgradeRangeEffect());//New // particle effects
                    StartCoroutine(UpgradeCoolEffect());//New // particle effects

                StartCoroutine(UpgradeIncrementDisplay());

                cards[currentIndex].cardLevel++;
                UpdateLevel();
                    
                    Debug.Log("Upgrade success");
                }else{
                    Debug.Log("Not enough money");
                }
        }else{
            tipAnim.SetTrigger("isActive");
            Debug.Log("You did not own this Character");
        }
    }

    private bool CanUpgrade(){
        if(cards[currentIndex].locked == false){
            CanUpgradeHero = true;
        }else{
            CanUpgradeHero = false;
        }

        return CanUpgradeHero;
    }

    #region Display All of characters and SELECTED Hero Detail INFO
    //MARKER Display Character DETAIL INFO on THIRD SCREEN
    public void DisplayInfo(){
        if(cards[currentIndex].locked == false){
            foreach(Transform child in infopreviews[1].transform.parent)
            {
                child.gameObject.SetActive(false);
            }
            infopreviews[currentIndex].SetActive(true);
            SelectButton.color = new Color(1, 1, 1, 1);
            cardSpriteImage.sprite = cards[currentIndex].cardSprite;
            cardSpriteImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            cardNameText.text = cards[currentIndex].cardName;
            cardHisText.text = cards[currentIndex].cardHistory;
            cardHealthText.text = cards[currentIndex].cardHealth.ToString();
            cardAttText.text = cards[currentIndex].cardAttack.ToString();
            cardSpeedText.text = cards[currentIndex].cardSpeed.ToString();//New
            cardRangeText.text = cards[currentIndex].cardRange.ToString();//New
            cardCoolText.text = cards[currentIndex].cardCool.ToString();//New

            cardDesText.text = cards[currentIndex].cardDescription;
            locks.SetActive(false);
        }else{
            foreach(Transform child in infopreviews[1].transform.parent)
            {
                child.gameObject.SetActive(false);
            }
            SelectButton.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            cardSpriteImage.sprite = cards[currentIndex].cardSprite;
            cardSpriteImage.GetComponent<Image>().color = new Color(0.15f, 0.15f, 0.15f, 1f);

            cardNameText.text = cards[currentIndex].cardName;
            cardHisText.text = "???";
            cardHealthText.text = "???";
            cardAttText.text = "???";
            cardSpeedText.text = "???";//New
            cardRangeText.text = "???";//New
            cardCoolText.text = "???";//New
            cardDesText.text = "       Character locked.\r\n No description available.\r\n :(";
            locks.SetActive(true);
        }
    }

    public void DisplayCharacterInfo(){
        if(cards[currentIndex].locked == false){

            SelectButton.color = new Color(1, 1, 1, 1);
            characterImage.sprite = cards[currentIndex].cardSprite;
            characterImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            itemOne.sprite = itemCard.slotOne_Image.sprite;
            itemTwo.sprite = itemCard.slotTwo_Image.sprite;
            itemThree.sprite = itemCard.slotThree_Image.sprite;
            itemFour.sprite = itemCard.slotFour_Image.sprite;
            weaponOne.sprite = weaponCard.slotOneImage.sprite;
            weaponTwo.sprite = weaponCard.slotTwoImage.sprite;
            weaponThree.sprite = weaponCard.slotThreeImage.sprite;
        }else{

            SelectButton.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            characterImage.sprite = cards[currentIndex].cardSprite;
            characterImage.GetComponent<Image>().color = new Color(0.15f, 0.15f, 0.15f, 1f);
            itemOne.sprite = notAvailabeImage;
            itemTwo.sprite = notAvailabeImage;
            itemThree.sprite = notAvailabeImage;
            itemFour.sprite = notAvailabeImage;
            weaponOne.sprite = notAvailabeImage;
            weaponTwo.sprite = notAvailabeImage;
            weaponThree.sprite = notAvailabeImage;
        }
    }

    public void DisplayLevelBar(){
        for(int j = 0; j < slots.Length; j++){
            for(int i = 0; i < levelNeededeExp.Length; i++){
                if(cards[j].locked == true){
                    //[DISPLAY EXP Bar and LEVEL TEXT]
                    slots[j].transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "XX/XXX";//MARKER Use One line
                    slots[j].transform.Find("Level Image").transform.GetChild(0).GetComponent<Text>().text = "?";//LV on the CORNER Display

                    cards[j].cardLevel = 0;//MARKER NEW Features

                    slots[j].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = 0.0f;
                }
                else{
                    if((cards[j].currentExperience >= levelNeededeExp[i]) && (cards[j].currentExperience <= levelNeededeExp[i + 1]))//MARKER The CURRENT_EXPERIENCE is NOT the TOTAL EXPERIENCE of the Character
                    {
                        if (j != GameManager.instance.playerID)//NEW
                        {
                            slots[j].transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = cards[j].currentExperience + "/" + levelNeededeExp[i + 1];//MARKER CORRECT
                            slots[j].transform.Find("Level Image").transform.GetChild(0).GetComponent<Text>().text = (i + 2).ToString();//LV

                            cards[j].cardLevel = i + 2;//MARKER NEW Features

                            slots[j].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = (float)cards[j].currentExperience / levelNeededeExp[i + 1];
                        }
                        else
                        {
                            slots[j].transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = GameManager.instance.playerExp + "/" + levelNeededeExp[i + 1];//MARKER CORRECT
                            slots[j].transform.Find("Level Image").transform.GetChild(0).GetComponent<Text>().text = (i + 2).ToString();//LV

                            cards[j].cardLevel = i + 2;//MARKER NEW Features

                            slots[j].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = (float)GameManager.instance.playerExp / levelNeededeExp[i + 1];
                        }

                    }
                }
            }
        }
    }
    #endregion

    //MARKER Display All of Characters on SECOND SCREEN [LOCKED or UNLOCKED]
    public void DisplayCharacters()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.GetChild(1).GetComponent<Image>().sprite = cards[i].cardSprite;

            if (!cards[i].locked)
            {
                slots[i].transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                slots[i].transform.GetChild(1).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1); ;
            }
        }
    }
    public void DisplayAvailableCharacter(){
        for(int j=0; j < charSlots.Length; j++){
            charSlots[j].transform.GetChild(1).GetComponent<Image>().sprite = cards[j].cardSprite;

            if (!cards[j].locked)
            {
                charSlots[j].transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                charSlots[j].transform.GetChild(1).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1); ;
            }
        }
    }
    public void DisplayItemCharOpen(){
        for(int x=0; x<itemCharSlots.Length; x++){
            itemCharSlots[x].transform.GetChild(0).GetComponent<Image>().sprite = cards[x].cardSprite;
            if (!cards[x].locked){
                itemCharSlots[x].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else{
                itemCharSlots[x].transform.GetChild(0).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1); ;
            }
        }
    }

    //MARKER Display TITLE & PAGE UI ON SELECTION(SECOND) SCREEN
    public void ShowPageUI()
    {
        titleText.text = "Hero Selection " + GetUnlockedCount() + "/" + cards.Length.ToString();
        pageText.text = (currentIndex + 1).ToString() + "/" + cards.Length.ToString();
    }

    public void UpdateLevel()//Hero Level。NextButton，BackButton Canvas,UpgradeButton中
    {
        cardLvText.text = "Level " + (cards[currentIndex].cardLevel+1).ToString();
        
    }

    //MARKER THIS HELPER METHOD ONLY used for CALCULATE How many cards UNCLOCKED in this game for ShowPageUI FUNCTION
    private int GetUnlockedCount()
    {
        int unlockedCount = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].locked == false)
            {
                unlockedCount++;
            }
        }
        return unlockedCount;
    }

    #region Only For Mouse Interaction with EXCLAMATION POINT(!!!!) Icon
    public void MouseEnterHisImage()
    {
        histroyImage.gameObject.SetActive(true);
    }

    public void MouseExitHisImage()
    {
        histroyImage.gameObject.SetActive(false);
    }
    #endregion

    #region VISUAL EFFECT (INCLUDES ROLLING Number Effect & PARTICLES system)
    //MARKER CALL this function when you PRESS UPGRADE BUTTON & CAN UPGRADE
    private void PlayParticles()
    {
        heroLevelupEffect.Play();
        healthLevelUpEffect.Play();
        attackLevelupEffect.Play();
        speedLevelupEffect.Play();//New
        rangeLevelupEffect.Play();//New
        coolLevelupEffect.Play();//New
    }

    //MARKER Rolling Number Effect. CALL THESE FUCNTION when you PRESS UPGRADE BUTTON & CAN UPGRADE 
    IEnumerator UpgradeHealthEffect()
    {
        int currentHealth = cards[currentIndex].cardHealth;

        while (cards[currentIndex].cardHealth < currentHealth + healthIncrement)
        {
            cards[currentIndex].cardHealth += 5;
            yield return new WaitForSeconds(1/20);
        }
    }

    IEnumerator UpgradeAttackEffect()
    {
        int currentAttack = cards[currentIndex].cardAttack;

        while(cards[currentIndex].cardAttack < currentAttack + attackIncrement)
        {
            cards[currentIndex].cardAttack += 5;
            yield return new WaitForSeconds(1/50);
        }
    }

    IEnumerator UpgradeSpeedEffect()//New //particle effects
    {
        int currentSpeed = cards[currentIndex].cardSpeed;

        while (cards[currentIndex].cardSpeed < currentSpeed + speedIncrement)
        {
            cards[currentIndex].cardSpeed += 5;
            yield return new WaitForSeconds(1 / 50);
        }
    }

    IEnumerator UpgradeRangeEffect()//New //particle effects
    {
        int currentRange = cards[currentIndex].cardRange;

        while (cards[currentIndex].cardRange < currentRange + rangeIncrement)
        {
            cards[currentIndex].cardRange += 5;
            yield return new WaitForSeconds(1 / 50);
        }
    }

    IEnumerator UpgradeCoolEffect()//New //particle effects
    {
        int currentCool = cards[currentIndex].cardCool;

        while (cards[currentIndex].cardCool < currentCool + coolIncrement)
        {
            cards[currentIndex].cardCool += 5;
            yield return new WaitForSeconds(1 / 50);
        }
    }


    IEnumerator UpgradeIncrementDisplay() {
        healthIncrementText.text = "+" + healthIncrement.ToString();
        attackIncrementText.text = "+" + attackIncrement.ToString();
        speedIncrementText.text = "+" + speedIncrement.ToString();//New //particle effects
        rangeIncrementText.text = "+" + rangeIncrement.ToString();//New //particle effects 
        coolIncrementText.text = "+" + coolIncrement.ToString();//New //particle effects 
        yield return new WaitForSeconds(1.0f);
        healthIncrementText.text = "";
        attackIncrementText.text = "";
        speedIncrementText.text = "";//New //particle effects 
        rangeIncrementText.text = "";//New //particle effects 
        coolIncrementText.text = "";//New //particle effects 
    }
    #endregion

}
