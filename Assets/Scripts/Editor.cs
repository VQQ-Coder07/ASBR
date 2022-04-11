using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Editor : MonoBehaviour
{
    public GameObject prefab;
    public Terrain goTerrain;
    public Transform parent, demoCam, Cam;
    public enum mode
    {
        move, build, delete, spawnpoint
    }
    public mode _mode;
    public static Editor instance;
    private Vector3 startPos, initPos;
    private bool holding;
    public float mutliplier;
    public float deltaTime;
    public float speed;
    private float _currentVelocity = 0;
    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast (ray,out hit, 200) && Editor.instance._mode == Editor.mode.delete)
            {
                if(hit.transform.CompareTag("Tile"))
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            if (goTerrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity) && Editor.instance._mode == Editor.mode.build) 
            {
                Instantiate(prefab, hit.point, Quaternion.identity, parent);
            }
            if (Editor.instance._mode == Editor.mode.move) 
            {
                startPos = Input.mousePosition;
                initPos = demoCam.position;
                holding = true;
            }
        }
        if(holding)
        {
            Vector3 demoPos = (initPos + (Input.mousePosition - startPos)) * mutliplier;
            demoCam.position = new Vector3(-demoPos.x, demoCam.position.y, -demoPos.y);
            //CameraController.gameObject.transform.position =  = Vector3.SmoothDamp(this.transform.position, new Vector3(realtarget.position.x, _startYPos, currentZpos), ref velocityF, smoothTime, speed, deltaTime);
            if (Input.GetMouseButtonUp(0))
            {
                initPos = new Vector3(-demoPos.x, demoCam.position.y, -demoPos.y);
                holding = false;
            }
        }
    }
}
