using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redirrect : MonoBehaviour
{
    public PlayerControl playerControl;
    private void OnCollisionEnter2D(Collision2D other)
    {
        playerControl.OnDownCollisionEnter2D(other);
    }
}
