using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform target;
    void FixedUpdate()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if(target != null)
        {
            this.transform.localScale = target.localScale;
            this.transform.position = target.position;
        }
    }
}
