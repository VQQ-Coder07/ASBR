using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWeapon_Spawn : MonoBehaviour
{
    [SerializeField] GameObject Instance;
    void Start(){
        SpawnWeapons_Collectable();
    }
    private void Update(){
;
    }
    void SpawnWeapons_Collectable(){
        if(GameManager.instance.weaponOne_Name == "Soccer" && GameManager.instance.weaponTwo_Name == "Football" && GameManager.instance.weaponThree_Name == "Basketball"){
            if(Instance == null){
                int randInt = Random.Range(1, 4);
                if(randInt == 1){
                    //Instance = (GameObject)Instantiate(SceneController.instance.Soccer_Weapon, transform.position, Quaternion.identity) as GameObject;
                }
                else if(randInt == 2){
                    //Instance = (GameObject)Instantiate(SceneController.instance.Basketball_Weapon, transform.position, Quaternion.identity) as GameObject;
                }
                if(randInt == 3){
                    //Instance = (GameObject)Instantiate(SceneController.instance.Football_Weapon, transform.position, Quaternion.identity) as GameObject;
                }
                else if(randInt == 4){
                    //Instance = (GameObject)Instantiate(SceneController.instance.Soccer_Weapon, transform.position, Quaternion.identity) as GameObject;
                }
            }
        }
        else{
            Debug.Log("Sorry fool");
        }
        //if(GameManager.instance.weaponOne_Name == "Football"){
        //    if(Instance == null){
        //        int randInt = Random.Range(1, 4 - 1);
        //        if(randInt == 1){
        //            Instance = (GameObject)Instantiate(SceneController.instance.FootballWeapon, transform.position, Quaternion.identity) as GameObject;
        //        }
        //        else if(randInt == 2){
        //            Instance = (GameObject)Instantiate(SceneController.instance.BasketballWeapon, transform.position, Quaternion.identity) as GameObject;
        //        }
        //        if(randInt == 3){
        //            Instance = (GameObject)Instantiate(SceneController.instance.SoccerWeapon, transform.position, Quaternion.identity) as GameObject;
        //        }
        //        else if (randInt == 4){

        //            Instance = (GameObject)Instantiate(SceneController.instance.FootballWeapon, transform.position, Quaternion.identity) as GameObject;
        //        }
        //    }
        //}
        //else{
        //    Debug.Log("Sorry idiot");
        //}
        //if(GameManager.instance.weaponThree_Name == "Basketball"){
        //    int randInt = Random.Range(1, 4 - 1);
        //    if(randInt == 1){
        //        Instance = (GameObject)Instantiate(SceneController.instance.BasketballWeapon, transform.position, Quaternion.identity) as GameObject;
        //    }
        //    else if(randInt == 2){
        //        Instance = (GameObject)Instantiate(SceneController.instance.SoccerWeapon, transform.position, Quaternion.identity) as GameObject;
        //    }
        //    if(randInt == 3){
        //        Instance = (GameObject)Instantiate(SceneController.instance.FootballWeapon, transform.position, Quaternion.identity) as GameObject;
        //    }
        //    else if(randInt == 4){
        //        Instance = (GameObject)Instantiate(SceneController.instance.BasketballWeapon, transform.position, Quaternion.identity) as GameObject;
        //    }
        //}
        //else{
        //    Debug.Log("Sorry stupid");
        //}
    }
}
