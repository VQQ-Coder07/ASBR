using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    private bool used;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !used)
        {
            other.GetComponent<PlayerControl>().AddSpawnPoint(this.gameObject);
            used = true;
        }
    }
}
