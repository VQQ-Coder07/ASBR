using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverlayWindowCtrl : ClosableWindowCtrl
{
    public static GameOverlayWindowCtrl instance;

    /* component refs */
    public JoystickComponentCtrl NavigatorJoystickComponent;
    public JoystickComponentCtrl AttackerJoystickComponent;
    public JoystickComponentCtrl AttackerJoystickComponentTwo;
    public JoystickComponentCtrl AttackJoystickComponentThree;
    public JoystickComponentCtrl AbilityJoystickComponent;


    /* UI ref */
    [Header("Item Sprite Slot")]
    public Image itemslotOne;
    public Image itemslotTwo;
    public Image itemslotThree;
    public Image itemslotFour;

    [Header("Weapon Sprite Slot")]
    public Image weaponSprite_One;
    public Image weaponSprite_Two;
    public Image weaponSprite_Three;

    /*Sprite ref */
     public Sprite bushSprite;
     public Sprite crateSprite;
     public  Sprite shieldSprite;
     public Sprite bearSprite;
    public Sprite notAvialiableSprite;

    private void Awake(){
        instance = this;
    }

    public override void Init(){
        base.Init();
    }

    private void Update(){
        if(SceneController.instance.Instance != null){
            if(NavigatorJoystickComponent.Horizontal != 0 || NavigatorJoystickComponent.Vertical != 0){

                Vector3 moveVector = (Vector3.right * NavigatorJoystickComponent.Horizontal + Vector3.forward * NavigatorJoystickComponent.Vertical);
                moveVector.Normalize();
                SceneController.instance.Instance.GetComponent<PlayerController>().Move(moveVector);
            }
            else{
                //Debug.Log("False");
                SceneController.instance.Instance.GetComponent<PlayerController>().StopMove();
            }
        }

        if(AttackerJoystickComponent.Horizontal != 0 || AttackerJoystickComponent.Vertical != 0){

            Vector3 directionVector = (Vector3.right * AttackerJoystickComponent.Horizontal + Vector3.forward * AttackerJoystickComponent.Vertical);
            directionVector.Normalize();
            SceneController.instance.Instance.GetComponent<PlayerController>().Aim(directionVector);
        }
        else if(AttackerJoystickComponentTwo.Horizontal != 0 || AttackerJoystickComponentTwo.Vertical != 0){
            Vector3 directionVector = (Vector3.right * AttackerJoystickComponentTwo.Horizontal + Vector3.forward * AttackerJoystickComponentTwo.Vertical);
            directionVector.Normalize();
            SceneController.instance.Instance.GetComponent<PlayerController>().Aim(directionVector);
        }
        else if(AttackJoystickComponentThree.Horizontal != 0 || AttackJoystickComponentThree.Vertical != 0){
            Vector3 directionVector = (Vector3.right * AttackJoystickComponentThree.Horizontal + Vector3.forward * AttackJoystickComponentThree.Vertical);
            directionVector.Normalize();
            SceneController.instance.Instance.GetComponent<PlayerController>().Aim(directionVector);
        }
            else if(AbilityJoystickComponent.Horizontal != 0 || AbilityJoystickComponent.Vertical != 0){
            Vector3 directionVector = (Vector3.right * AbilityJoystickComponent.Horizontal + Vector3.forward * AbilityJoystickComponent.Vertical);
            directionVector.Normalize();
            //SceneController.instance.Instance.GetComponent<PlayerController>().Aim(directionVector);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().Aim(directionVector);

        }
        else
        if(SceneController.instance.Instance != null)
        {
            SceneController.instance.Instance.GetComponent<PlayerController>().StopAim();
            if(GameObject.FindGameObjectWithTag("Player") != null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().StopAim();
        }
    }
        

    public void OnTapAttackerJoystickComponent(){

        if (SceneController.instance.Instance != null)
            SceneController.instance.Instance.GetComponent<PlayerController>().Attack();
       


        if(SceneController.instance.Instance.GetComponent<PlayerController>().isSoccer == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isSoccer = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isBasketball == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isBasketball = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isFootball == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isFootball = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isAirsoft == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isAirsoft = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isArrow == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isArrow = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isFrisbee == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isFrisbee = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isHockey == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isHockey = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isGolf == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isGolf = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isTableTennis == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isTableTennis = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isTennis == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isTennis = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isVolleyball == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isVolleyball = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isBaseball == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isBaseball = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isVolleyball == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isVolleyball = false;
        }
        else if(SceneController.instance.Instance.GetComponent<PlayerController>().isBowling == true){
            SceneController.instance.Instance.GetComponent<PlayerController>().isBowling = false;
        }
    }
}
