using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public static SceneController instance;
    public GameObject dummy;
    public bool tutorial;
    #region Useable Weapon
    [Header("Usaable Weapon")]
    [SerializeField] public GameObject AirosftWeapon;
    [SerializeField] public GameObject ArrowWeapon;
    [SerializeField] public GameObject FrisbeeWeapon;
    [SerializeField] public GameObject HockeyWeapon;
    [SerializeField] public GameObject GolfWeapon;
    [SerializeField] public GameObject TableTennisWeapon;
    [SerializeField] public GameObject TennisWeapon;
    [SerializeField] public GameObject BaseballWeapon;
    [SerializeField] public GameObject SoccerWeapon;
    [SerializeField] public GameObject VolleyWeapon;
    [SerializeField] public GameObject BasketWeapon;
    [SerializeField] public GameObject FootballWeapon;
    [SerializeField] public GameObject BowlingWeapon;
    #endregion

    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] public GameObject[] Players_DropItems;
    [SerializeField] public GameObject Bot_DropItems;
    [SerializeField] public GameObject healthPack;
    [SerializeField] public GameObject currentWeapon;

    /* component refs */
    [SerializeField] public GameObject Design;
    [SerializeField] public GameObject UnitsContainer;
    [SerializeField] public GameObject PlayerSpawnPosition;
    [SerializeField] public StageItemCtrl Stage;
    [SerializeField] public GameObject ParticlesContainer;
    [SerializeField] public GameObject Instance;
    [SerializeField] public GameObject WeaponInstance;
	public PUN2_RoomController RoomController;

    void Awake () {
        instance = this;
        Application.targetFrameRate = 60;

        //Ignore collision between players
        Physics.IgnoreLayerCollision(11, 11); //Layer 11 - Player
        Physics.IgnoreLayerCollision(14, 14);
    }

    private void Start()
    {
        //SetUpItem_UI();
       SetUpWeapon_UI();
        SpawnPlayer();
    }
    #region SpawnPlayer
    void SpawnPlayer(){
        if(tutorial)
        {
            Instance = RoomController.SpawnPlayer(dummy);
            return;
        }
        if(GameManager.instance.playerName == "Jack"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[0]);
			//Instance.tag = "Player";
            //Instance = (GameObject)Instantiate(playerPrefabs[0], PlayerSpawnPosition.transform.position, Quaternion.identity) as GameObject;
        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Teddy"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[1]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Luke"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[2]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Bruce"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[3]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Kill Shot"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[4]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Lola"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[5]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Rambo"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[6]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Cody"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[7]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Coby"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[8]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Cory"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[9]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Garett"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[10]);        }else{
            Debug.Log("Character not selected");
        }
        if(GameManager.instance.playerName == "Tyler"){
			Instance = RoomController.SpawnPlayer(playerPrefabs[11]);        }else{
            Debug.Log("Character not selected");
        }
    }
    #endregion

    void SetUpItem_UI()
    {
        //#region First ItemCheck
        //Debug.LogError(GameManager.instance.ItemTwo_Name);
        if(GameManager.instance.ItemOne_Name == ""){
            GameOverlayWindowCtrl.instance.itemslotOne.sprite = GameManager.instance.item_OneSprite;
            GameOverlayWindowCtrl.instance.itemslotOne.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.ItemTwo_Name == ""){
            GameOverlayWindowCtrl.instance.itemslotTwo.sprite = GameManager.instance.item_TwoSprite;
            GameOverlayWindowCtrl.instance.itemslotTwo.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.ItemThree_Name == ""){
            GameOverlayWindowCtrl.instance.itemslotThree.sprite = GameManager.instance.item_ThreeSprite;
            GameOverlayWindowCtrl.instance.itemslotThree.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.ItemFour_Name == ""){
            GameOverlayWindowCtrl.instance.itemslotFour.sprite = GameManager.instance.item_FourSprite;
            GameOverlayWindowCtrl.instance.itemslotFour.GetComponent<Button>().interactable = true;
        }
    }
        void SetUpWeapon_UI(){
        #region First WeaponCheck
        if(GameManager.instance.weaponOne_Name == "Soccer"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Soccer"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Soccer"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }


        if(GameManager.instance.weaponOne_Name == "Archery"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Archery"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Archery"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }


        if(GameManager.instance.weaponOne_Name == "Golf"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Golf"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Golf"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }


        if(GameManager.instance.weaponOne_Name == "Baseball"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Baseball"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Baseball"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }
        #endregion

        #region Second WeaponCheck
        if(GameManager.instance.weaponTwo_Name == "Football"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponOne_Name == "Football"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Football"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }


        if(GameManager.instance.weaponOne_Name == "Airsoft"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Airsoft"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Airsoft"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }


        if(GameManager.instance.weaponOne_Name == "Hockey"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Hockey"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Hockey"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }

        if(GameManager.instance.weaponOne_Name == "Tennis"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Tennis"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if (GameManager.instance.weaponThree_Name == "Tennis"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }

        if(GameManager.instance.weaponOne_Name == "Bowling"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Bowling"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Bowling"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }
        #endregion

        #region Third WeaponCheck
        if(GameManager.instance.weaponThree_Name == "Basketball"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponOne_Name == "Basketball"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Basketball"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }


        if(GameManager.instance.weaponOne_Name == "Frisbee"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Frisbee"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Frisbee"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }

        if(GameManager.instance.weaponOne_Name == "Tabletennis"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Tabletennis"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Tabletennis"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }

        if(GameManager.instance.weaponOne_Name == "Volleyball"){
            GameOverlayWindowCtrl.instance.weaponSprite_One.sprite = GameManager.instance.weapon_OneSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_One.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponTwo_Name == "Volleyball"){
            GameOverlayWindowCtrl.instance.weaponSprite_Two.sprite = GameManager.instance.weapon_TwoSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Two.GetComponent<Button>().interactable = true;
        }
        else if(GameManager.instance.weaponThree_Name == "Volleyball"){
            GameOverlayWindowCtrl.instance.weaponSprite_Three.sprite = GameManager.instance.weapon_ThreeSprite;
            GameOverlayWindowCtrl.instance.weaponSprite_Three.GetComponent<Button>().interactable = true;
        }
        #endregion
    }
}
