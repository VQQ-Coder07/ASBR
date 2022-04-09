using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatePlay : MonoBehaviour
{
    public string animName;
    public float minTime;
    public float maxTime;
    private Animator anim()
    {
        return this.GetComponent<Animator>();
    }
    void Start()
    {
        Invoke("Play", Random.Range(minTime, maxTime));
    }
    void Play()
    {
        anim().Play(animName);
    }
}
