using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHexagon : MonoBehaviour
{
    public GameObject rap;
    public GameObject hexagon;
    public GameObject parent;
    public float count;
    public float distance;
    int index;

    public enum Selection {horizontal, vertical};
    public Selection direction;

    void Start()
    {
        Call();
    }
    void Call()
    {
        if(index < count)
        {
            index++;
            Debug.LogError(index);
            HInstantiate();
        }
    }
    void HInstantiate()
    {
        if(direction == Selection.vertical)
        {
            //Debug.LogError(this.transform.position.y + ((float)index * distance));
            Instantiate(hexagon, new Vector3(this.transform.position.x, this.transform.position.y + ((float)index * distance), this.transform.position.z), Quaternion.identity, parent.transform);
        }
        else
        {
            Debug.LogError(((float)index * distance));
            Instantiate(hexagon, new Vector3(this.transform.position.x + ((float)index * distance), this.transform.position.y, this.transform.position.z), Quaternion.identity, parent.transform);
        }
        Call();
    }
}