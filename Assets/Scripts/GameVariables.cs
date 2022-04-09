using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static GameVariables refs;
    public GameObject mainscr, specscr, endscr, startscr;
    public GameObject deathparts;
    public void Awake()
    {
        refs = this;
    }
}
