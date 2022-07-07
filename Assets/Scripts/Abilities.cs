using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Abilities : MonoBehaviourPun, IPunObservable
{
    public bool[] global; 
    public bool[] resizeable;
    public int[] cooldown;
    public int[] duration;
    public PlayerController controller;
    public GameObject[] ranges;
    public int _ranges;
    public int id;
    public GameObject invisibleIcon, wall, healIcon, waterIcon, shieldIcon, waterColider;
    public static Abilities instance;
    public int[] ids;
    [SerializeField]
    private PhotonView photonView, target;
    public Vector3 offset;
    public bool bot, ab6;
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
        //Invoke("ability" + id.ToString(), 0f);
        if (global[id - 1])
        {
            photonView.RPC("ability" + id.ToString(), RpcTarget.All);
        }
        else
        {   
            if(target != null)
            photonView.RPC("ability" + id.ToString(), RpcTarget.All, photonView.ViewID, target.ViewID);
        }
    }
    [PunRPC]
    private void ability1(int sender, int _target)
    {
        if (photonView.IsMine)
        {
            if (photonView.ViewID == sender)
            {
                Debug.LogError("SND");
            }
            if (photonView.ViewID == _target)
            {
                Debug.LogError("REC");
                GetComponent<NetworkSync>().ModifyHealth(photonView.ViewID, -50);
            }
        }
        else
        {
            if (PhotonView.Find(_target).IsMine)
            {
                PhotonView.Find(_target).gameObject.GetComponent<Abilities>().ability6(sender, _target);
            }
        }
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
    private void ability3()
    {
        this.GetComponent<PlayerController>().speed = 3f;
        Invoke("endability3", duration[2]);
    }
    private void endability3()
    {
        this.GetComponent<PlayerController>().speed = 2f;
    }
    [PunRPC]
    private void ability5()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 100 + new Vector3(0, 500, 0));
    }
    [PunRPC]
    private void ability6(int sender, int _target)
    {
        if(photonView.IsMine)
        {
            if (photonView.ViewID == sender)
            {
                Debug.LogError("SND");
            }
            if (photonView.ViewID == _target)
            {
                Debug.LogError("REC");
                GetComponent<PlayerController>().speed = 0;
                GetComponent<PlayerController>().attackRange = 0;
                Invoke("end_ability6", duration[5]);
            }
        }
        else
        {
            if(PhotonView.Find(_target).IsMine)
            {
                PhotonView.Find(_target).gameObject.GetComponent<Abilities>().ability6(sender, _target);
            }
        }
    }
    private void end_ability6()
    {
        GetComponent<PlayerController>().speed = GetComponent<PlayerController>().init_speed;
        GetComponent<PlayerController>().attackRange = GetComponent<PlayerController>().init_attackRange;
    }
    [PunRPC]
    private void ability7(int sender, int _target)
    {
        if(photonView.IsMine)
        {
            if (photonView.ViewID == sender)
            {
                Debug.LogError("SND");
            }
            if (photonView.ViewID == _target)
            {
                Debug.LogError("REC");
                Quaternion rot = transform.rotation;
                transform.LookAt(PhotonView.Find(sender).gameObject.transform);
                this.GetComponent<Rigidbody>().AddForce(transform.forward * 350 + new Vector3(0, 90, 0));
                transform.rotation = rot;
            }
        }
        else
        {
            if(PhotonView.Find(_target).IsMine)
            {
                PhotonView.Find(_target).gameObject.GetComponent<Abilities>().ability7(sender, _target);
            }
        }
    }
    [PunRPC]
    private void ability8()
    {
        if (photonView.IsMine)
        {
            healIcon.SetActive(true);
            InvokeRepeating("heal", 0, 0.9f);
            Invoke("endability8", duration[7]);
        }
        else
        {

        }
    }
    private void heal()
    {
        GetComponent<NetworkSync>().ModifyHealth(photonView.ViewID, 10);
    }
    private void endability8()
    {
        CancelInvoke("heal");
        healIcon.SetActive(false);
    }
    [PunRPC]
    private void ability9()
    {
        //Vector3 force = new Vector3(0, 500, 0) * transform.rotation
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 350 + new Vector3(0, 90, 0));
    }
    [PunRPC]
    private void ability10()
    {
        if (photonView.IsMine)
        {
            shieldIcon.SetActive(true);
            GetComponent<NetworkSync>().ab10 = true;
            Invoke("endability10", duration[7]);
        }
        else
        {
            GetComponent<NetworkSync>().ab10 = false;
        }
    }
    private void endability10()
    {
        shieldIcon.SetActive(false);
    }
    [PunRPC]
    private void ability11()
    {
        if (photonView.IsMine)
        {
            controller.InWater = false;
            waterColider.SetActive(false);
            waterIcon.SetActive(true);
            Invoke("endability11", duration[10]);
        }
        else
        {

        }
    }
    private void endability11()
    {
        waterColider.SetActive(true);
        waterIcon.SetActive(false);
    }
    private GameObject _wall;
    [PunRPC]
    private void ability12()
    {
        if(_wall == null)
        {
            Transform demo = controller.AttackRangeComponent.transform.GetChild(1).GetChild(11).transform;
            _wall = Instantiate(wall, demo.position + offset, demo.rotation);
            Invoke("endability12", duration[11]);
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
            if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(hit.point, unitCenterPos);
                if (distance < distanceToObstacle)
                    target = hit.transform.gameObject.GetComponent<PhotonView>();
                    
            }
            else
            {
                target = null;
            }
                if (resizeable[ids[id -1] - 1])
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
