using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public GameObject demo;
    public float speed;
    private float t = 0;
    private float t2 = 0;
    private float op;
    private enum state {inTrigger, outTrigger};
    private state status = state.outTrigger;

    private void Start()
    {
        LineRenderer[] lrs = demo.transform.GetComponentsInChildren<LineRenderer>();
        foreach (LineRenderer lr in lrs)
        {
            Color slrc;
            slrc = lr.startColor;
            lr.startColor = new Color(slrc.r, slrc.g, slrc.b, 0f);

            Color elrc;
            elrc = lr.endColor;
            lr.endColor = new Color(elrc.r, elrc.g, elrc.b, 0f);
        }
        SpriteRenderer[] sprs = demo.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in sprs)
        {
            Color src;
            src = sr.color;
            sr.color = new Color(src.r, src.g, src.b, 0f);
        }

        TextMesh[] tms = demo.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh tm in tms)
        {
            Color tmc;
            tmc = tm.color;
            tm.color = new Color(tmc.r, tmc.g, tmc.b, 0f);
        }
    }
    private float col()
    {
        return demo.transform.GetChild(0).gameObject.GetComponent<TextMesh>().color.a;   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Debug.LogError("DAMN UNITY COLLIDERS");
            status = state.inTrigger;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && status == state.inTrigger)
        t2 = 0;
        t = 0;
        //Debug.LogError("EXIT");
        status = state.outTrigger;
    }
    private void Update()
    {
        switch (status)
        {
            case state.inTrigger:
                op = Mathf.Lerp(0f, 1f, t);
                t += speed * Time.deltaTime;
                return;
            case state.outTrigger:
                op = Mathf.Lerp(1f, 0f, t2);
                t2 += speed * Time.deltaTime;
                return;
        }
    }
    private void LateUpdate()
    {
        LineRenderer[] lrs = demo.transform.GetComponentsInChildren<LineRenderer>();
        foreach (LineRenderer lr in lrs)
        {
            Color slrc;
            slrc = lr.startColor;
            lr.startColor = new Color(slrc.r, slrc.g, slrc.b, (op / 2));

            Color elrc;
            elrc = lr.endColor;
            lr.endColor = new Color(elrc.r, elrc.g, elrc.b, (op / 2));
        }
        SpriteRenderer[] sprs = demo.transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in sprs)
        {
            Color src;
            src = sr.color;
            sr.color = new Color(src.r, src.g, src.b, op);
        }

        TextMesh[] tms = demo.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh tm in tms)
        {
            Color tmc;
            tmc = tm.color;
            tm.color = new Color(tmc.r, tmc.g, tmc.b, op);
        }
    }
}
