using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public tutorial tt;
    public int val;
    private bool once;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !once)
        {
            once = true;
            tt.forward(val);
        }
    }
}