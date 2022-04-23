using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int id;
    public Vector3 pos;
    public Quaternion rot;
    public GameObject spawnpoint;
    public void FixedUpdate()
    {
        rot = this.transform.localRotation;
        pos = this.transform.position;
    }
}
