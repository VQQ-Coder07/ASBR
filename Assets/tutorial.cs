using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial : MonoBehaviour
{
    public Text text;
    public string[] messages;
    public Animator anim;
    public void forward(int value)
    {
        text.text = messages[value];
        anim.Play("tutorial");
    }
}
