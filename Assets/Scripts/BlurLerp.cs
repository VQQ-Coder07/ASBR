using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurLerp : MonoBehaviour
{
    public Material blur;
    private bool prev;
    public bool val;
    public float y;
    public float smoothtime;
    public float smoothtimeout;
    void Update()
    {
        if(prev != val)
        {
            y = 0;
            prev = val;
        }
        if(val)
        {
            blur.SetFloat("_blurSizeXY", Mathf.Lerp(0, 10, y));
            y += smoothtime * Time.deltaTime;
        }
        else
        {
            blur.SetFloat("_blurSizeXY", Mathf.Lerp(10, 0, y));
            y += smoothtimeout * Time.deltaTime;
        }
    }
    void OnDisable()
    {
        blur.SetFloat("_blurSizeXY", 0);
    }
}
