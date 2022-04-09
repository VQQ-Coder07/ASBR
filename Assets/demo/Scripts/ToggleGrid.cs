using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGrid : MonoBehaviour
{
    public Animator button;
    private bool visible = true;
    public void toggle()
    {
        visible = !visible;
        this.GetComponent<Animator>().SetBool("enabled", visible);
        button.SetBool("enabled", visible);
    }
}
