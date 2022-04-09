using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    public Vector3 offset;
    private Vector3 initPos, lastPos;
    public float speed;
    [SerializeField]
    private float t, y;
    public enum state {inCol, outCol};
    public state status;
    public bool pause;
    private void Start()
    {
        status = state.outCol;
        initPos = this.transform.position;
        lastPos = this.transform.position + offset;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        status = state.inCol;
    }
    private void OnTriggerExit2D()
    {
        status = state.outCol;
        pause = false;
    }
    public bool bPause;
    private void Update()
    {
        if(status != state.inCol && transform.position != lastPos && !pause)
        {
            bPause = false;
            this.transform.position = new Vector3(Mathf.Lerp(initPos.x, lastPos.x, t), Mathf.Lerp(initPos.y, lastPos.y, t), Mathf.Lerp(initPos.z, lastPos.z, t));
            t += speed * Time.deltaTime;
            if (t >= 1)
            {
                t = 0;
                pause = true;
            }
        }
        else if(status == state.inCol && transform.position != initPos && !bPause)
        {
            this.transform.position = new Vector3(Mathf.Lerp(lastPos.x, initPos.x, y), Mathf.Lerp(lastPos.y, initPos.y, y), Mathf.Lerp(lastPos.z, initPos.z, y));
            y += speed * Time.deltaTime;
            if (y >= 1)
            {
                y = 0;
                bPause = true;
                //pause = true;
            }
        }
    }
}