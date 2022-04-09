using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[HideInInspector] 
    public bool disable;
    public Transform Player;
    public Vector3 _offset;
    public float _smoothTime;
    public float _lerpTime;
    private Vector3 velocity = Vector3.zero;
    private bool playerExists;
    public bool levels;
    private bool ready = true;
    private float defSize;
    [SerializeField]
    private float t;

    public bool editor;
    private void Awake()
    {
        defSize = this.GetComponent<Camera>().orthographicSize;
    }
    public void Reset()
    {
        ready = false;
    }
    public void Ready(Transform _player)
    {
        Player = _player;
        playerExists = true;
    }
    void Update()
    {
        if (!disable)
        {
            if(!ready)
            {
                this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(this.GetComponent<Camera>().orthographicSize, defSize, t);
                t += _lerpTime * Time.deltaTime;
                if(t > 1)
                {
                    t = 0;
                    ready = true;
                }
            }
            if((playerExists && !levels) && Player != null) this.transform.position = Vector3.SmoothDamp(this.transform.position, Player.position + _offset, ref velocity, _smoothTime);
            else if(levels)
            {
                if (!editor)
                {
                    Player = GameObject.FindGameObjectWithTag("Player").transform;
                }
                if(Player.gameObject == null || !playerExists)
                {
                    playerExists = true;
                    return;
                }
                else if(Player.gameObject != null)
                {
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, Player.position + _offset, ref velocity, _smoothTime);
                    playerExists = true;
                }
            }
        }
        else
        {
            
        }
    }
}