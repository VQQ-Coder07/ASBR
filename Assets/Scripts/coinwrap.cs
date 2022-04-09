using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinwrap : MonoBehaviour
{
    public Animator text;
    public float smoothTime; 
    public Transform[] coins;
    public Transform[] startposes;
    public Transform[] finalposes;
    public float[] velocity;
    public float time;
    public float amplification;
    public float destroylimit;
    public bool powerup;
    private void Start()
    {
        this.gameObject.layer = 5;
        text = Chests.instance.text;
        text.SetTrigger("restore");
    }
    private void LateUpdate()
    {
        time += Time.deltaTime;
        for(int j=0; j<coins.Length; j++)
        {
            if(time > (float)j * amplification)
            {
                coins[j].position = Vector3.Lerp(startposes[j].position, finalposes[j].position, velocity[j]);
                velocity[j] += smoothTime * Time.deltaTime;
            }
        }
        if(velocity[velocity.Length - 1] > destroylimit)
        {
            text.SetTrigger("fade");
            if(powerup)
            {
                Chests.instance.text.gameObject.GetComponent<Animator>().SetTrigger("fade");
                this.GetComponent<Animator>().SetTrigger("animate");
            }
            Destroy(this);
        }
    }
}
