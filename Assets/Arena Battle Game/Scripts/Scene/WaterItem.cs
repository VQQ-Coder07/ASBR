using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 12)
            return;

        if(other.gameObject.transform.parent.tag == "Player"){
            PlayerController unit = other.transform.GetComponentInParent<PlayerController>();
            if(unit != null){
                other.transform.GetComponentInParent<PlayerController>().waterColliders++;
                unit.speed = 0.9f;
                unit.InWater = true;
            }
        }
        else if(other.gameObject.transform.parent.tag == "Bot"){
            BotController unit = other.transform.GetComponentInParent<BotController>();
            if(unit != null){
                unit.agent.speed = 1;
                unit.InWater = true;
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer != 12)
        return;
        if(other.gameObject.transform.parent.tag == "Player"){
            PlayerController unit = other.transform.GetComponentInParent<PlayerController>();
            if(unit != null){
                if(other.transform.GetComponentInParent<PlayerController>().waterColliders < 2)
                {
                    unit.speed = 2;
                    unit.InWater = false;
                }
                other.transform.GetComponentInParent<PlayerController>().waterColliders--;
            }
        }
        else if(other.gameObject.transform.parent.tag == "Bot"){
            BotController unit = other.transform.GetComponentInParent<BotController>();
            if(unit != null){
                unit.agent.speed = 3.5f;
                unit.InWater = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer != 12)
            return;

        if(other.gameObject.transform.parent.tag == "Player"){
            PlayerController unit = other.GetComponentInParent<PlayerController>();
            if(unit != null){
                unit.speed = 0.9f;
                unit.InWater = true;
            }
        }
        else if(other.gameObject.transform.parent.tag == "Bot"){
            BotController unit = other.GetComponentInParent<BotController>();
            if(unit != null){
                unit.agent.speed = 1;
                unit.InWater = true;
            }
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.layer != 12)
            return;
        if(other.gameObject.transform.parent.tag == "Player"){
            PlayerController unit = other.GetComponentInParent<PlayerController>();
            if(unit != null){
                unit.speed = 2;
                unit.InWater = false;
            }
        }
        else if(other.gameObject.transform.parent.tag == "Bot"){
            BotController unit = other.GetComponentInParent<BotController>();
            if(unit != null){
                unit.agent.speed = 3.5f;
                unit.InWater = false;
            }
        }
    }
}
