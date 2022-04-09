using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars : MonoBehaviour
{
    public static GameVars refs;
    public GameObject mainscr, specscr, endscr, startscr;
    public void Awake()
    {
        refs = this;
    }
}
