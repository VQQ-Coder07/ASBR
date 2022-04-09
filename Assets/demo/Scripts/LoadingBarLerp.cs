using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarLerp : MonoBehaviour
{
    public Animator camera;
    public float smoothTime;
    private float yVelocity = 0.0f;
    private float val;
    private Slider slider;
    private bool once;
    private bool start = true;
    public bool joininggame;
    private void Start()
    {
        slider = this.GetComponent<Slider>();
    }
    public void SetVal(float value)
    {
        once = true;
        val = value;
    }
    public void Update()
    {
        if(once)
        {
            if(yVelocity != val)
            {
                slider.value = Mathf.SmoothDamp(slider.value, val, ref yVelocity, smoothTime);
            }
            if(slider.value >= 95 && start && !joininggame)
            {
                this.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("doit");
                val = 0;
                yVelocity = 0;
                slider.value = 0;
                camera.SetTrigger("rotate");
                start = false;
            }
        }
    }
}
