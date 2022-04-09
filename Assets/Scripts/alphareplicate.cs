using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class alphareplicate : MonoBehaviour
{
    void Update()
    {
        Image parentimg = transform.parent.GetComponent<Image>();
        Image thisimg = GetComponent<Image>();
        thisimg.color = new Color(thisimg.color.r, thisimg.color.g, thisimg.color.b, parentimg.color.a/5);
    }
}
