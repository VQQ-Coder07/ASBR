using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDestroy : MonoBehaviour
{
    public float speed;
    private float t;
    void Update()
    {
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, Mathf.Lerp(1, 0, t));
        t += speed * Time.deltaTime;
        if(sp.color.a == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
