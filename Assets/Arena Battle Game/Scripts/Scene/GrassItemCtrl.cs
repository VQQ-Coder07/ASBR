﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassItemCtrl : MonoBehaviour {

    private Material _originalMaterial;
    private int _unitsCountInsideGrass;

    private void Awake() {
        this._originalMaterial = GetComponent<Renderer>().sharedMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<GrassTransparency>().inGrass = true;
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer != 10)   //Layer 10 - GrassDetector
            return;

        /*if(other.gameObject.transform.parent.tag == "Player"){
            PlayerController unit = other.GetComponentInParent<PlayerController>();
            if(unit != null){
                if(unit.isOurTeam){
                    _unitsCountInsideGrass++;
                    this._Refresh();
                }
                unit.OnUnitEnterGrass();
            }
        }*/
        else if(other.gameObject.transform.parent.tag == "Bot"){
            BotController unit = other.GetComponentInParent<BotController>();
            if(unit != null){
                if(unit.isOurTeam){
                    _unitsCountInsideGrass--;
                    this._Refresh();
                }

                unit.OnUnitExitGrass();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<GrassTransparency>().inGrass = false;
        if (other.gameObject.layer != 10)
            return;

        if(other.gameObject.transform.parent.tag == "Player"){
            PlayerController unit = other.GetComponentInParent<PlayerController>();
            if(unit != null){
                if(unit.isOurTeam){
                    _unitsCountInsideGrass--;
                    this._Refresh();
                }

                unit.OnUnitExitGrass();
            }
        }
        else if(other.gameObject.transform.parent.tag == "Bot"){
            BotController unit = other.GetComponentInParent<BotController>();
            if(unit != null){
                if(unit.isOurTeam){
                    _unitsCountInsideGrass--;
                    this._Refresh();
                }

                unit.OnUnitExitGrass();
            }
        }
    }

    private void _Refresh(){
        if(this._unitsCountInsideGrass > 0){
            //that means there is one or more units on grass
            this.FadeTo(0.25f, 0.5f);
        }else{
            //that means there is no units inside grass
            this.FadeTo(1, 0.5f);
        }
    }


    private IEnumerator _coroutine;
    public void FadeTo(float targetOpacity, float duration){
        if(_coroutine != null)
            this.StopCoroutine(this._coroutine);

        this._coroutine = this._FadeTo(GetComponent<Renderer>().material, targetOpacity, duration);
        this.StartCoroutine(this._coroutine);
    }

    IEnumerator _FadeTo(Material material, float targetOpacity, float duration){
        Color color = material.color;
        float startOpacity = color.a;

        float t = 0;
        while (t < duration){
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);
            material.color = color;

            yield return null;
        }

        if (targetOpacity == 1)
            GetComponent<Renderer>().material = this._originalMaterial;
    }
}
