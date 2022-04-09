using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public bool value;
    public UnityEvent OnChangedValue;
    public void Start()
    {
        OnChangedValue.Invoke();
    }
    public Animator anim()
    {
        return GetComponentInChildren<Animator>();
    }
    public void enable()
    {
        value = true;
        anim().SetBool("toggle", value);
        OnChangedValue.Invoke();
    }
    public void disable()
    {
        value = false;
        anim().SetBool("toggle", value);
        OnChangedValue.Invoke();
    }
    public void Toggle()
    {
        value = !value;
        anim().SetBool("toggle", value);
        OnChangedValue.Invoke();
    }
    void LateUpdate()
    {
        anim().SetBool("toggle", value);
    }
}
