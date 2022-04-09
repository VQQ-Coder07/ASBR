using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTrigger : MonoBehaviour
{
    public List<GameObject> targets;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            targets.Remove(other.gameObject);
        }
    }
}
