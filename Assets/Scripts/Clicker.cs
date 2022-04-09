using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    private int index = 0;
    public Text text;
    public void Add()
    {
        index++;
        text.text = string.Format("The Useless Button [{0}]", index);
    }
}
