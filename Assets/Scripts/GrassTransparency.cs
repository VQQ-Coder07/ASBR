using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTransparency : MonoBehaviour
{
    public Collider[] hitColliders;
    public bool inGrass;
    public float distance;
    public static GrassTransparency instance;

    private void Awake()
    {
        instance = this;
    }

    void LateUpdate()
    {
        if (hitColliders != null)
        {
            foreach (var hml in hitColliders)
            {
                if (hml != null)
                {
                    if (hml.GetComponent<GrassItemCtrl>() != null)
                    {
                        //hml.GetComponent<Renderer>().material.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                        hml.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }

            }
        }

        if (inGrass)
        {
            hitColliders = Physics.OverlapSphere(this.transform.position, distance);
            foreach (var htc in hitColliders)
            {
                if (htc.GetComponent<GrassItemCtrl>() != null)
                {
                    htc.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    //    Material mat = htc.gameObject.GetComponent<Renderer>().material;
                    //    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 100);
                    //htc.GetComponent<Renderer>().material.color = new Color(255.0f, 255.0f, 255.0f, 100.0f);
                }
            }
        }
    }
}
