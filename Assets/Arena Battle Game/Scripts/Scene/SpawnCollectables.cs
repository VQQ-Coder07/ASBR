using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour
{
    public GameObject Instance;
    public GameObject[] collectables;

    void Start(){
        On_Spawn();
    }

    void On_Spawn(){
        if(Instance == null){
            int randInt = Random.Range(0, collectables.Length);
            Instance = (GameObject)Instantiate(collectables[randInt], transform.position, Quaternion.identity) as GameObject;
        }
        else{
            Debug.Log("Already exist");
        }
    }
}
