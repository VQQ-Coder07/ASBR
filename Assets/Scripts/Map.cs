using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Map")]
public class Map : ScriptableObject
{
    public string name = "Untitled";
    public int[] ids;
    public Vector3[] poses;
    public Quaternion[] rots;
    public bool[] spawnpoints;
}
