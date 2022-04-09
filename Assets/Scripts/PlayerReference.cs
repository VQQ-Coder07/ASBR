using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReference : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Text>().text = PlayerPrefs.GetString("nickname");
    }
}
