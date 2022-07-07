using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Abilities : MonoBehaviourPun, IPunObservable
{
    public bool[] resizeable;
    public int[] cooldown;
    public int[] duration;
    public PlayerController controller;
    public GameObject[] ranges;
    public int _ranges;
    public int id;
    public GameObject invisibleIcon, wall;
    public static Abilities instance;
    public int[] ids;
    private PhotonView photonView;
    public Vector3 offset;
    private void Awake()
    {
        photonView = this.gameObject.GetComponent<PhotonView>();
        if(photonView.IsMine)
        controller = this.GetComponent<PlayerController>();
        instance = this;
    }
    private void Start()
    {
        foreach(GameObject obj in ranges)
        {
            obj.SetActive(false);
        }
        foreach(GameObject obj in GameVariables.refs.abilityIcons)
        {
            obj.SetActive(false);
        }
        //id = GameManager.instance.playerID;
        GameVariables.refs.abilityIcons[ids[id -1] - 1].SetActive(true);
    }
    public void init()
    {
        Invoke("ability" + id.ToString(), 0f);
        photonView.RPC("ability" + id.ToString(), RpcTarget.All);
    }
    [PunRPC]
    private void ability2()
    {
        if(photonView.IsMine)
        {
            this.GetComponent<Outline1>().enabled = true;
            invisibleIcon.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(7).gameObject.SetActive(false);
        }
        Invoke("endability2", duration[1]);
    }
    private void endability2()
    {
        if(photonView.IsMine)
        {
            this.GetComponent<Outline1>().enabled = false;
            invisibleIcon.SetActive(false);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(7).gameObject.SetActive(true);
        }
    }
    [PunRPC]
    private void ability5()
    {
        //Vector3 force = new Vector3(0, 500, 0) * transform.rotation
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 100 + new Vector3(0, 500, 0));
    }
    private void ability9()
    {
        //Vector3 force = new Vector3(0, 500, 0) * transform.rotation
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 350 + new Vector3(0, 90, 0));
    }
    private GameObject _wall;
    [PunRPC]
    private void ability12()
    {
        if(_wall == null)
        {
            Transform demo = controller.AttackRangeComponent.transform.GetChild(1).GetChild(11).transform;
            _wall = Instantiate(wall, demo.position + offset, demo.rotation);
            Invoke("endability12", duration[1]);
        }
    }
    private void endability12()
    {
        Destroy(_wall);
    }
    public void Aim(Vector3 directionVector){
        if(!controller.InWater)
        {
            controller.aiming = true;
            controller.AttackRangeComponent.transform.GetChild(1).GetChild(ids[id -1] -1).gameObject.SetActive(true);
            controller.AttackRangeComponent.gameObject.SetActive(true);
            controller._aimDirectionVector = directionVector;
            controller.AttackRangeComponent.rotation = Quaternion.LookRotation(controller._aimDirectionVector);
            controller.transform.rotation = Quaternion.LookRotation(controller._aimDirectionVector);

            float distanceToObstacle = controller.attackRange;
            RaycastHit hit;
            Vector3 unitCenterPos = transform.position + new Vector3(0, 0.5f, 0);
            Ray ray = new Ray(unitCenterPos, controller._aimDirectionVector);
            if(Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Obstacle")){
                float distance = Vector3.Distance(hit.point, unitCenterPos);
                if (distance < distanceToObstacle)
                    distanceToObstacle = distance; 
            }
            if(resizeable[ids[id -1] - 1])
            controller.AttackRangeComponent.transform.localScale = new Vector3(1, 1, distanceToObstacle);
            else
            controller.AttackRangeComponent.transform.localScale = new Vector3(1, 1, 3);
        }
    }
    public void StopAim()
    {
        controller.aiming = false;
        controller.AttackRangeComponent.gameObject.SetActive(false);
        controller.AttackRangeComponent.transform.GetChild(1).GetChild(ids[id -1] -1).gameObject.SetActive(false);

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
