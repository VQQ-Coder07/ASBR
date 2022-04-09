using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public GameObject BloodEffect;
    public Transform Blade; 
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("INSTANTIATEE");
            Instantiate(BloodEffect, Blade.position, Quaternion.identity, this.transform.parent);
        }
    }
}