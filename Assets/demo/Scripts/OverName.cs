using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverName : MonoBehaviour
{
    public GameObject Manager;
    void OnMouseOver()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        if(Input.GetMouseButtonDown(0))
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
            //Manager.GetComponent<Match>().TypeMode();
        }
    }

    void OnMouseExit()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
    }
}