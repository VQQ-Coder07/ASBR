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
    public GameObject player()
    {
        return CameraController.instance.realtarget.gameObject;
    }
    public void ability()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Abilities>().init();
    }
    public GameObject[] abilityIcons;
}
