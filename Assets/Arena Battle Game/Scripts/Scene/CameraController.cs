using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;

    /* prefabs */

    /* public vars */
    public GameObject mainCamera;
    public Transform target;

    public Transform realtarget;
    public float smoothTime = 0.3F;
    public float deltaTime;
    public float speed;
    public float distanceToTarget = -10.0f;

    /* private vars */
    private float _currentVelocity = 0;
    private float _startXPos;
    private float _startYPos;

    private Vector3 velocityF = Vector3.zero;

    void Awake(){
        instance = this;

        this._startXPos = this.transform.position.x;
        this._startYPos = this.transform.position.y;
    }

    void Start()
    {
        Invoke("SearchPlayer", 0.1f);
    }
    void SearchPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            realtarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Invoke("SearchPlayer", 0.1f);
        }
    }
    private void LateUpdate(){
        if (realtarget == null)
            return;

        float targetZPos = realtarget.transform.position.z + distanceToTarget;
        float currentZpos = Mathf.SmoothDamp(this.transform.position.z, targetZPos, ref _currentVelocity, smoothTime);

        this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(realtarget.position.x, _startYPos, currentZpos), ref velocityF, smoothTime, speed, deltaTime);
    }
}
