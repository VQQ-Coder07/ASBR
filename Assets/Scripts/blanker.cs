using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blanker : MonoBehaviour
{
    private Animator anim()
    {
        return this.gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim().Play("in");
        this.GetComponent<BlurLerp>().val = true;
    }
    public void postDestroy()
    {
        this.GetComponent<BlurLerp>().val = false;
    }
    public void DestroyGm()
    {
        gameObject.SetActive(false);
    }
}
