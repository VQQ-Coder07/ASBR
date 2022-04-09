using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    public GameObject[] Buttons;
    public Color SelectedColor;
    public Color DisabledColor;

    public void UpdateButton(int value)
    {
        foreach (GameObject ob in Buttons)
        {
            ob.transform.GetChild(0).gameObject.GetComponent<Text>().color = DisabledColor;
            ob.transform.GetChild(1).gameObject.GetComponent<Image>().color = DisabledColor;
        }
        Buttons[value].transform.GetChild(0).gameObject.GetComponent<Text>().color = SelectedColor;
        Buttons[value].transform.GetChild(1).gameObject.GetComponent<Image>().color = SelectedColor;
    }
}