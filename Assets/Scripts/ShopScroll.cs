using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopScroll : MonoBehaviour
{
    public Color highlightColor;
    public Color normalColor;
    public Scrollbar scroll;
    public RectTransform content;
    public RectTransform demopos1, demopos2, demopos3, demopos4;
    public float limit1, limit2, limit3;
    public TMP_Text text1, text2, text3, text4;
    public void Button1()
    {
        content.position = demopos1.position;
        content.sizeDelta = demopos1.sizeDelta;
        PlayAnim();
    }
    public void Button2()
    {
        content.position = demopos2.position;
        content.sizeDelta = demopos2.sizeDelta;
        PlayAnim();
    }
    public void Button3()
    {
        content.position = demopos3.position;
        content.sizeDelta = demopos3.sizeDelta;
        PlayAnim();
    }
    public void Button4()
    {
        content.position = demopos4.position;
        content.sizeDelta = demopos4.sizeDelta;
        PlayAnim();
    }
    private void PlayAnim()
    {
        this.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("scroll");
    }
    public void LateUpdate()
    {
        if(scroll.value <= limit1)
        {
            normalize();
            text1.color = highlightColor;
        }
        else if(scroll.value <= limit2)
        {
            normalize();
            text2.color = highlightColor;
        }
        else if(scroll.value <= limit3)
        {
            normalize();
            text3.color = highlightColor;
        }
        else if(scroll.value > limit3)
        {
            normalize();
            text4.color = highlightColor;
        }
    }
    private void normalize()
    {
        text1.color = normalColor;
        text2.color = normalColor;
        text3.color = normalColor;
        text4.color = normalColor;
    }
}
