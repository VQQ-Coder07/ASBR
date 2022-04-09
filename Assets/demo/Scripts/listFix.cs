using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listFix : MonoBehaviour
{
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
        //Add bee skin
    }
}
