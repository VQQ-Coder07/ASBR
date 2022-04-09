using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReference1 : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Text>().text = string.Format("Your name is {0}.", PlayerPrefs.GetString("nickname"));
    }
}
